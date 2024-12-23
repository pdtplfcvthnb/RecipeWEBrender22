using System.ComponentModel.DataAnnotations;

namespace BlazorAppRecipe.DataAccess.Models.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
