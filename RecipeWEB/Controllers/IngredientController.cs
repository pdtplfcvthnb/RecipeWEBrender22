﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.Ingredient;
using RecipeWEB.Entities;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        public RecipeContext Context { get; }
        public IngredientController(RecipeContext context)
        {
            Context = context;
        }
        [Authorization.AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Ingredient> ingredients = Context.Ingredients.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(ingredients);
        }
        [Authorization.Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Ingredient? ingredient = Context.Ingredients.Where(x => x.IngredientId == id).FirstOrDefault();
            if (ingredient == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }

        [Authorization.Authorize]
        [HttpPost]
        public IActionResult Add(CreateIngredientContract ingredient)
        {
            var ingredient1 = new Ingredient()
            {
                Name = ingredient.Name,
                AllergenId = ingredient.AllergenId,
            };
            Context.Ingredients.Add(ingredient1);
            Context.SaveChanges();
            return Ok(ingredient1);
        }

        [Authorization.Authorize]
        [HttpPut]
        public IActionResult Update(UpdateIngredientContract ingredient)
        {
            Ingredient? ingredientforUp = Context.Ingredients.Where(x => x.IngredientId == ingredient.IngredientId).FirstOrDefault();
            if (ingredientforUp == null)
            {
                return BadRequest("Not Found");
            }
            ingredientforUp.Name = ingredient.Name;
            ingredientforUp.AllergenId = ingredient.AllergenId;
            Context.SaveChanges();
            return Ok(ingredientforUp);
        }

        [Authorization.Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Ingredient? ingredient = Context.Ingredients.Where(x => x.IngredientId == id).FirstOrDefault();
            if (ingredient == null)
            {
                return BadRequest("Not Found");
            }
            Context.Ingredients.Remove(ingredient);
            Context.SaveChanges();
            return Ok();
        }
    }
}
