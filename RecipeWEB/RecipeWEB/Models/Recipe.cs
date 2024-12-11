using System;
using System.Collections.Generic;

namespace RecipeWEB.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string Instructions { get; set; } = null!;

    public int? PrepTime { get; set; }

    public int? CookTime { get; set; }

    public int? Servings { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<FavoriteRecipe> FavoriteRecipes { get; set; } = new List<FavoriteRecipe>();

    public virtual ICollection<RecipeComment> RecipeComments { get; set; } = new List<RecipeComment>();

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual ICollection<RecipeRating> RecipeRatings { get; set; } = new List<RecipeRating>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Diet> Diets { get; set; } = new List<Diet>();

    public virtual ICollection<KitchenTool> Tools { get; set; } = new List<KitchenTool>();
}
