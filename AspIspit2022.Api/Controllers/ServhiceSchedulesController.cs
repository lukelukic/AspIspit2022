using AspIspit2022.Application.DTO;
using AspIspit2022.Application.UseCases.Commands;
using AspIspit2022.Application.UseCases.Queries;
using AspIspit2022.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspIspit2022.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceSchedulesController : ControllerBase
    {
        private UseCaseHandler handler;

        public ServiceSchedulesController(UseCaseHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery] ServiceSchedulesSearch search,
            [FromServices] IGetServiceSchedulesQuery query)
        {
            return Ok(handler.HandleQuery(query, search));
        }

        [HttpPut("/api/serviceschedules/finish")]
        public IActionResult Put([FromBody] FinishServiceDto dto, 
            [FromServices] IFinishServiceCommand command)
        {
            handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
