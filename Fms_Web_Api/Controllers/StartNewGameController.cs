using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartNewGameController : ControllerBase
    {
        private readonly PlayerCreator _playerCreator = new PlayerCreator();
        
        // POST api/values
        [HttpPost]
        public void Post([FromBody] GameDetails gameDetails)
        {
             _playerCreator.CreateAllPlayers();
        }


    }
}
