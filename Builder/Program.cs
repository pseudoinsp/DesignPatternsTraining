using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class MsSqlConnectionString
    {
        public MsSqlConnectionString(string server, bool integratedSecurity)
        {
            Server = server;
            IntegratedSecurity = integratedSecurity;
        }

        public MsSqlConnectionString(string server, string database)
        {
            Server = server;
            Database = database;
        }

        public MsSqlConnectionString(string server, string userId, string password)
        {
            Server = server;
            UserId = userId;
            Password = password;
        }

        // ++++ konstruktorok halmaza
        // minden uj parameter ~megduplazza a ctor-ok szamat -> builder

        // ezek egy halmaza opcionalis, van ami akkor kell ha masik nincs, ... 
        public string Server { get; set; } // Server
        public string Database { get; set; } // Initial catalog | def: master
        public bool IntegratedSecurity { get; set; } // WindowsAuthentication | if true -> no user/pw
        public string UserId { get; set; }
        public string Password { get; set; }
    }



    public abstract class SqlConnectionStringBuilderBase
    {
        public string Server { get; private set; } // Server
        public string Database { get; private set; } // Initial catalog | def: master
        public bool IntegratedSecurity { get; private set; } // WindowsAuthentication | if true -> no user/pw
        public string UserId { get; private set; }
        public string Password { get; private set; }

        // ez az advanced verzio: ezek + private setterek
        public SqlConnectionStringBuilderBase WithServer(string server)
        {
            Server = server;
            return this;
        }

        public SqlConnectionStringBuilderBase WithDatabase(string database)
        {
            Database = database;
            return this;
        }

        public SqlConnectionStringBuilderBase WithIntegratedSecurity(bool integratedSecurity)
        {
            IntegratedSecurity = integratedSecurity;
            // null user credentials?
            return this;
        }

        public SqlConnectionStringBuilderBase WIthUserCredentials(string un, string pw)
        {
            UserId = un;
            Password = pw;
            // set integratedsec to false?
            return this;
        }

        public abstract string GetConnectionString();
    }

    public class MsSqlConnectionStringBuilder : SqlConnectionStringBuilderBase
    {
        public override string GetConnectionString()
        {
            if (string.IsNullOrWhiteSpace(Server))
                throw new InvalidOperationException("Server property not set");

            var sb = new StringBuilder();
            sb.Append($"Server={Server};");

            if (string.IsNullOrWhiteSpace(Database))
                sb.Append("Initial Catalog=master;");
            else
                sb.Append($"Initial Catalog={Database};");

            if (IntegratedSecurity)
            {
                if (!string.IsNullOrWhiteSpace(UserId) || !string.IsNullOrWhiteSpace(Password))
                    throw new InvalidOperationException("Cannot set user credentials for windows auth");
                sb.Append("IntegratedSecurity=True;");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(Password))
                    throw new InvalidOperationException("User credentials not set");
                sb.Append($"User ID={UserId};Password={Password}");
            }

            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilderBase builder = new MsSqlConnectionStringBuilder();

            // ez nehezkes, nem ad annyira domain helpet
            //builder.UserId = "asd";

            string connectionString = builder.WithServer("localhost")
                .WithDatabase("Products")
                .WithIntegratedSecurity(true)
                .GetConnectionString();

            Console.WriteLine(connectionString);

            Console.ReadKey();
        }
    }
}
