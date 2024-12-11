namespace RecipeWEB.Contracts.RecipeRating
{
    public class UpdateRecipeRatingContract
    {
        public int RatingId { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }
        public string? Review { get; set; }
        public DateTime? RatingDate { get; set; }
    }
}
