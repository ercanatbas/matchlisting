using System.Net.Mime;
using System.Threading.Tasks;
using MatchList.Api.Filters;
using MatchList.Application.Queries.Matches;
using MatchList.Application.Queries.MathAuditLogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatchList.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class MatchController : ControllerBase
    {
        private readonly IMatchQuery         _matchQuery;
        private readonly IMatchAuditLogQuery _matchAuditLogQuery;

        public MatchController(IMatchQuery matchQuery, IMatchAuditLogQuery matchAuditLogQuery)
        {
            _matchQuery         = matchQuery;
            _matchAuditLogQuery = matchAuditLogQuery;
        }

        [HttpGet(Name = "You can shoot all matches.")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(MatchModel),        StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JsonErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _matchQuery.GetAllMatches();
            return Ok(response);
        }

        [HttpGet("logs", Name = "You can take logs of the match.")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(MatchAuditLogModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JsonErrorResponse),  StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(JsonErrorResponse),  StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllLogs([FromQuery] MatchAuditLogListRequest request)
        {
            var response = await _matchAuditLogQuery.GetAllMatchLogs(request);
            return Ok(response);
        }
    }
}