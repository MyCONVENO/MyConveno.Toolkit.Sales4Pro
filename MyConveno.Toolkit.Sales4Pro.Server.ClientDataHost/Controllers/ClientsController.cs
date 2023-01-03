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

        [HttpGet("GetById")]
        //.../Clients/GetById?id=1
        public async Task<ActionResult<Client>> Get(string clientid = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[Client] ");
                sbSelect.Append("WHERE (ClientId = '" + clientid + "')");

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
    }
}
