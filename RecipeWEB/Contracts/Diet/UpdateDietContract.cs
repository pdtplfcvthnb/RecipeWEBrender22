namespace RecipeWEB.Contracts.Diet
{
    public class UpdateDietContract
    {
        public int DietId { get; set; }
        public string Name { get; set; } = null!;
    }
}
