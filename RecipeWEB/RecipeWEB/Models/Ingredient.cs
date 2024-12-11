using System;
using System.Collections.Generic;

namespace RecipeWEB.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string Name { get; set; } = null!;

    public int? AllergenId { get; set; }

    public virtual Allergen? Allergen { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
