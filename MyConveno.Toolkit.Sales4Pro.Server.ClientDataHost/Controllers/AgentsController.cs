using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(IConfiguration config, ILogger<AgentsController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetByAgentNumber")]
        //.../Agents/GetByAgentNumber?agentnumber=12345
        public ActionResult<Agent> Get(string agentnumber = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[Agent] ");
                sbSelect.Append("WHERE (AgentNumber =  '" + agentnumber + "')");

                IEnumerable<Agent> agents = (List<Agent>)connection.Query<Agent>(sbSelect.ToString(), null, null, true, 0);

                if (agents == null || !agents.Any())
                    return NotFound();
                else
                    return Ok(agents.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        //.../Agents/GetAll
        public ActionResult<IEnumerable<Agent>> Get()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[Agent]");

                IEnumerable<Agent> agents = (IEnumerable<Agent>)connection.Query<Agent>(sbSelect.ToString(), null, null, true, 0);

                if (agents == null || !agents.Any())
                    return Ok(new List<Agent>());
                else
                    return Ok(agents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<string> AddAgent(Agent agent)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbInsert = new();
                sbInsert.Append("INSERT INTO [Agent] (");
                sbInsert.Append("AgentNumber, ");
                sbInsert.Append("AgentName, ");
                sbInsert.Append("Metadata) ");
                sbInsert.Append("VALUES (");
                sbInsert.Append("@AgentNumber, ");
                sbInsert.Append("@AgentName, ");
                sbInsert.Append("@Metadata) ");

                int rowsAffected = connection.Execute(sbInsert.ToString(), agent);
                return Ok(rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpPut]
        public ActionResult UpdateAgent(Agent agent)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbUpdate = new();
                sbUpdate.Append("UPDATE [Agent] SET ");
                sbUpdate.Append("AgentNumber = @AgentNumber, ");
                sbUpdate.Append("AgentName = @AgentName, ");
              
                sbUpdate.Append("Metadata = @Metadata ");
                sbUpdate.Append("WHERE (AgentNumber = @AgentNumber)");

                int rowsAffected = connection.Execute(sbUpdate.ToString(), agent);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpDelete("{agentnumber}")]
        public ActionResult DeleteAgent(string agentnumber)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbDelete = new();
                sbDelete.Append("DELETE FROM [Agent] ");
                sbDelete.Append(string.Format("WHERE (AgentNumber = '{0}')", agentnumber));

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
