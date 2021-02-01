using Microsoft.AspNetCore.Mvc;

namespace CadContato.WebApi.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Lembre-se de gerar a documentação Swagger");
        }
    }
}
