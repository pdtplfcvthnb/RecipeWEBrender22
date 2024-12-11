using RecipeWEB.Models;

namespace RecipeWEB.Contracts.FavoriteRecipe
{
    public class CreateFavoriteRecipeContract
    {
        public int UserId { get; set; }

        public int RecipeId { get; set; }

        public DateTime? AddedDate { get; set; }

    }
}
