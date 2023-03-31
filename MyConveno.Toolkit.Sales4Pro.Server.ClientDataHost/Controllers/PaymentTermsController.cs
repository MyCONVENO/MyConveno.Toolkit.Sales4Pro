using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentTermsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<PaymentTermsController> _logger;

        public PaymentTermsController(IConfiguration config, ILogger<PaymentTermsController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetByPaymentTermId")]
        //.../PaymentTerms/GetByPaymentTermId?PaymentTermid=12345
        public ActionResult<PaymentTerm> Get(string PaymentTermid = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[PaymentTerm] ");
                sbSelect.Append("WHERE (PaymentTermId =  '" + PaymentTermid + "')");

                IEnumerable<PaymentTerm> PaymentTerms = (List<PaymentTerm>)connection.Query<PaymentTerm>(sbSelect.ToString(), null, null, true, 0);

                if (PaymentTerms == null || !PaymentTerms.Any())
                    return NotFound();
                else
                    return Ok(PaymentTerms.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        //.../PaymentTerms/GetAll
        public ActionResult<IEnumerable<PaymentTerm>> Get()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[PaymentTerm]");

                IEnumerable<PaymentTerm> PaymentTerms = (IEnumerable<PaymentTerm>)connection.Query<PaymentTerm>(sbSelect.ToString(), null, null, true, 0);

                if (PaymentTerms == null || !PaymentTerms.Any())
                    return Ok(new List<PaymentTerm>());
                else
                    return Ok(PaymentTerms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<string> AddPaymentTerm(PaymentTerm PaymentTerm)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbInsert = new();
                sbInsert.Append("INSERT INTO [PaymentTerm] (");
                sbInsert.Append("PaymentTermId, ");
                sbInsert.Append("PaymentTermName) ");
                sbInsert.Append("VALUES (");
                sbInsert.Append("@PaymentTermId, ");
                sbInsert.Append("@PaymentTermName) ");
              
                int rowsAffected = connection.Execute(sbInsert.ToString(), PaymentTerm);
                return Ok(rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult UpdatePaymentTerm(PaymentTerm PaymentTerm)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbUpdate = new();
                sbUpdate.Append("UPDATE [PaymentTerm] SET ");
                sbUpdate.Append("PaymentTermName = @PaymentTermName ");
                sbUpdate.Append("WHERE (PaymentTermId = @PaymentTermId)");

                int rowsAffected = connection.Execute(sbUpdate.ToString(), PaymentTerm);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{PaymentTermid}")]
        public ActionResult DeletePaymentTerm(string PaymentTermid)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbDelete = new();
                sbDelete.Append("DELETE FROM [PaymentTerm] ");
                sbDelete.Append(string.Format("WHERE (PaymentTermId = '{0}')", PaymentTermid));

                int rowsAffected = connection.Execute(sbDelete.ToString());
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{DeleteAll}")]
        public ActionResult DeleteAllPaymentTerms()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                connection.Execute("TRUNCATE TABLE [PaymentTerm]");
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
