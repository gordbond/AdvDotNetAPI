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
