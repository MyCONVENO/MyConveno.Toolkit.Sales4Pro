using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IConfiguration config, ILogger<UsersController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetByCredentials")]
        //.../Users/GetByCredentials?username=michael.coelsch@outlook.de?password=1234
        public ActionResult<User> Get(string username = "", string password = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[User] ");
                sbSelect.Append("WHERE (UserName =  '" + username + "') ");
                sbSelect.Append("AND (Password = '" + password + "')");

                IEnumerable<User> users = (List<User>)connection.Query<User>(sbSelect.ToString(), null, null, true, 0);

                if (users == null || !users.Any())
                    return NotFound();
                else
                    return Ok(users.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("GetByUserName")]
        //.../Users/GetByUserName?username=michael.coelsch@outlook.de
        public ActionResult<User> Get(string username = "")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[User] ");
                sbSelect.Append("WHERE (UserName =  '" + username + "')");

                IEnumerable<User> users = (List<User>)connection.Query<User>(sbSelect.ToString(), null, null, true, 0);

                if (users == null || !users.Any())
                    return NotFound();
                else
                    return Ok(users.FirstOrDefault());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("GetAll")]
        //.../Users/GetAll
        public ActionResult<IEnumerable<User>> Get()
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbSelect = new();
                sbSelect.Append("SELECT * FROM dbo.[User]");

                IEnumerable<User> users = (IEnumerable<User>)connection.Query<User>(sbSelect.ToString(), null, null, true, 0);

                if (users == null || !users.Any())
                    return NotFound();
                else
                    return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult<string> AddUser(User user)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbInsert = new();
                sbInsert.Append("INSERT INTO [User] (");
                sbInsert.Append("UserId, ");
                sbInsert.Append("UserName, ");
                sbInsert.Append("Password, ");
                sbInsert.Append("Metadata) ");
                sbInsert.Append("VALUES (");
                sbInsert.Append("@UserId, ");
                sbInsert.Append("@UserName, ");
                sbInsert.Append("@Password, ");
                sbInsert.Append("@Metadata) ");

                int rowsAffected = connection.Execute(sbInsert.ToString(), user);
                return Ok(rowsAffected);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpPut]
        public ActionResult UpdateUser(User user)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbUpdate = new();
                sbUpdate.Append("UPDATE [User] SET ");
                sbUpdate.Append("UserId = @UserId, ");
                sbUpdate.Append("UserName = @UserName, ");
                sbUpdate.Append("Password = @Password, ");
                sbUpdate.Append("Metadata = @Metadata ");
                sbUpdate.Append("WHERE (UserId = @UserId)");

                int rowsAffected = connection.Execute(sbUpdate.ToString(), user);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpDelete("{userid}")]
        public ActionResult DeleteUser(string userid)
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));

            try
            {
                StringBuilder sbDelete = new();
                sbDelete.Append("DELETE FROM [User] ");
                sbDelete.Append(string.Format("WHERE (UserId = '{0}')", userid));

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
