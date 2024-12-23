using System.ComponentModel.DataAnnotations;

namespace BlazorAppRecipe.DataAccess.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
