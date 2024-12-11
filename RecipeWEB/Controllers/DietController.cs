using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.Diet;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietController : ControllerBase
    {
        public RecipeContext Context { get; }
        public DietController(RecipeContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Diet> diets = Context.Diets.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(diets);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Diet? diet = Context.Diets.Where(x => x.DietId == id).FirstOrDefault();
            if (diet == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Add(CreateDietContract diet)
        {
            var diet1 = new Diet()
            {
                Name = diet.Name,
            };
            Context.Diets.Add(diet1);
            Context.SaveChanges();
            return Ok(diet1);
        }

        [HttpPut]
        public IActionResult Update(UpdateDietContract diet)
        {
            Diet? dietforUp = Context.Diets.Where(x => x.DietId == diet.DietId).FirstOrDefault();
            if (dietforUp == null)
            {
                return BadRequest("Not Found");
            }
            dietforUp.Name = diet.Name;
            Context.SaveChanges();
            return Ok(dietforUp);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Diet? diet = Context.Diets.Where(x => x.DietId == id).FirstOrDefault();
            if (diet == null)
            {
                return BadRequest("Not Found");
            }
            Context.Diets.Remove(diet);
            Context.SaveChanges();
            return Ok();
        }

    }
}
