using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvDotNetAPI.Models
{
    /// <summary>
    /// Provider Model with License number and Address information.
    /// Provider inherits from person.
    /// </summary>
    public class Provider : Person
    {
        //Provider's LicenseNumber - using an unsigned 32 bit int
        [Required]
        public UInt32 LicenseNumber { get; set; }

        //Address for the provider
        [Required]
        public string Address { get; set; }
    }
}
