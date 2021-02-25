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
        //Name of the organization
        [Required]
        public string Name { get; set; }

        //THIS STILL NEEDS TO BE UPDATED -> MUST BE ONE OF: Hospital, Clinic or Pharmacy
        //Type of the Organization
        [Required]
        public string Type { get; set; }

        //Address of Organization
        [Required]
        public string Address { get; set; }

    }
}
