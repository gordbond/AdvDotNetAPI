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
