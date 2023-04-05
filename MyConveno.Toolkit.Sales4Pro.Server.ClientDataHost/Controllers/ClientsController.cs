using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IConfiguration config, ILogger<ClientsController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        //.../Clients/GetAll
        public ActionResult<IEnumerable<Client>> Get()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[Client]");

                IEnumerable<Client> clients = (IEnumerable<Client>)connection.Query<Client>(sbSelect.ToString(), null, null, true, 0);

                if (clients == null || !clients.Any())
                    return Ok(new List<Client>());
                else
                    return Ok(clients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("GetById")]
        //.../Clients/GetById?id=1
        public async Task<ActionResult<Client>> Get(string clientId = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[Client] ");
                sbSelect.Append("WHERE (ClientId = '" + clientId + "')");

                IEnumerable<Client> clients = await connection.QueryAsync<Client>(sbSelect.ToString());

                if (clients == null || !clients.Any())
                    return NotFound();
                else
                    return Ok(clients.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult UpdateClient(Client client)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbUpdate = new();
                sbUpdate.Append("UPDATE [Client] SET ");
                sbUpdate.Append("ClientName = @ClientName, ");
                sbUpdate.Append("Metadata = @Metadata ");
                sbUpdate.Append("WHERE (ClientId = @ClientId)");

                int rowsAffected = connection.Execute(sbUpdate.ToString(), client);
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
