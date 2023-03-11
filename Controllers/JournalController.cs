using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TreeBase.DTO;
using TreeBase.ExceptionHandling;
using TreeBase.Helpers;
using TreeBase.Services;

namespace TreeBase.Controllers
{
    [Tags("user.journal")]
    [Route("/api.user.journal")]
    public class JournalController : Controller
    {
        private readonly IJournalService repository;

        public JournalController(IJournalService repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("getRange")]
        [ProducesResponseType(typeof(LogRecordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = Defines.FindJornals)]
        public async Task<IActionResult> GetRange([FromQuery] RecordFindRequest request) =>
            new OkObjectResult(await repository.Find(request));


        [HttpGet]
        [Route("getSingle")]
        [ProducesResponseType(typeof(LogRecordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = Defines.GetJornal)]
        public async Task<IActionResult> GetById([FromQuery][Required] int logId) =>
            new OkObjectResult(await repository.GetById(logId));
    }
}
