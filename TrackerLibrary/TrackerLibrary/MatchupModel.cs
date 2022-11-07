using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    /// <summary>
    /// Represents one match in the tournament
    /// </summary>
    public class MatchupModel
    {
        /// <summary>
        /// The unique identifier number for storage
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The set of teams that were involved in this match
        /// </summary>
        public List<MatchupEntryModel> Entrries { get; set } = new List<MatchupEntryModel>();
        /// <summary>
        /// The team that won the match
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// Which round this match is a part of
        /// </summary>
        public int  MatchupRound { get; set; }
    }
}
