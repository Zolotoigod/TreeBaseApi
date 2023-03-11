using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TreeBase.DTO;
using TreeBase.ExceptionHandling;
using TreeBase.Helpers;
using TreeBase.Services;

namespace TreeBase.Controllers
{
    [Tags("user.tree.node")]
    [Route("/api.user.tree.node")]
    public class TreeNodeController : Controller
    {
        private readonly ITreeService repository;

        public TreeNodeController(ITreeService repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = Defines.AddNode)]
        public async Task<IActionResult> Add([FromQuery] AddNodeRequest request)
        {
            await repository.Add(request);
            return new OkResult();
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = Defines.RemoveNode)]
        public async Task<IActionResult> Delete(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] int nodeId)
        {
            await repository.Remove(treeName, nodeId);
            return new OkResult();
        }

        [HttpPatch]
        [Route("rename")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Description = Defines.UpdateNode)]
        public async Task<IActionResult> Update([FromQuery]UpdateNodeRequest request)
        {
            await repository.Update(request);
            return new OkResult();
        }
    }
}
