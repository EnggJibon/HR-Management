using System;
using System.Configuration;
using ERP.Utilities.Infrastructure.Contract;

namespace ERP.Utilities.Infrastructure
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string ConnectionString
        {
            get
            {
                var server = ConfigurationManager.ConnectionStrings["ERP"].ConnectionString;
                if(string.IsNullOrEmpty(server))
                    throw new Exception("A valid connection string needs to be set in the configuration file.");
                return server;
            }
        }
    }
}