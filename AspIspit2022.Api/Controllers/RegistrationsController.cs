using AspIspit2022.Application.DTO;
using AspIspit2022.Application.UseCases.Commands;
using AspIspit2022.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspIspit2022.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        // POST api/<RegistrationsController>
        [HttpPost]
        public IActionResult Post(
            [FromBody] RegistrationDto dto,
            [FromServices] IAddRegistrationCommand command,
            [FromServices] UseCaseHandler handler
            )
        {
            handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        
    }
}
