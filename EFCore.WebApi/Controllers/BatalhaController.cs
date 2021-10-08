using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using EfCore.Repository;
using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhaController : ControllerBase
    {
        private readonly IEFCoreRepository _repository;

        public BatalhaController(IEFCoreRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<BatalhaController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetBatalhas());
        }

        // GET api/<BatalhaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_repository.GetBatalhaById(id));
        }

        // POST api/<BatalhaController>
        [HttpPost]
        public async Task<IActionResult> Post(Batalha batalha)
        {
            try
            {
                _repository.Add(batalha);
                if (!(await _repository.SaveChangesAsync()))
                    return BadRequest("Não foi possível incluir.");

                return Ok(batalha);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BatalhaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha batalha)
        {
            try
            {
                var batalhaAux = await _repository.GetBatalhaById(id);

                if (batalhaAux is null)
                    return BadRequest("Não foi possível concluir a alteração.");

                _repository.Update(batalha);
                
                if (!(await _repository.SaveChangesAsync()))
                    return BadRequest("Não foi possível incluir.");

                return Ok(batalha);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BatalhaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var batalhaAux = await _repository.GetBatalhaById(id);

                if (batalhaAux is null)
                    return BadRequest("Registro não encontrado");

                _repository.Remove(batalhaAux);
                await _repository.SaveChangesAsync();

                return Ok(batalhaAux);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
