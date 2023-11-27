using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class StockCatalogsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<StockCatalogsController> _logger;

        public StockCatalogsController(IConfiguration config, ILogger<StockCatalogsController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetByStockCatalogId")]
        //.../StockCatalogs/GetByStockCatalogId?StockCatalogid=12345
        public ActionResult<StockCatalog> Get(string StockCatalogid = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[StockCatalog] ");
                sbSelect.Append("WHERE (StockCatalogId =  '" + StockCatalogid + "')");

                IEnumerable<StockCatalog> StockCatalogs = (List<StockCatalog>)connection.Query<StockCatalog>(sbSelect.ToString(), null, null, true, 0);

                if (StockCatalogs == null || !StockCatalogs.Any())
                    return NotFound();
                else
                    return Ok(StockCatalogs.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        //.../StockCatalogs/GetAll
        public ActionResult<IEnumerable<StockCatalog>> Get()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[StockCatalog]");

                IEnumerable<StockCatalog> StockCatalogs = (IEnumerable<StockCatalog>)connection.Query<StockCatalog>(sbSelect.ToString(), null, null, true, 0);

                if (StockCatalogs == null || !StockCatalogs.Any())
                    return Ok(new List<StockCatalog>());
                else
                    return Ok(StockCatalogs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<string> AddStockCatalog(StockCatalog StockCatalog)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbInsert = new();
                sbInsert.Append("INSERT INTO [StockCatalog] (");
                sbInsert.Append("StockCatalogId, ");
                sbInsert.Append("StockCatalogName) ");
                sbInsert.Append("VALUES (");
                sbInsert.Append("@StockCatalogId, ");
                sbInsert.Append("@StockCatalogName) ");
              
                int rowsAffected = connection.Execute(sbInsert.ToString(), StockCatalog);
                return Ok(rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpPut]
        public ActionResult UpdateStockCatalog(StockCatalog StockCatalog)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbUpdate = new();
                sbUpdate.Append("UPDATE [StockCatalog] SET ");
                sbUpdate.Append("StockCatalogName = @StockCatalogName ");
                sbUpdate.Append("WHERE (StockCatalogId = @StockCatalogId)");

                int rowsAffected = connection.Execute(sbUpdate.ToString(), StockCatalog);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpDelete("{StockCatalogid}")]
        public ActionResult DeleteStockCatalog(string StockCatalogid)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbDelete = new();
                sbDelete.Append("DELETE FROM [StockCatalog] ");
                sbDelete.Append(string.Format("WHERE (StockCatalogId = '{0}')", StockCatalogid));

                int rowsAffected = connection.Execute(sbDelete.ToString());
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
