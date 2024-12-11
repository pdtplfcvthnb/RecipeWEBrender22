using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWEB.Contracts.RecipeIngredient
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateRecipeIngredientContract : ControllerBase
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string? Quantity { get; set; }
    }
}
