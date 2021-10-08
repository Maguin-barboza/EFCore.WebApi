using System.Linq;
using EFCore.Domain;
using EFCore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroiController: ControllerBase
    {
		private readonly HeroAppContext _context;

		public HeroiController(HeroAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_context.Herois);
        }

        [HttpPost]
        public ActionResult Post(Heroi heroi)
        {
            _context.Add(heroi);
            _context.SaveChanges();

            return Ok(heroi);
        }

        [HttpPut("{Id}")]
        public ActionResult Put(int Id, Heroi heroi)
        {
            var heroiAux = (from hero in _context.Herois
                            where hero.Id == Id
                            select hero).FirstOrDefault();

            heroiAux.Nome = heroi.Nome;
            //_context.Update(heroiAux);
            _context.SaveChanges();

            return Ok(_context.Herois.FirstOrDefault(x => x.Id == Id));
        }

        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            var heroiAux = (from hero in _context.Herois
                            where hero.Id == Id
                            select hero).FirstOrDefault();
            
            _context.Remove(heroiAux);
            _context.SaveChanges();

            return Ok("Registro deletado");
        }

    }
}