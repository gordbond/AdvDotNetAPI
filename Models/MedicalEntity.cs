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
            this.CreationTime = DateTime.UtcNow;
        }
    }
}
