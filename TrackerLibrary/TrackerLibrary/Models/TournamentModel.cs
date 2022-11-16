using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        /// <summary>
        /// The unique identifier number for storage
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tournament name that should be indicated
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Entry fee in USD
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// List of participating teams
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();

        /// <summary>
        /// List of prizes, if this list is acceptable
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// List of rounds between the teams
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();
    }
}
