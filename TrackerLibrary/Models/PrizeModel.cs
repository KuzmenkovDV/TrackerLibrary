using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier number for storage
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Number of place, such as 1,2 etc
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// Text place name, such as First, Second, etc
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// Prize amount in USD, such as 100USD
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// Prize amount in percentages from 0 to 100
        /// </summary>
        public double PrizePercentage { get; set; }

        public PrizeModel()
        {

        }
        public PrizeModel(string placeNumber, string placeName, string prizeAmount, string prizePercentage)
        {
            
            PlaceName = placeName;
            PlaceNumber = int.Parse(placeNumber);
            PrizeAmount = decimal.Parse(prizeAmount);
            PrizePercentage = double.Parse(prizePercentage);
        }
    }
}
