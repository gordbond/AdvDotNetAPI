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
       
        //MedicalEntity DBset - used to create queries for instances of MedicalEntity
        public DbSet<MedicalEntity> MedicalEntities { get; set; }

        //Person DBset - used to create queries for instances of Person
        public DbSet<Person> People { get; set; }

        //Patient DBset - used to create queries for instances of Patient
        public DbSet<Patient> Patients { get; set; }

        //Patient DBset - used to create queries for instances of Patient
        public DbSet<Provider> Providers { get; set; }

        //Provider DBset - used to create queries for instances of Provider
        public DbSet<Organization> Organizations { get; set; }
    }
}
