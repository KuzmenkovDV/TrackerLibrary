using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
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
            model.Id =1;
            return model;
        }
    }
}
