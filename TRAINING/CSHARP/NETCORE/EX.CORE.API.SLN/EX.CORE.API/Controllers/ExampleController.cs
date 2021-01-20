using EX.DTO;
using EX.SignalR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EX.CORE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ExampleHub exampleHub;

        public ExampleController(
            ExampleHub exampleHub
        )
        {
            this.exampleHub = exampleHub;
        }

        [HttpGet()]
        public async Task<IActionResult> GetExample()
        {
            await exampleHub.SendAll("Test", new Message
            {
                FullName = "Tellek Liberty",
                Age = 47,
                Address = "13241 Saratoga Ln N, Champlin, MN 55316"
            });
            return await Task.FromResult(new JsonResult("Jane Doe"));
        }

        [HttpGet("{value}")]
        public async Task<IActionResult> GetExample(string value)
        {
            await exampleHub.SendAll("Test", new Message
            {
                FullName = "Tellek Liberty",
                Age = 47,
                Address = "13241 Saratoga Ln N, Champlin, MN 55316"
            });
            return await Task.FromResult(new JsonResult("John Doe" + value));
        }
    }
}
