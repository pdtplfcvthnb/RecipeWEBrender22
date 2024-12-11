using System;
using System.Collections.Generic;

namespace RecipeWEB.Models;

public partial class Diet
{
    public int DietId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
