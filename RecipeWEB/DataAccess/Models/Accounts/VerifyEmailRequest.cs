﻿using System.ComponentModel.DataAnnotations;

namespace RecipeWEB.DataAccess.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
