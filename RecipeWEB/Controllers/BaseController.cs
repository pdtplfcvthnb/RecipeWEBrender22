using Microsoft.AspNetCore.Mvc;
using RecipeWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace RecipeWEB.Controllers
{
    [Controller]
    public class BaseController : ControllerBase
    {
        public User User => (User)HttpContext.Items["User"];
    }
}
