using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; }
        public static void InitialiseDataConnections(bool sqlStorage, bool txtStorage)
        { 
            if (sqlStorage)
            {
                //TODO - create SQL connection
            }

            if (txtStorage)
            {
                //TODO - create txt connection
            }
        }
    }
}
