using RecipeWEB.Models;

namespace RecipeWEB.Contracts.Ingredient
{
    public class CreateIngredientContract
    {
        public string Name { get; set; } = null!;
        public int? AllergenId { get; set; }
    }
}
