using EFCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Repository
{
    public interface IEFCoreRepository
    {
        void Add<T>(T TEntity) where T : class;
        void Update<T>(T TEntity) where T : class;
        void Remove<T>(T TEntity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<List<Heroi>> GetHerois();
        Task<Heroi> GetHeroiById(int Id);

        Task<List<Batalha>> GetBatalhas();
        Task<Batalha> GetBatalhaById(int Id);
    }
}
