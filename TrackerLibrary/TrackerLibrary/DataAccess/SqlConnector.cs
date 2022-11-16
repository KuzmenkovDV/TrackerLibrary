using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        //TODO - create a method that actually saves the info
        /// <summary>
        /// Saves a new prize to a SQL database
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information dummy</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
            {
                connection.Open();
                return model;
            }
        }
    }
}
