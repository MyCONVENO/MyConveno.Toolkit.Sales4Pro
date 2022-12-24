//using Dapper;
//using System.Data;
//using System.Data.SqlClient;
//using System.Text;

//namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
//{
//    public class DataService : IDataService
//    {
//        private readonly IConfiguration _config;

//        //private static string? sqlConnection = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_ClientDB");

//        public DataService(IConfiguration config)
//        {
//            _config = config;
//        }

//        #region Client

//        public Client GetClientById(string clientid = "")
//        {
//            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

//            try
//            {
//                StringBuilder sbSelect = new();
//                sbSelect.Append("SELECT * FROM dbo.[Client] ");
//                sbSelect.Append("WHERE (ClientId = '" + clientid + "')");

//                List<Client> clients = new();

//                clients = (List<Client>)connection.Query<Client>(sbSelect.ToString(), null, null, true, 0);

//                if (clients == null || !clients.Any())
//                    return null;
//                else
//                    return clients.FirstOrDefault();
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        #endregion

//        #region User

//        public User GetUserByCredentials(string username = "", string password = "", string connectionId = "SQLAZURECONNSTR_ClientDB")
//        {
//            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

//            try
//            {
//                StringBuilder sbSelect = new();
//                sbSelect.Append("SELECT * FROM dbo.[User] ");
//                sbSelect.Append("WHERE (UserName =  '" + username + "') ");
//                sbSelect.Append("AND (Password = '" + password + "')");

//                List<User> users = new();

//                users = (List<User>)connection.Query<User>(sbSelect.ToString(), null, null, true, 0);

//                if (users == null || !users.Any())
//                    return null;
//                else
//                    return users.FirstOrDefault();
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public User GetUserByUserName(string username = "", string connectionId = "SQLAZURECONNSTR_ClientDB")
//        {
//            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

//            try
//            {
//                StringBuilder sbSelect = new();
//                sbSelect.Append("SELECT * FROM dbo.[User] ");
//                sbSelect.Append("WHERE (UserName =  '" + username + "')");

//                List<User> users = new();


//                users = (List<User>)connection.Query<User>(sbSelect.ToString(), null, null, true, 0);

//                if (users == null || !users.Any())
//                    return null;
//                else
//                    return users.FirstOrDefault();
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public IEnumerable<User> GetAllUsers(string connectionId = "SQLAZURECONNSTR_ClientDB")
//        {
//            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

//            try
//            {
//                StringBuilder sbSelect = new();
//                sbSelect.Append("SELECT * FROM dbo.[User]");

//                IEnumerable<User> users;

//                users = (IEnumerable<User>)connection.Query<User>(sbSelect.ToString(), null, null, true, 0);

//                if (users == null || !users.Any())
//                    return null;
//                else
//                    return users;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        public int AddUser(User user, string connectionId = "SQLAZURECONNSTR_ClientDB")
//        {
//            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

//            try
//            {
//                StringBuilder sbInsert = new();
//                sbInsert.Append("INSERT INTO [User] (");
//                sbInsert.Append("UserId, ");
//                sbInsert.Append("UserName, ");
//                sbInsert.Append("Password, ");
//                sbInsert.Append("Metadata) ");
//                sbInsert.Append("VALUES (");
//                sbInsert.Append("@UserId, ");
//                sbInsert.Append("@UserName, ");
//                sbInsert.Append("@Password, ");
//                sbInsert.Append("@Metadata) ");

//                int rowsAffected = connection.Execute(sbInsert.ToString(), user);
//                return rowsAffected;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                return 0;
//            }
//        }

//        public int UpdateUser(User user, string connectionId = "SQLAZURECONNSTR_ClientDB")
//        {
//            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

//            try
//            {
//                StringBuilder sbUpdate = new();
//                sbUpdate.Append("UPDATE [User] SET ");
//                sbUpdate.Append("UserId = @UserId, ");
//                sbUpdate.Append("UserName = @UserName, ");
//                sbUpdate.Append("Password = @Password, ");
//                sbUpdate.Append("Metadata = @Metadata ");
//                sbUpdate.Append("WHERE (UserId = @UserId)");

//                int rowsAffected = connection.Execute(sbUpdate.ToString(), user);
//                return rowsAffected;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                return 0;
//            }
//        }

//        public int DeleteUser(string userid, string connectionId = "SQLAZURECONNSTR_ClientDB")
//        {
//            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

//            try
//            {
//                StringBuilder sbDelete = new();
//                sbDelete.Append("DELETE FROM [User] ");
//                sbDelete.Append(string.Format("WHERE (UserId = '{0}')", userid));

//                int rowsAffected = connection.Execute(sbDelete.ToString());
//                return rowsAffected;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                return 0;
//            }
//        }

//        #endregion

//    }
//}
