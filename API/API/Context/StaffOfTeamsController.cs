using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Context
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffOfTeamsController : ControllerBase
    {
        private readonly RailWayContext _context;

        public StaffOfTeamsController(RailWayContext context)
        {
            _context = context;
        }

        // GET: api/StaffOfTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffOfTeam>>> GetStaffOfTeams()
        {
            return await _context.StaffOfTeams.ToListAsync();
        }

        // GET: api/StaffOfTeams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffOfTeam>> GetStaffOfTeam(int id)
        {
            var staffOfTeam = await _context.StaffOfTeams.FindAsync(id);

            if (staffOfTeam == null)
            {
                return NotFound();
            }

            return staffOfTeam;
        }

        // PUT: api/StaffOfTeams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffOfTeam(int id, StaffOfTeam staffOfTeam)
        {
            if (id != staffOfTeam.IdSot)
            {
                return BadRequest();
            }

            _context.Entry(staffOfTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffOfTeamExists(id))
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

        // POST: api/StaffOfTeams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StaffOfTeam>> PostStaffOfTeam(StaffOfTeam staffOfTeam)
        {
            _context.StaffOfTeams.Add(staffOfTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffOfTeam", new { id = staffOfTeam.IdSot }, staffOfTeam);
        }

        // DELETE: api/StaffOfTeams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffOfTeam(int id)
        {
            var staffOfTeam = await _context.StaffOfTeams.FindAsync(id);
            if (staffOfTeam == null)
            {
                return NotFound();
            }

            _context.StaffOfTeams.Remove(staffOfTeam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StaffOfTeamExists(int id)
        {
            return _context.StaffOfTeams.Any(e => e.IdSot == id);
        }
    }
}
