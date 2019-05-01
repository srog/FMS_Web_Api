using Fms_Web_Api.Data;
using Fms_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fms_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerAttributesController : ControllerBase
    {
        private readonly PlayerAttributesQuery _PlayerAttributesQuery = new PlayerAttributesQuery();

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<PlayerAttributes> Get(int id)
        {
            return _PlayerAttributesQuery.Get(id);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody] PlayerAttributes PlayerAttributes)
        {
            return _PlayerAttributesQuery.Add(PlayerAttributes);
        }

        // PUT api/values/5
        [HttpPut]
        public int Put([FromBody] PlayerAttributes PlayerAttributes)
        {
            return _PlayerAttributesQuery.Update(PlayerAttributes);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _PlayerAttributesQuery.Delete(id);
        }
    }
}
