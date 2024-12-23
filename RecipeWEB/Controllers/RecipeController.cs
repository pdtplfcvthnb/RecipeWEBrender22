using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Entities;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        public RecipeContext Context { get; }
        public RecipeController(RecipeContext context)
        {
            Context = context;
        }

        [Authorization.AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Recipe> recipes = Context.Recipes.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(recipes);
        }

        [Authorization.Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Recipe? recipe = Context.Recipes.Where(x => x.RecipeId == id).FirstOrDefault();
            if (recipe == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }

        [Authorization.Authorize]
        [HttpPost]
        public IActionResult Add(Recipe recipe)
        {
            Context.Recipes.Add(recipe);
            Context.SaveChanges();
            return Ok(recipe);
        }

        [Authorization.Authorize]
        [HttpPut]
        public IActionResult Update(Recipe recipe)
        {
            Context.Recipes.Add(recipe);
            Context.SaveChanges();
            return Ok(recipe);
        }

        [Authorization.Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Recipe? recipe = Context.Recipes.Where(x => x.RecipeId == id).FirstOrDefault();
            if (recipe == null)
            {
                return BadRequest("Not Found");
            }
            Context.Recipes.Remove(recipe);
            Context.SaveChanges();
            return Ok();
        }

    }
}
