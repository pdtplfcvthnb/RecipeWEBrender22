using System;
using System.Collections.Generic;

namespace RecipeWEB.Models;

public partial class Allergen
{
    public int AllergenId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
