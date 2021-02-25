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

      

            //Official name for the Immunization
            [Required]
            [StringLength(128)]
            public string OfficialName { get; set; }

            //Trademark name for Immunization
            [StringLength(128)]
            public string TradeName { get; set; }

            //Lot Number
            [Required]
            [StringLength(255)]
            public string LotNumber { get; set; }

            //Expiration date 
            [Required]
            public DateTimeOffset Expiration { get; set; }

            //Date updated
            public DateTimeOffset UpdatedTime { get; set; }
        }
    
}
