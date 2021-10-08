using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCore.Domain;
using EfCore.Repository;

namespace EFCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmaController : ControllerBase
    {
        private readonly HeroAppContext _context;

        public ArmaController(HeroAppContext context)
        {
            _context = context;
        }

        // GET: api/Arma
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Arma>>> GetArmas()
        {
            return await _context.Armas.ToListAsync();
        }

        // GET: api/Arma/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Arma>> GetArma(int id)
        {
            var arma = await _context.Armas.FindAsync(id);

            if (arma == null)
            {
                return NotFound();
            }

            return arma;
        }

        // PUT: api/Arma/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArma(int id, Arma arma)
        {
            if (id != arma.Id)
            {
                return BadRequest();
            }

            _context.Entry(arma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArmaExists(id))
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

        // POST: api/Arma
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Arma>> PostArma(Arma arma)
        {
            _context.Armas.Add(arma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArma", new { id = arma.Id }, arma);
        }

        // DELETE: api/Arma/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArma(int id)
        {
            var arma = await _context.Armas.FindAsync(id);
            if (ArmaExists(id))
            {
                return NotFound();
            }

            _context.Armas.Remove(arma);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArmaExists(int id)
        {
            return _context.Armas.Any(e => e.Id == id);
        }
    }
}
