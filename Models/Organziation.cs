using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvDotNetAPI.Models
{
    /// <summary>
    /// Organization Model - Name, Type and Address
    /// Inherits from Medical Entity
    /// </summary>
    public class Organization : MedicalEntity
    {
        //Name of the organization with getter and setter methods
        [Required]
        public string Name { get; set; }

        //Type of the Organization with getter and setter methods
        [Required]
        [RegularExpression(@"\b(?i:Hospital|Clinic|Pharmacy)\b")]
        public string Type { get; set; }

        //Address of Organization with getter and setter methods
        [Required]
        
        public string Address { get; set; }

    }
}
