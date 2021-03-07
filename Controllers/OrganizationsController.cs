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
    public class OrganizationsController : ControllerBase
    {
        //Dependency injected data context
        private readonly MedicalDataContext _context;

        // Logger extension
        private readonly ILogger _logger;

        /// <summary>
        /// OrganizationsController Constructor assigns data context via DI
        /// </summary>
        /// <param name="context">data context</param>
        public OrganizationsController(MedicalDataContext context, ILogger<OrganizationsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        ///  GET: api/Organizations
        /// Retrieves Organizations
        /// If no parameter is used it returns all Organizations
        /// Can filter results by name or type
        /// </summary>
        /// <param name="name">Name of organization</param>
        /// <param name="type">Type of organization</param>
        /// <returns>List of organizations</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations(string name, string type)
        {

            IQueryable<Organization> filteredList = _context.Organizations.AsQueryable();

            if (!String.IsNullOrEmpty(name)) { 
                filteredList = filteredList.Where(e => e.Name == name).AsQueryable();
            }

            if (!String.IsNullOrEmpty(type))
            {
                filteredList = filteredList.Where(e => e.Type == type).AsQueryable();
            }


            return await filteredList.ToListAsync();
        }

        
        /// <summary>
        /// GET: api/Organizations/5
        /// Retrieve a single Organization by Id
        /// </summary>
        /// <param name="id">Id of organization</param>
        /// <returns>A single organization</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganization(Guid id)
        {
            var organization = await _context.Organizations.FindAsync(id);

            if (organization == null)
            {
                var notFound = NotFound();
                _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                return notFound;
            }

            return organization;
        }

        
        /// <summary>
        /// PUT: api/Organizations/5
        /// Update an organization
        /// </summary>
        /// <param name="id">Id of organization</param>
        /// <param name="organization">Data for organization</param>
        /// <returns>No content and status code 204</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganization(Guid id, Organization organization)
        {
            if (id != organization.Id)
            {
                var badRequest = BadRequest();
                _logger.LogError($"Message: {badRequest}, Status code: {badRequest.StatusCode}, Id: {Guid.NewGuid()}");
                return badRequest;
            }

            _context.Entry(organization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
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
        /// POST: api/Organizations
        /// Create a new Organization
        /// </summary>
        /// <param name="organization">Data for organization</param>
        /// <returns>Action result and status code 201</returns>
        [HttpPost]
        public async Task<ActionResult<Organization>> PostOrganization(Organization organization)
        {
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganization", new { id = organization.Id }, organization);
        }

        /// <summary>
        /// DELETE: api/Organizations/5
        /// Delete an organization by Id
        /// </summary>
        /// <param name="id">Id for organization</param>
        /// <returns>No content + status code 204</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(Guid id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
            {
                var notFound = NotFound();
                _logger.LogError($"Message: {notFound}, Status code: {notFound.StatusCode}, Id: {Guid.NewGuid()}");
                return notFound;
            }

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizationExists(Guid id)
        {
            return _context.Organizations.Any(e => e.Id == id);
        }
    }
}
