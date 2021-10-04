using Microsoft.Data.SqlClient;

namespace AutomatedFinances.Infrastructure.Data
{
    internal sealed class DatabaseSettings
    {
        public string Server { get; set; }

        public string Password {get; set;}

        public string Database { get; set; }

        public string UserName { get; set; }

        public int CmdTimeout { get; set; } = 30;

        public string ConnectionString => new SqlConnectionStringBuilder()
        {
            DataSource = Server,
            InitialCatalog = Database,
            UserID = UserName,
            Password = Password,
        }.ConnectionString;
    }
}
