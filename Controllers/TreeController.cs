using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TreeBase.DTO;
using TreeBase.ExceptionHandling;
using TreeBase.Helpers;
using TreeBase.Services;

namespace TreeBase.Controllers
{
    [Tags("user.tree")]
    [Route("/api.user.tree")]
    public class TreeController : Controller
    {
        private readonly ITreeService repository;

        public TreeController(ITreeService repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(NodeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = Defines.GetOrCreateTree)]
        public async Task<IActionResult> GetNode([FromQuery][Required] string treeName)
        {
            return new OkObjectResult(await repository.GetOrCreateTree(treeName));
        }
    }
}
