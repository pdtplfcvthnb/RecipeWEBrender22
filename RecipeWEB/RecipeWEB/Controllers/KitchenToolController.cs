using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Contracts.KitchenTool;
using RecipeWEB.Models;

namespace RecipeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitchenToolController : ControllerBase
    {
        public RecipeContext Context { get; }
        public KitchenToolController(RecipeContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<KitchenTool> kitchenTools = Context.KitchenTools.ToList();
            //В <> пишем имя таблицы, а после context название таблицы+s
            return Ok(kitchenTools);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            KitchenTool? kitchenTool = Context.KitchenTools.Where(x => x.ToolId == id).FirstOrDefault();
            if (kitchenTool == null)
            {
                return BadRequest("Not Found");
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Add(CreateKitchenToolContract kitchenTool)
        {
            var kitchenTool1 = new KitchenTool()
            {
                Name = kitchenTool.Name,
            };
            Context.KitchenTools.Add(kitchenTool1);
            Context.SaveChanges();
            return Ok(kitchenTool);
        }

        [HttpPut]
        public IActionResult Update(UpdateKitchenToolContract kitchenTool)
        {
            KitchenTool? kitchenToolforUp = Context.KitchenTools.Where(x => x.ToolId == kitchenTool.ToolId).FirstOrDefault();
            if (kitchenToolforUp == null)
            {
                return BadRequest("Not Found");
            }
            kitchenToolforUp.Name = kitchenTool.Name;
            Context.SaveChanges();
            return Ok(kitchenToolforUp);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            KitchenTool? kitchenTool = Context.KitchenTools.Where(x => x.ToolId == id).FirstOrDefault();
            if (kitchenTool == null)
            {
                return BadRequest("Not Found");
            }
            Context.KitchenTools.Remove(kitchenTool);
            Context.SaveChanges();
            return Ok();
        }
    }
}
