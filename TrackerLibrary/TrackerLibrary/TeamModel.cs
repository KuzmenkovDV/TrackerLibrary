﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class TeamModel
    {
        /// <summary>
        /// The unique identifier number for storage
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List of persons, who are parts of the team
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();

        /// <summary>
        /// String name that should be indicated
        /// </summary>
        public string TeamName { get; set; }
    }
}
