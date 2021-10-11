using System.Linq;
using EFCore.Domain;
using EfCore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace EFCore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroiController: ControllerBase
    {
		private readonly IEFCoreRepository _repo;

        public HeroiController(IEFCoreRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _repo.GetHerois());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            var heroi = await _repo.GetHeroiById(Id);

            return Ok(heroi);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Heroi heroi)
        {
            _repo.Add(heroi);
            await _repo.SaveChangesAsync();

            return Ok(heroi);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Put(int Id, Heroi heroi)
        {
            try
            {
                var heroiAux = _repo.GetHeroiById(Id);


                _repo.Update(heroi);
                await _repo.SaveChangesAsync();

                return Ok(heroi);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                var heroi = await _repo.GetHeroiById(Id);

                _repo.Remove(heroi);
                await _repo.SaveChangesAsync();

                return Ok("Registro deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}