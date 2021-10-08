using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using EFCore.Repository;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly HeroAppContext _context;

        public BatalhaController(HeroAppContext context)
        {
            _context = context;
        }

        // GET: api/<BatalhaController>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_context.Batalhas.ToList());
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var Batalha = _context.Batalhas.FirstOrDefault(b => b.Id == id);

            return Ok(Batalha);
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public ActionResult Post(Batalha batalha)
        {
            try
            {
                _context.Add(batalha);
                _context.SaveChanges();

                return Ok(batalha);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Batalha batalha)
        {
            try
            {
                var batalhaAux = _context.Batalhas.AsNoTracking().FirstOrDefault(b => b.Id == id);

                if (batalhaAux is null)
                    return BadRequest("Registro não encontrado.");

                _context.Update(batalha);
                _context.SaveChanges();

                return Ok(batalha);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var batalhaAux = _context.Batalhas.FirstOrDefault(b => b.Id == id);

                if (batalhaAux is null)
                    return BadRequest("Registro não encontrado");

                _context.Remove(batalhaAux);
                _context.SaveChanges();

                return Ok(batalhaAux);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
