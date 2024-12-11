using RecipeWEB.Models;

namespace RecipeWEB.Contracts.FavoriteRecipe
{
    public class UpdateFavoriteRecipeContract
    {
        public int FavoriteRecipeId { get; set; }

        public int UserId { get; set; }

        public int RecipeId { get; set; }

        public DateTime? AddedDate { get; set; }
    }
}
