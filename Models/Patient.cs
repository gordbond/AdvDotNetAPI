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
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

    }
}
