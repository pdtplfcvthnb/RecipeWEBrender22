using MapsterMapper;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http.Metadata;
using RecipeWEB.Authorization;
using RecipeWEB.Helpers;
using BlazorAppRecipe.DataAccess.Models.Accounts;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Formats.Asn1;
using RecipeWEB.Models;
using RecipeWEB.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using RecipeWEB.Entities;

namespace RecipeWEB.Services
{
    public class AccountService : IAccountService
    {
        private readonly RecipeContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountService(
            RecipeContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }
        private void removeOldRefreshTokens(User account)
        {
            account.RefreshTokens.RemoveAll(x => !x.IsActive && x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = await _context.Users.Include(x => x.RefreshTokens).AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email);
            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(model.Password, account.Password))
                throw new AppException("Email or password is incorrect");

            var jwtToken = _jwtUtils.GenerateJwtToken(account);
            var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            removeOldRefreshTokens(account);
            _context.Users.Update(account);
            await _context.SaveChangesAsync();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }
        public async Task<AccountResponse> Create(CreateRequest model)
        {
            var existingUserCount = await _context.Users.Where(x => x.Email == model.Email).CountAsync();

            if (existingUserCount > 0)
                throw new AppException($"Email '{model.Email}' is already registered");
            var account = _mapper.Map<User>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _context.Users.Update(account);
            await _context.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);
        }
        private async Task<User> getAccount(int id)
        {
            var account = (_context.Users.Where(x => x.UserId == id)).FirstOrDefault();

            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }        
        
        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            var account = await getAccount(id);

            if (await _context.Users.Where(x => x.Email == model.Email).CountAsync() >0 )
                throw new AppException($"Email '{model.Email}' is already registered");
            
            if (!string.IsNullOrEmpty(model.Password))
                account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            _context.Users.Update(account);
            await _context.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);
        }
        public async Task Delete(int id)
        {
            var account = await getAccount(id);
            _context.Users.Remove(account);
            await _context.SaveChangesAsync();
        }

        private async Task<string> generateResetToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var tokenIsUnique = await _context.Users.AnyAsync(x => x.ResetToken == token);
            if(!tokenIsUnique)
                return await generateResetToken();

            return token;
        }
        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = _context.Users.Where(x => x.Email == model.Email).FirstOrDefault();
            if (account == null) return;

            account.ResetToken = await generateResetToken();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            _context.Users.Update(account);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            List<User> accounts = await _context.Users.ToListAsync();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        private async Task<User> getAccountByRefreshToken(string token)
        {
            var account = (_context.Users.Where(u => u.RefreshTokens.Any(t => t.Token == token))).SingleOrDefault();
            if (account == null) throw new AppException("Invalid token");
            return account;
        }

        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replace by new token");
            return newRefreshToken;
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User account, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)                    
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                   revokeDescendantRefreshTokens(childToken, account, ipAddress, reason);
            }
        }
        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsActive)
                throw new AppException("Invalid token");

            var newRefreshToken = await rotateRefreshToken(refreshToken, ipAddress);
            account.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(account);

            _context.Users.Update(account);
            await _context.SaveChangesAsync();

            var jwtToken = _jwtUtils.GenerateJwtToken(account);

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;
        }

        public async Task<string> generateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var tokenIsUnique = (await _context.Users.AnyAsync(x => x.ResetToken == token));

            if (tokenIsUnique)
                return await generateVerificationToken();
            return token;
        }

        public async Task Register(RegisterRequest model, string origin)
        {
            string email = model.Email;

            if (await _context.Users.AnyAsync(x => x.Email == model.Email))
                return; 

            var account = _mapper.Map<User>(model);

            var isFirstAccount = (await _context.Users.AnyAsync(x => x.Email == model.Email));
            account.Username = model.Login;
            account.FirstName = model.Firstname;
            account.LastName = model.Lastname;
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;
            account.VerificationToken = await generateVerificationToken();

            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _context.Users.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        private async Task<User> getAccountByResetToken(string token)
        {
            var account = await _context.Users.Where(x => x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow).SingleOrDefaultAsync();
            if (account == null) throw new AppException("Invalid token");
            return account;
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = await getAccountByRefreshToken(model.Token);

            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            _context.Users.Update(account);
            await _context.SaveChangesAsync();

        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var account= await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _context.Users.Update(account);
            await _context.SaveChangesAsync();

        }

        public async Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            await getAccountByResetToken(model.Token);
        }

        public async Task VerifyEmail(string token)
        {
            var account = _context.Users.Where(x => x.VerificationToken == token).FirstOrDefault();

            if (account == null)
                throw new AppException("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;
            _context.Users.Update(account);
            await _context.SaveChangesAsync();

        }
    }
}
