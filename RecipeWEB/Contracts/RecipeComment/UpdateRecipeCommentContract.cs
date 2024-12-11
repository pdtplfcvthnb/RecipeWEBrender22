using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWEB.Contracts.RecipeComment
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateRecipeCommentContract : ControllerBase
    {
        public int CommentId { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime? CommentDate { get; set; }

    }
}
