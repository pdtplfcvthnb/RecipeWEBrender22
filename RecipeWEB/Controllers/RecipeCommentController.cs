using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.RecipeComment;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeCommentController : ControllerBase
    {
        public RecipeContext Context { get; }
        public RecipeCommentController(RecipeContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<RecipeComment> recipeComments = Context.RecipeComments.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(recipeComments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            RecipeComment? recipeComment = Context.RecipeComments.Where(x => x.CommentId == id).FirstOrDefault();
            if (recipeComment == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Add(CreateRecipeCommentContract recipeComment)
        {
            var recipeComment1 = new RecipeComment()
            {
                RecipeId = recipeComment.RecipeId,
                UserId = recipeComment.UserId,
                Comment = recipeComment.Comment,
                CommentDate  = recipeComment.CommentDate,
            };
            Context.RecipeComments.Add(recipeComment1);
            Context.SaveChanges();
            return Ok(recipeComment1);
        }

        [HttpPut]
        public IActionResult Update(UpdateRecipeCommentContract recipeComment)
        {
            RecipeComment? recipeCommentforUp = Context.RecipeComments.Where(x => x.CommentId == recipeComment.CommentId).FirstOrDefault();
            if (recipeCommentforUp == null)
            {
                return BadRequest("Not Found");
            }
            recipeCommentforUp.RecipeId = recipeComment.RecipeId;
            recipeCommentforUp.UserId = recipeComment.UserId;
            recipeCommentforUp.Comment = recipeComment.Comment;
            recipeCommentforUp.CommentDate = recipeComment.CommentDate; 
            Context.SaveChanges();
            return Ok(recipeCommentforUp);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            RecipeComment? recipeComment = Context.RecipeComments.Where(x => x.CommentId == id).FirstOrDefault();
            if (recipeComment == null)
            {
                return BadRequest("Not Found");
            }
            Context.RecipeComments.Remove(recipeComment);
            Context.SaveChanges();
            return Ok();
        }
    }
}
