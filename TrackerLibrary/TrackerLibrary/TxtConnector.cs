using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class TxtConnector : IDataConnection
    {
        //TODO - create an actual method
        public PrizeModel CreatePrize(PrizeModel model)
        {
            
            model.Id = 1;

            return model;  
        }
    }
}
