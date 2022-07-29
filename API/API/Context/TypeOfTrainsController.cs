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
    public class TypeOfTrainsController : ControllerBase
    {
        private readonly RailWayContext _context;

        public TypeOfTrainsController(RailWayContext context)
        {
            _context = context;
        }

        // GET: api/TypeOfTrains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfTrain>>> GetTypeOfTrains()
        {
            return await _context.TypeOfTrains.ToListAsync();
        }

        // GET: api/TypeOfTrains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfTrain>> GetTypeOfTrain(int id)
        {
            var typeOfTrain = await _context.TypeOfTrains.FindAsync(id);

            if (typeOfTrain == null)
            {
                return NotFound();
            }

            return typeOfTrain;
        }

        // PUT: api/TypeOfTrains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeOfTrain(int id, TypeOfTrain typeOfTrain)
        {
            if (id != typeOfTrain.IdTypeOfTrain)
            {
                return BadRequest();
            }

            _context.Entry(typeOfTrain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOfTrainExists(id))
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

        // POST: api/TypeOfTrains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeOfTrain>> PostTypeOfTrain(TypeOfTrain typeOfTrain)
        {
            _context.TypeOfTrains.Add(typeOfTrain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeOfTrain", new { id = typeOfTrain.IdTypeOfTrain }, typeOfTrain);
        }

        // DELETE: api/TypeOfTrains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeOfTrain(int id)
        {
            var typeOfTrain = await _context.TypeOfTrains.FindAsync(id);
            if (typeOfTrain == null)
            {
                return NotFound();
            }

            _context.TypeOfTrains.Remove(typeOfTrain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeOfTrainExists(int id)
        {
            return _context.TypeOfTrains.Any(e => e.IdTypeOfTrain == id);
        }
    }
}
