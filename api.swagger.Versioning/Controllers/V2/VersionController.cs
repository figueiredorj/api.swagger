using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.swagger.Versioning.Controllers.V2
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("2.0")]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;

        public VersionController(ILogger<VersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public string Get() => "Version 2.0";
    }
}