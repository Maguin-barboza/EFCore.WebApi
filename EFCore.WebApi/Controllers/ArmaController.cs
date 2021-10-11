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
        private readonly IEFCoreRepository _repo;

        public ArmaController(IEFCoreRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Arma/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Arma>> Get(int id)
        {
            var arma = await _repo.GetArma(id);

            if (arma == null)
            {
                return NotFound();
            }

            return arma;
        }

        // POST: api/Arma
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Arma>> Post(Arma arma)
        {
            _repo.Add(arma);

            if (!(await _repo.SaveChangesAsync()))
                return BadRequest("Não foi possível fazer a inclusão do registro.");

            return Ok(arma);
        }

        // PUT: api/Arma/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Arma arma)
        {
            try
            {
                var armaAux = await _repo.GetArma(id);

                if (armaAux is null)
                    BadRequest("Registro não encontrado.");

                _repo.Update(arma);

                if (!(await _repo.SaveChangesAsync()))
                    return BadRequest("Não foi possível modificar Registro.");

                return Ok(arma);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Arma/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var arma = await _repo.GetArma(id);

            var armaAux = await _repo.GetArma(id);

            if (armaAux is null)
                NotFound("Registro não encontrado.");

            _repo.Remove(arma);

            if (!(await _repo.SaveChangesAsync()))
                return BadRequest("Não foi possível modificar Registro.");

            return Ok("Registro deletado com sucesso.");
        }
    }
}
