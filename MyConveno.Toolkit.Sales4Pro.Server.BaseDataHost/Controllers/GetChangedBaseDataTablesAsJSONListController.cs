using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MyConveno.Toolkit.Sales4Pro.Server.BaseDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class GetChangedBaseDataTablesAsJSONListController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<GetChangedBaseDataTablesAsJSONListController> _logger;

        public GetChangedBaseDataTablesAsJSONListController(IConfiguration config, ILogger<GetChangedBaseDataTablesAsJSONListController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Get(string jsontablelist)
        {
            long initDatetimeTicks = new DateTime(2000, 1, 1).Ticks;
            CSVBaseDataService service = new(_config);

            Dictionary<string, long> jsonTableDict = JsonSerializer.Deserialize<Dictionary<string, long>>(jsontablelist);

            List<UpdateProgressItem> results = new();

            foreach (string key in jsonTableDict.Keys)
            {
                long syncDateTimeTicks = jsonTableDict.ContainsKey(key) ? jsonTableDict[key] : initDatetimeTicks;
                int changes = service.GetTableChangesCount(key, syncDateTimeTicks);
                if (changes > 0)
                    results.Add(new UpdateProgressItem(key, changes));
            }

            return Ok(JsonConvert.SerializeObject(results));
        }
    }
}
