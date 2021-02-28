using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.swagger.Versioning.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;

        public VersionController(ILogger<VersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get() => "Version 1.0";
    }
}