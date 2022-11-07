using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{   /// <summary>
    /// Represents a person
    /// </summary>
    public class PersonModel
    {
        /// <summary>
        /// The unique identifier number for storage
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents first name of a person
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Represents family name of a person
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Represents email adress where email should be sent
        /// </summary>
        public string EmailAdress { get; set; }
        /// <summary>
        /// Represents contact number of the person
        /// </summary>
        public string CellphoneNumber { get; set; }
    }
}
