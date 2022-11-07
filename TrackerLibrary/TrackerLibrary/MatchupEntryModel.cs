using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    /// <summary>
    /// Represents one team in matchup
    /// </summary>
    public class MatchupEntryModel
    {
        /// <summary>
        /// The unique identifier number for storage
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents one team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for the particular team
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the matchup that should be done before
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }



    }
}
