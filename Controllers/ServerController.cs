using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {

        private readonly APIContext _ctx;
        public ServerController(APIContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var response = _ctx.Servers.OrderBy(s => s.Id).ToList();
            return Ok(response);

        }

        [HttpGet("{id}",Name="GetServer")]
        public IActionResult Get(int id)
        {
            var response = _ctx.Servers.Find(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = _ctx.Servers.Find(id);

            if (server == null)
            {
                return NotFound();
            }
            if (msg.Payload == "activate")
            {
                server.IsOnline = true;
               
            }
            if (msg.Payload == "deactivate")
            {
                server.IsOnline = false;
               
            }
            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}