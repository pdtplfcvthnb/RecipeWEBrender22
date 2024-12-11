using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.RecipeRating;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RecipeRatingController : ControllerBase
    {
        public RecipeContext Context { get; }
        public RecipeRatingController(RecipeContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<RecipeRating> recipeRatings = Context.RecipeRatings.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(recipeRatings);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            RecipeRating? recipeRating = Context.RecipeRatings.Where(x => x.RatingId == id).FirstOrDefault();
            if (recipeRating == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Add(CreateRecipeRatingContract recipeRating)
        {
            var recipeRating1 = new RecipeRating()
            {
                RecipeId = recipeRating.RecipeId,
                UserId = recipeRating.UserId,
                Rating = recipeRating.Rating,
                Review = recipeRating.Review,
                RatingDate = recipeRating.RatingDate,
            };
            Context.RecipeRatings.Add(recipeRating1);
            Context.SaveChanges();
            return Ok(recipeRating1);
        }

        [HttpPut]
        public IActionResult Update(UpdateRecipeRatingContract recipeRating)
        {
            RecipeRating? recipeRatingforUp = Context.RecipeRatings.Where(x => x.RatingId == recipeRating.RatingId).FirstOrDefault();
            if (recipeRatingforUp == null)
            {
                return BadRequest("Not Found");
            }
            recipeRatingforUp.RatingId = recipeRating.RatingId;
            recipeRatingforUp.RatingId = recipeRating.UserId; 
            recipeRatingforUp.Rating = recipeRating.Rating;
            recipeRatingforUp.Review = recipeRating.Review;
            recipeRatingforUp.RatingDate = recipeRating.RatingDate;
            Context.SaveChanges();
            return Ok(recipeRatingforUp);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            RecipeRating? recipeRating = Context.RecipeRatings.Where(x => x.RatingId == id).FirstOrDefault();
            if (recipeRating == null)
            {
                return BadRequest("Not Found");
            }
            Context.RecipeRatings.Remove(recipeRating);
            Context.SaveChanges();
            return Ok();
        }
    }
}
