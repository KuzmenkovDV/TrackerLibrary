using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }
        public static void InitialiseDataConnections(DataConnectionType dct)
        {
            switch (dct)
            {
                case DataConnectionType.Sql:
                    {                        
                        SqlConnector sql = new SqlConnector();
                        Connection = sql;
                        break;
                    }

                case DataConnectionType.TextFile:
                    {
                        TextConnector txt = new TextConnector();
                        Connection = txt;
                        break;
                    }
            }
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
