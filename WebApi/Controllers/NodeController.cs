using Application.NodeApplication;
using Application.Singleton.NodeApplication.SendedModel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("node")]
    public class NodeController : ControllerBase
    {
        private readonly NodeApplication _nodeApplication;

        public NodeController(
            NodeApplication nodeApplication
        )
        {
            _nodeApplication = nodeApplication;
        }

        [HttpPost("connect")]
        public IActionResult ConnectNode([FromBody] NodeConnectionModel model)
        {
            _nodeApplication.ConnectNode(model);
            return Ok();
        }

        [HttpPost("disconnect")]
        public IActionResult DisconnectNode([FromBody] NodeDisconnectionModel model)
        {
            _nodeApplication.DisconnectNode(model);
            return Ok();
        }
    }
}
