namespace RecipeWEB.Contracts.RecipeRating
{
    public class CreateRecipeRatingContract
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }
        public string? Review { get; set; }
        public DateTime? RatingDate { get; set; }
    }
}
