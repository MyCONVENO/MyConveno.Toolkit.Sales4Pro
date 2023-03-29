using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class AssociationsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AssociationsController> _logger;

        public AssociationsController(IConfiguration config, ILogger<AssociationsController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetByAssociationId")]
        //.../Associations/GetByAssociationId?associationid=12345
        public ActionResult<Association> Get(string associationid = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[Association] ");
                sbSelect.Append("WHERE (AssociationId =  '" + associationid + "')");

                IEnumerable<Association> associations = (List<Association>)connection.Query<Association>(sbSelect.ToString(), null, null, true, 0);

                if (associations == null || !associations.Any())
                    return NotFound();
                else
                    return Ok(associations.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        //.../Associations/GetAll
        public ActionResult<IEnumerable<Association>> Get()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[Association]");

                IEnumerable<Association> associations = (IEnumerable<Association>)connection.Query<Association>(sbSelect.ToString(), null, null, true, 0);

                if (associations == null || !associations.Any())
                    return Ok(new List<Association>());
                else
                    return Ok(associations);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<string> AddAssociation(Association association)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbInsert = new();
                sbInsert.Append("INSERT INTO [Association] (");
                sbInsert.Append("AssociationId, ");
                sbInsert.Append("AssociationName) ");
                sbInsert.Append("VALUES (");
                sbInsert.Append("@AssociationId, ");
                sbInsert.Append("@AssociationName) ");
              
                int rowsAffected = connection.Execute(sbInsert.ToString(), association);
                return Ok(rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpPut]
        public ActionResult UpdateAssociation(Association association)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbUpdate = new();
                sbUpdate.Append("UPDATE [Association] SET ");
                sbUpdate.Append("AssociationName = @AssociationName ");
                sbUpdate.Append("WHERE (AssociationId = @AssociationId)");

                int rowsAffected = connection.Execute(sbUpdate.ToString(), association);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpDelete("{associationid}")]
        public ActionResult DeleteAssociation(string associationid)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbDelete = new();
                sbDelete.Append("DELETE FROM [Association] ");
                sbDelete.Append(string.Format("WHERE (AssociationId = '{0}')", associationid));

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
