using System;
using System.Collections.Generic;

namespace RecipeWEB.Models;

public partial class KitchenTool
{
    public int ToolId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
