using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvDotNetAPI.Models
{
    /// <summary>
    /// Person model with first and last name.
    /// Person inherits from medical entity.
    /// </summary>
    public class Person : MedicalEntity
    {
        //FirstName string variable with getter and setter methods
        [Required]
        public string FirstName { get; set; }

        //LastName string variable with getter and setter methods
        [Required]
        public string LastName { get; set; }
    }
}
