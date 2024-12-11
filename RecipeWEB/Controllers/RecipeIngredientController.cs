using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.RecipeComment;
using RecipeWEB.Contracts.RecipeIngredient;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientController : ControllerBase
    {
        public RecipeContext Context { get; }
        public RecipeIngredientController(RecipeContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<RecipeIngredient> recipeIngredients = Context.RecipeIngredients.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(recipeIngredients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            RecipeIngredient? recipeIngredient = Context.RecipeIngredients.Where(x => x.IngredientId == id && x.RecipeId == id).FirstOrDefault();
            if (recipeIngredient == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Add(CreateRecipeIngredientContract recipeIngredient)
        {
            var recipeIngredient1 = new RecipeIngredient()
            {
                IngredientId = recipeIngredient.IngredientId,
                Quantity = recipeIngredient.Quantity,
            };
            Context.RecipeIngredients.Add(recipeIngredient1);
            Context.SaveChanges();
            return Ok(recipeIngredient1);
        }

        [HttpPut]
        public IActionResult Update(UpdateRecipeIngredientContract recipeIngredient)
        {
            RecipeIngredient? recipeIngredientforUp = Context.RecipeIngredients.Where(x => x.IngredientId == recipeIngredient.IngredientId && x.RecipeId == recipeIngredient.RecipeId).FirstOrDefault();
            if (recipeIngredientforUp == null)
            {
                return BadRequest("Not Found");
            }
            recipeIngredientforUp.IngredientId = recipeIngredient.IngredientId;
            recipeIngredientforUp.Quantity = recipeIngredient.Quantity;
            Context.SaveChanges();
            return Ok(recipeIngredientforUp);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            RecipeIngredient? recipeIngredient = Context.RecipeIngredients.Where(x => x.IngredientId == id && x.RecipeId == id).FirstOrDefault();
            if (recipeIngredient == null)
            {
                return BadRequest("Not Found");
            }
            Context.RecipeIngredients.Remove(recipeIngredient);
            Context.SaveChanges();
            return Ok();
        }
    }
}
