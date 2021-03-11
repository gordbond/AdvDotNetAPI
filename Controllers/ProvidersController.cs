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
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        //Dependency injected data context
        private readonly MedicalDataContext _context;

        // Logger extension
        private readonly ILogger _logger;

        /// <summary>
        /// ProvidersController Constructor assigns data context via DI
        /// </summary>
        /// <param name="context">data context</param>
        public ProvidersController(MedicalDataContext context, ILogger<ProvidersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// GET: api/provider
        /// No parameters gets all Providers
        /// Or use firstName, lastName or licenseNumber to get Provider by a specific filter
        /// </summary>
        /// <param name="firstName">Provider's first name</param>
        /// <param name="lastName">Provider's Last name</param>
        /// <param name="licenseNumber">Provider's licenseNumber</param>
        /// <returns>List of Provider data</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProviders(string firstName, string lastName, string licenseNumber)
        {
            IQueryable<Provider> filteredList = _context.Providers.AsQueryable();

            if (!String.IsNullOrEmpty(firstName))
            {
                filteredList = filteredList.Where(e => e.FirstName == firstName).AsQueryable();
            }

            if (!String.IsNullOrEmpty(lastName))
            {
                filteredList = filteredList.Where(e => e.LastName == lastName).AsQueryable();
            }

            if (!String.IsNullOrEmpty(licenseNumber))
            {
                uint licenseNumberForQuery = uint.Parse(licenseNumber);
                filteredList = filteredList.Where(e => e.LicenseNumber == licenseNumberForQuery).AsQueryable();
            }

            return await filteredList.ToListAsync();
        }


        /// <summary>
        /// GET: api/Providers/5
        /// </summary>
        /// <param name="id">Id of Provider to get</param>
        /// <returns>Specific Provider info based on Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(Guid id)
        {
            var provider = await _context.Providers.FindAsync(id);

            if (provider == null)
            {
                var notFound = NotFound();
                _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                return notFound;
            }

            return provider;
        }

        // 
        /// <summary>
        /// PUT: api/Providers/5
        /// </summary>
        /// <param name="id">Id of provider to update</param>
        /// <param name="provider">provider data to update</param>
        /// <returns>No content - status code representing no content</returns>
        [HttpPut("{id}")]
        [Consumes("application/xml")]
        public async Task<IActionResult> PutProvider(Guid id, Provider provider)
        {
            if (id != provider.Id)
            {
                var badRequest = BadRequest();
                _logger.LogError($"Message: {badRequest}, Status code: {badRequest.StatusCode}, Id: {Guid.NewGuid()}");
                return badRequest;
            }

            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
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
        ///  POST: api/Providers
        ///  Create a Provider 
        /// </summary>
        /// <param name="provider">provider data to create</param>
        /// <returns>ActionResult - status code 201</returns>
        [HttpPost]
        [Consumes("application/xml")]
        public async Task<ActionResult<Provider>> PostProvider(Provider provider)
        {
            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvider", new { id = provider.Id }, provider);
        }

        /// <summary>
        /// DELETE: api/Providers/5
        /// </summary>
        /// <param name="id">Id for provide to delete</param>
        /// <returns>No content - status code 204</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(Guid id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider == null)
            {
                var notFound = NotFound();
                _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                return notFound;
            }

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProviderExists(Guid id)
        {
            return _context.Providers.Any(e => e.Id == id);
        }
    }
}
