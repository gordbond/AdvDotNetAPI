/* We, Gord Bond, student number 000786196, Tiago Franco de Goes Teles, student number 000786450, 
 * Olaoluwa Anthony-Egorp, student number 000776467, and Mitchell Aninyang, student number 000796709, 
 * certify that all code submitted is our own work; that we have not copied it from any other source. 
 * We also certify that we have not allowed our work to be copied by others.
 */
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvDotNetAPI.Models;

namespace AdvDotNetAPI.Models
{
    /// <summary>
    /// Database context for this API
    /// Co-ordinateswith between EF and the model
    /// </summary>
    public class MedicalDataContext : DbContext
    {

        public MedicalDataContext(DbContextOptions<MedicalDataContext> options)
            : base(options)
        {
        }

        //Immunization DBset - used to create queries for instances of Immunizations
        public DbSet<Immunization> Immunizations { get; set; }

        //Patient DBset - used to create queries for instances of Patient
        public DbSet<Patient> Patients { get; set; }

        //Patient DBset - used to create queries for instances of Patient
        public DbSet<Provider> Providers { get; set; }

        //Provider DBset - used to create queries for instances of Provider
        public DbSet<Organization> Organizations { get; set; }
    }
}
