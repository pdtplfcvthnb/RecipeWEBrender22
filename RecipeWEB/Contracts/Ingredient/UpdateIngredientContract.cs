using RecipeWEB.Models;

namespace RecipeWEB.Contracts.Ingredient
{
    public class UpdateIngredientContract
    {
        public int IngredientId { get; set; }
        public string Name { get; set; } = null!;
        public int? AllergenId { get; set; }

    }
}
