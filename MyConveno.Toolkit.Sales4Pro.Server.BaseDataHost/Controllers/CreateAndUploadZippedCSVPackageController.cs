using Microsoft.AspNetCore.Mvc;

namespace MyConveno.Toolkit.Sales4Pro.Server.BaseDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class CreateAndUploadZippedCSVPackageController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CreateAndUploadZippedCSVPackageController> _logger;

        public CreateAndUploadZippedCSVPackageController(IConfiguration config, ILogger<CreateAndUploadZippedCSVPackageController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get(string tableName, long syncDateTimeTicks)
        {
            using (CSVBaseDataService service = new CSVBaseDataService(_config))
            {
                return Ok(service.CreateAndUploadZippedCSVPackage(tableName, syncDateTimeTicks));
            }
        }
    }
}
