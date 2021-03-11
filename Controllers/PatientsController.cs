using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvDotNetAPI.Models;
using Microsoft.Extensions.Logging;

namespace AdvDotNetAPI.Controllers
{
    /// <summary>
    /// Controller for the Patient API
    /// 
    /// Get - Allows for firstName, lastName and dateOfBirth parameters
    ///     - Can also get by a specific Id
    /// Put - Updates a specific patient (id's must match)
    /// Post - Create a new patient
    /// Delete - Deletes a patient by specific id
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        //Dependency injected data context
        private readonly MedicalDataContext _context;

        // Logger extension
        private readonly ILogger _logger;


        /// <summary>
        /// PatientsController Constructor assigns data context via DI
        /// </summary>
        /// <param name="context">data context</param>
        public PatientsController(MedicalDataContext context, ILogger<PatientsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// GET: api/Patients
        /// No parameters gets all patients
        /// Or use firstName, lastName or dataOfBirth to get patients by a specific filter
        /// </summary>
        /// <param name="firstName">Patient's first name</param>
        /// <param name="lastName">Patient's Last name</param>
        /// <param name="dateOfBirth">Patient's Date of birth</param>
        /// <returns>List of patient data</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients(string firstName = null, string lastName = null, string dateOfBirth = null)
        {
            
            IQueryable<Patient> filteredList = _context.Patients.AsQueryable();

            if (!String.IsNullOrEmpty(firstName))
            {
                filteredList = filteredList.Where(e => e.FirstName == firstName).AsQueryable();
            }

            if (!String.IsNullOrEmpty(lastName))
            {
                filteredList = filteredList.Where(e => e.LastName == lastName).AsQueryable();
            }

            if (!String.IsNullOrEmpty(dateOfBirth))
            {
                DateTimeOffset dateForQuery = DateTimeOffset.Parse(dateOfBirth);
                filteredList = filteredList.Where(e => e.DateOfBirth == dateForQuery).AsQueryable();
            }

            return await filteredList.ToListAsync();
           

        }




        /// <summary>
        /// GET: api/Patients/5
        /// </summary>
        /// <param name="id">Id of patient to get</param>
        /// <returns>Specific patient info based on Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                var notFound = NotFound();
                _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                return notFound;
            }

            return patient;
        }


        /// <summary>
        /// PUT: api/Patients/5
        /// </summary>
        /// <param name="id">Id of patient to update</param>
        /// <param name="patient">patient data to update</param>
        /// <returns>No content - status code representing no content</returns>
        [HttpPut("{id}")]
        [Consumes("application/xml")]
        public async Task<IActionResult> PutPatient(Guid id, Patient patient)
        {
            if (id != patient.Id)
            {
                Console.Write("issue with the ID. Given ID: " + id + " Patient ID: " + patient.Id);
                var badRequest = BadRequest();
                _logger.LogError($"Message: {badRequest}, Status code: {badRequest.StatusCode}, Id: {Guid.NewGuid()}");
                return badRequest;
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    var notFound = NotFound();
                    _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                    return notFound;
                }
                else
                {
                    var badRequest = BadRequest();
                    _logger.LogError($"Message: {badRequest}, Status code: {badRequest.StatusCode}, Id: {Guid.NewGuid()}");
                    throw;
                }
            }

            return NoContent();
        }

       
        /// <summary>
        /// POST: api/Patients
        /// </summary>
        /// <param name="patient">Patient to create</param>
        /// <returns>An action result/status code</returns>
        [HttpPost]
        [Consumes("application/xml")]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }


        /// <summary>
        /// DELETE: api/Patients/5
        /// </summary>
        /// <param name="id">ID of patient to delete</param>
        /// <returns>An action result/status code</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                var notFound = NotFound();
                _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                return notFound;
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientExists(Guid id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }

    
}
