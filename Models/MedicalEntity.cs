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
    /// Medical entity is the root class with ID and creation time data.
    /// Automatically generates an Id and creation date
    /// </summary>
    public class MedicalEntity
    {
        //Primary Key - Id for each entity in db
        [Key]
        [Required]
        public Guid Id { get; set; }

        //Date the entity was created
        [Required]
        public DateTimeOffset CreationTime { get; set; }

        public MedicalEntity()
        {
            this.CreationTime = DateTimeOffset.UtcNow;
        }
    }
}
