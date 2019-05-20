using Fms_Web_Api.Models;
using Fms_Web_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    public class AdvanceWeekController : Controller
    {
        private IGameDetailsService _gameDetailsService { get; }
        public AdvanceWeekController(IGameDetailsService gameDetailsService)
        {
            _gameDetailsService = gameDetailsService;

        }
        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] GameDetails gameDetails)
        {
            return _gameDetailsService.AdvanceWeek(gameDetails);
        }
    }
}
