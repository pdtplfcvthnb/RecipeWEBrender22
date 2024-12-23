﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.Alergens;
using RecipeWEB.Entities;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AllergenController : ControllerBase
    {
        public RecipeContext Context { get; }
        public AllergenController(RecipeContext context)
        {
            Context = context;
        }
        [Authorization.AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Allergen> allergens = Context.Allergens.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(allergens);
        }
        [Authorization.Authorize(Role.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Allergen? allergen = Context.Allergens.Where(x => x.AllergenId == id).FirstOrDefault();
            if (allergen == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }
        [Authorization.Authorize]
        [HttpPost]
        public IActionResult Add(CreateAllergenContract allergen)
        {
            var allergen1 = new Allergen()
            {
                Name = allergen.Name,
            };
            Context.Allergens.Add(allergen1);
            Context.SaveChanges();  
            return Ok(allergen1);
        }
        [Authorization.Authorize]
        [HttpPut]
        public IActionResult Update(CreateAllergenContract allergen)
        {
            Allergen? allergenforUp = Context.Allergens.Where(x => x.AllergenId == allergen.AllergenId).FirstOrDefault();
            if (allergenforUp == null)
            {
                return BadRequest("Not Found");
            }
            allergenforUp.Name = allergen.Name;
            Context.SaveChanges();
            return Ok(allergenforUp);
        }
        [Authorization.Authorize(Role.Admin)]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Allergen? allergen = Context.Allergens.Where(x => x.AllergenId == id).FirstOrDefault();
            if (allergen == null)
            {
                return BadRequest("Not Found");
            }
            Context.Allergens.Remove(allergen);
            Context.SaveChanges();
            return Ok();
        }
    }
}
