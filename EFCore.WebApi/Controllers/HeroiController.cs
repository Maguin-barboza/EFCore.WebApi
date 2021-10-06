using System.Linq;

using EFCore.WebApi.Data;
using EFCore.WebApi.Models;
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
        public ActionResult Post()
        {
            _context.AddRange(new Heroi() {Nome = "Batman"},
                              new Heroi() {Nome = "Robin"},
                              new Heroi() {Nome = "Arqueiro Verde"},
                              new Heroi() {Nome = "Black Cat"}
                              );
            
            _context.SaveChanges();

            return Ok("Todos Gravados com sucesso!");
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