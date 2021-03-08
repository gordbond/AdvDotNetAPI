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
    /// Controller for Immunizations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImmunizationsController : ControllerBase
    {
        //Dependency injected data context
        private readonly MedicalDataContext _context;

        // Logger extension
        private readonly ILogger _logger;

        /// <summary>
        /// ImmunizationController - assigns data context via DI
        /// </summary>
        /// <param name="context">data context</param>
        public ImmunizationsController(MedicalDataContext context, ILogger<ImmunizationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

      
        /// <summary>
        /// GET: api/Immunizations
        /// No parameters retrieves all immunizations
        /// Or use creationTime, officialName or tradeName or lotNumber to get immunizations by a specific filter
        /// </summary>
        /// <param name="creationTime">Time Immunization created</param>
        /// <param name="officialName">Offical Immunization Name</param>
        /// <param name="tradeName">Trade Name for Immunization</param>
        /// <param name="lotNumber">Lot number for Immunization</param>
        /// <returns>List of Immunizations</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Immunization>>> GetImmunizations(string creationTime, string officialName, string tradeName, string lotNumber)
        {
            IQueryable<Immunization> filteredList = _context.Immunizations.AsQueryable();

            if (!String.IsNullOrEmpty(creationTime))
            {
                DateTimeOffset dateForQuery = DateTimeOffset.Parse(creationTime);
                filteredList = filteredList.Where(e => e.CreationTime == dateForQuery).AsQueryable();
            }

            if (!String.IsNullOrEmpty(officialName)) 
            {
                filteredList = filteredList.Where(e => e.OfficialName == officialName).AsQueryable();
            }

            if (!String.IsNullOrEmpty(tradeName))
            {
                filteredList = filteredList.Where(e => e.TradeName == tradeName).AsQueryable();
            }

            if (!String.IsNullOrEmpty(lotNumber))
            {
                filteredList = filteredList.Where(e => e.LotNumber == lotNumber).AsQueryable();
            }

            return await filteredList.ToListAsync();

        }

        
        /// <summary>
        /// GET: api/Immunizations/5
        /// Retrieve Immunizaton by Id
        /// </summary>
        /// <param name="id">Id of Immunization</param>
        /// <returns>A single Immunization</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Immunization>> GetImmunization(Guid id)
        {
            var immunization = await _context.Immunizations.FindAsync(id);

            if (immunization == null)
            {
                var notFound = NotFound();
                _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                return notFound;
            }

            return immunization;
        }

        
        /// <summary>
        /// PUT: api/Immunizations/5
        /// Updates a Immunization record
        /// Automatically updates UpdatedTime
        /// </summary>
        /// <param name="id">Id of Immunization</param>
        /// <param name="immunization">Immunization Data to update</param>
        /// <returns>No content + 204 status code</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImmunization(Guid id, Immunization immunization)
        {
            if (id != immunization.Id)
            {
                var badRequest = BadRequest(ModelState);
                _logger.LogError($"Message: {badRequest}, Status code: {badRequest.StatusCode}, Id: {Guid.NewGuid()}");
                return badRequest;
            }
            //Auto generate the UpdatedTime
            immunization.UpdatedTime = DateTimeOffset.UtcNow;

            _context.Entry(immunization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImmunizationExists(id))
                {
                    var notFound = NotFound();
                    _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                    return notFound;
                }
                else
                {
                    var badRequest = BadRequest(ModelState);
                    _logger.LogError($"Message: {badRequest}, Status code: {badRequest.StatusCode}, Id: {Guid.NewGuid()}");
                    throw;
                }
            }

            return NoContent();
        }

        
        /// <summary>
        /// POST: api/Immunizations
        /// Creates a new Immunization and updated "UpdatedTime"
        /// </summary>
        /// <param name="immunization">Data for Immunization record</param>
        /// <returns>ActionResult and status code</returns>
        [HttpPost]
        public async Task<ActionResult<Immunization>> PostImmunization(Immunization immunization)
        {
            //Auto generate the UpdatedTime
            immunization.UpdatedTime = DateTimeOffset.UtcNow;

            _context.Immunizations.Add(immunization);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImmunization", new { id = immunization.Id }, immunization);
        }


        /// <summary>
        /// DELETE: api/Immunizations/5
        /// Deletes a Immunization record by specific Id
        /// </summary>
        /// <param name="id">Id of immunization</param>
        /// <returns>No content + status code</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImmunization(Guid id)
        {
            var immunization = await _context.Immunizations.FindAsync(id);
            if (immunization == null)
            {
                var notFound = NotFound();
                _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                return notFound;
            }

            _context.Immunizations.Remove(immunization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImmunizationExists(Guid id)
        {
            return _context.Immunizations.Any(e => e.Id == id);
        }
    }
}
