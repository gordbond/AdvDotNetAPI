using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdvDotNetAPI.Models;

namespace AdvDotNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImmunizationsController : ControllerBase
    {
        private readonly MedicalDataContext _context;

        public ImmunizationsController(MedicalDataContext context)
        {
            _context = context;
        }

        // GET: api/Immunizations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Immunization>>> GetImmunizations()
        {
            return await _context.Immunizations.ToListAsync();
        }

        // GET: api/Immunizations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Immunization>> GetImmunization(Guid id)
        {
            var immunization = await _context.Immunizations.FindAsync(id);

            if (immunization == null)
            {
                return NotFound();
            }

            return immunization;
        }

        // PUT: api/Immunizations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImmunization(Guid id, Immunization immunization)
        {
            if (id != immunization.Id)
            {
                return BadRequest();
            }

            _context.Entry(immunization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImmunizationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Immunizations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Immunization>> PostImmunization(Immunization immunization)
        {
            _context.Immunizations.Add(immunization);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImmunization", new { id = immunization.Id }, immunization);
        }

        // DELETE: api/Immunizations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImmunization(Guid id)
        {
            var immunization = await _context.Immunizations.FindAsync(id);
            if (immunization == null)
            {
                return NotFound();
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
