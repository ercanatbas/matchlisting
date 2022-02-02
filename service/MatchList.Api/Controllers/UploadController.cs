using System.Net.Mime;
using System.Threading.Tasks;
using MatchList.Api.Filters;
using MatchList.Application.Commands.Matches;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchList.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class UploadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UploadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("files", Name = "You can upload match files.")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(void),              StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> File([FromForm] MatchUploadCommand command)
        {
            var commandResult = await _mediator.Send(command);
            if (!commandResult)
                return BadRequest();
            return NoContent();
        }
    }
}