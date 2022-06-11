using AspIspit2022.Application.UseCases.Commands;
using AspIspit2022.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspIspit2022.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleTypesController : ControllerBase
    {       
        // DELETE api/<ServiceTypesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, 
            [FromServices] IDeleteVehicleTypeCommand command,
            [FromServices] UseCaseHandler handler)
        {
            handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
