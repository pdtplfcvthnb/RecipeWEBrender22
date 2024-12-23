using RecipeWEB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace RecipeWEB.Models;

public partial class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();
    public virtual ICollection<RecipeComment> RecipeComments { get; set; } = new List<RecipeComment>();
    public virtual ICollection<RecipeRating> RecipeRatings { get; set; } = new List<RecipeRating>();
    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    public bool AcceptTerms { get; set; }
    public Role Role { get; set; }
    public string? VerificationToken { get; set; }
    public DateTime? Verified { get; set; }
    public bool IsVerified => Verified.HasValue || PasswordReset.HasValue; 
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? PasswordReset { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
    public bool OwnsToken(string token)
    {
        return this.RefreshTokens?.Find(x => x.Token == token) != null;
    }
}