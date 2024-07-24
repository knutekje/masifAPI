using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using masifAPI.Data;
using masifAPI.Model;

namespace masifAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly MasifContext _context;

        public IncidentController(MasifContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetIncidents()
        {
            return await _context.Incidents.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Incident>> GetIncident(long id)
        {
            var incident = await _context.Incidents.FindAsync(id);

            if (incident == null)
            {
                return NotFound();
            }

            return incident;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncident(long id, Incident incident)
        {
            if (id != incident.ReportID)
            {
                return BadRequest();
            }

            _context.Entry(incident).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(id))
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

        
        [HttpPost]
        public async Task<ActionResult<Incident>> PostIncident(Incident incident)
        {
            _context.Incidents.Add(incident);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IncidentExists(incident.ReportID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIncident", new { id = incident.ReportID }, incident);
        }

      
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncident(long id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncidentExists(long id)
        {
            return _context.Incidents.Any(e => e.ReportID == id);
        }
    }
}
