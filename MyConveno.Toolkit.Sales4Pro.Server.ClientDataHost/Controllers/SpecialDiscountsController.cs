using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialDiscountsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SpecialDiscountsController> _logger;

        public SpecialDiscountsController(IConfiguration config, ILogger<SpecialDiscountsController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetBySpecialDiscountId")]
        //.../SpecialDiscounts/GetBySpecialDiscountNumber?specialDiscountId=12345
        public ActionResult<SpecialDiscount> Get(string specialDiscountId = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[SpecialDiscount] ");
                sbSelect.Append("WHERE (SpecialDiscountId =  '" + specialDiscountId + "')");

                IEnumerable<SpecialDiscount> specialDiscounts = (List<SpecialDiscount>)connection.Query<SpecialDiscount>(sbSelect.ToString(), null, null, true, 0);

                if (specialDiscounts == null || !specialDiscounts.Any())
                    return Ok(new List<SpecialDiscount>());
                else
                    return Ok(specialDiscounts.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        //.../SpecialDiscounts/GetAll
        public ActionResult<IEnumerable<SpecialDiscount>> Get()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[SpecialDiscount]");

                IEnumerable<SpecialDiscount> specialDiscounts = (IEnumerable<SpecialDiscount>)connection.Query<SpecialDiscount>(sbSelect.ToString(), null, null, true, 0);

                if (specialDiscounts == null || !specialDiscounts.Any())
                    return NotFound();
                else
                    return Ok(specialDiscounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<string> AddSpecialDiscount(SpecialDiscount specialDiscount)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbInsert = new();
                sbInsert.Append("INSERT INTO [SpecialDiscount] (");
                sbInsert.Append("SpecialDiscountId, ");
                sbInsert.Append("Metadata) ");
                sbInsert.Append("VALUES (");
                sbInsert.Append("@SpecialDiscountId, ");
                sbInsert.Append("@Metadata) ");

                int rowsAffected = connection.Execute(sbInsert.ToString(), specialDiscount);
                return Ok(rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult UpdateSpecialDiscount(SpecialDiscount specialDiscount)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbUpdate = new();
                sbUpdate.Append("UPDATE [SpecialDiscount] SET ");
                sbUpdate.Append("Metadata = @Metadata ");
                sbUpdate.Append("WHERE (SpecialDiscountId = @SpecialDiscountId)");

                int rowsAffected = connection.Execute(sbUpdate.ToString(), specialDiscount);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{specialDiscountId}")]
        public ActionResult DeleteSpecialDiscount(string specialDiscountId)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbDelete = new();
                sbDelete.Append("DELETE FROM [SpecialDiscount] ");
                sbDelete.Append(string.Format("WHERE (SpecialDiscountId = '{0}')", specialDiscountId));

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
