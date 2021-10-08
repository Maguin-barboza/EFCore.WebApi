using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Repository
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly HeroAppContext _context;

        public EFCoreRepository(HeroAppContext context)
        {
            _context = context;
        }

        public void Add<T>(T TEntity) where T : class
        {
            _context.Add(TEntity);
        }

        public void Update<T>(T TEntity) where T : class
        {
            _context.Update(TEntity);
        }

        public void Remove<T>(T TEntity) where T : class
        {
            _context.Remove(TEntity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<Heroi>> GetHerois()
        {
            IQueryable<Heroi> query = _context.Herois
                                      .Include(h => h.Identidade)
                                      .Include(h => h.Armas)
                                      .Include(h => h.HeroisBatalhas)
                                      .ThenInclude(hb => hb.Batalha)
                                      .OrderBy(h => h.Id);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Heroi> GetHeroiById(int Id)
        {
            Heroi hero = await _context.Herois.Where(h => h.Id == Id)
                                              .Include(h => h.Identidade)
                                              .Include(h => h.Armas)
                                              .Include(h => h.HeroisBatalhas)
                                              .ThenInclude(hb => hb.Batalha)
                                              .FirstOrDefaultAsync();
            
            return hero;
        }

        public async Task<List<Batalha>> GetBatalhas()
        {
            IQueryable<Batalha> query = _context.Batalhas.Include(b => b.HeroisBatalhas)
                                                         .ThenInclude(bh => bh.Heroi);
            return await query.ToListAsync();
        }

        public async Task<Batalha> GetBatalhaById(int Id)
        {
            Batalha batalha = await _context.Batalhas.Where(b => b.Id == Id)
                                                     .Include(b => b.HeroisBatalhas)
                                                     .ThenInclude(hb => hb.Heroi)
                                                     .FirstOrDefaultAsync();
            return batalha;
        }
    }
}
