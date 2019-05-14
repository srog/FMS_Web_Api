using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormationsController : Controller
    {
        private readonly IConfiguration _configuration;
        public FormationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<Formations> GetAll()
        {
            return _configuration.GetSection("FormationSection").Get<Formations>();
        }
    }
}
