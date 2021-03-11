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
    /// Patient Model with date of birth information.
    /// Patient inherits from person.
    /// </summary>
    public class Patient : Person
    {
        //DateTimeOffset Object with getter and setter methods
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

    }
}
