/* We, Gord Bond, student number 000786196, Tiago Franco de Goes Teles, student number 000786450, 
 * Olaoluwa Anthony-Egorp, student number 000776467, and Mitchell Aninyang, student number 000796709, 
 * certify that all code submitted is our own work; that we have not copied it from any other source. 
 * We also certify that we have not allowed our work to be copied by others.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvDotNetAPI.Models
{
    
        /// <summary>
        /// Immunization with Official Name, Trademark, Lot Number, Expiration date and updated time
        /// Inherit from Medical entity
        /// </summary>
        public class Immunization : MedicalEntity
        {



        //Official name for the Immunization with getter and setter methods
        [Required]
            [StringLength(128)]
            public string OfficialName { get; set; }

        //Trademark name for Immunization with getter and setter methods
        [StringLength(128)]
            public string TradeName { get; set; }

        //Lot Number with getter and setter methods
        [Required]
            [StringLength(255)]
            public string LotNumber { get; set; }

        //Expiration date  DateTimeOffset with getter and setter methods
        [Required]
            public DateTimeOffset Expiration { get; set; }

            //Date updated with getter and setter methods
            public DateTimeOffset UpdatedTime { get; set; }
        }
    
}
