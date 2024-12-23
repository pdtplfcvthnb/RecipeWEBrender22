using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.Users;
using RecipeWEB.Entities;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public RecipeContext Context { get; }
        public UserController(RecipeContext context)
        {
            Context = context;
        }

        [Authorization.Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = Context.Users.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(users);
        }

        [Authorization.Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            User? user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }

        [Authorization.AllowAnonymous]
        [HttpPost]
        public IActionResult Add(CreateUserContract user)
        {
            var user1 = new User()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegistrationDate = user.RegistrationDate,
            };
            Context.Users.Add(user1);
            Context.SaveChanges();
            return Ok(user1);
        }

        [Authorization.Authorize(Role.Admin)]      
        [HttpPut("id:int")]
        public IActionResult Update(UpdateUserContract user)
        {
            User? userforUp = Context.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
            if (userforUp == null)
            {
                return BadRequest("Not Found");
            }
            userforUp.Username = user.Username;
            userforUp.Email = user.Email;
            userforUp.Password = user.Password;
            userforUp.FirstName = user.FirstName;
            userforUp.LastName = user.LastName;
            Context.SaveChanges();
            return Ok(userforUp);
        }
        [Authorization.Authorize(Role.Admin)]
        [HttpDelete("id:int")]
        public IActionResult Delete(int id)
        {
            User? user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Not Found");
            }
            Context.Users.Remove(user);
            Context.SaveChanges();
            return Ok(user);
        }
    }
}
