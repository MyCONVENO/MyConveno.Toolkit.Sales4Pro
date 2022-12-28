using Microsoft.AspNetCore.Mvc;

namespace MyConveno.Toolkit.Sales4Pro.Server.BaseDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteZIPFileInBlobController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<DeleteZIPFileInBlobController> _logger;

        public DeleteZIPFileInBlobController(IConfiguration config, ILogger<DeleteZIPFileInBlobController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get(string filename)
        {
            CSVBaseDataService service = new CSVBaseDataService(_config);
            service.DeleteZIPFileFromBLOBStorage(filename);
            return Ok("File deleted");
        }
    }
}
