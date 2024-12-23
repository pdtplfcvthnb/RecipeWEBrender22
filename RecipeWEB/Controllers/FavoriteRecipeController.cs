using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.FavoriteRecipe;
using RecipeWEB.Entities;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteRecipeController : ControllerBase
    {
        public RecipeContext Context { get; }
        public FavoriteRecipeController(RecipeContext context)
        {
            Context = context;
        }
        [Authorization.AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<FavoriteRecipe> favoriteRecipes = Context.FavoriteRecipes.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(favoriteRecipes);
        }
        [Authorization.Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            FavoriteRecipe? favoriteRecipe = Context.FavoriteRecipes.Where(x => x.FavoriteRecipeId == id).FirstOrDefault();
            if (favoriteRecipe == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }
        [Authorization.Authorize]
        [HttpPost]
        public IActionResult Add(CreateFavoriteRecipeContract favoriteRecipe)
        {
            var favoriteRecipe1 = new FavoriteRecipe()
            {
                RecipeId = favoriteRecipe.RecipeId,
            };
            Context.FavoriteRecipes.Add(favoriteRecipe1);
            Context.SaveChanges();
            return Ok(favoriteRecipe);
        }
        [Authorization.Authorize]
        [HttpPut]
        public IActionResult Update(UpdateFavoriteRecipeContract favoriteRecipe)
        {
            FavoriteRecipe? favoriteRecipeforUp = Context.FavoriteRecipes.Where(x => x.FavoriteRecipeId == favoriteRecipe.FavoriteRecipeId).FirstOrDefault();
            if (favoriteRecipeforUp == null)
            {
                return BadRequest("Not Found");
            }
            favoriteRecipeforUp.UserId = favoriteRecipe.UserId;
            favoriteRecipeforUp.RecipeId = favoriteRecipe.RecipeId;
            Context.SaveChanges();
            return Ok(favoriteRecipeforUp);
        }
        [Authorization.Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            FavoriteRecipe? favoriteRecipe = Context.FavoriteRecipes.Where(x => x.FavoriteRecipeId == id).FirstOrDefault();
            if (favoriteRecipe == null)
            {
                return BadRequest("Not Found");
            }
            Context.FavoriteRecipes.Remove(favoriteRecipe);
            Context.SaveChanges();
            return Ok();
        }
    }
}
