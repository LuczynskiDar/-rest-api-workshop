using System.Collections.Generic;
using System.Threading.Tasks;
using Songify.Simple.Helpers;
using Songify.Simple.Models;

namespace Songify.Simple.DAL
{
    public interface IArtistRepository
    {

            public IUnitOfWork UnitOfWork { get; }
            public void Add(Artist model);
            public Task<Artist> Get(int id);
            public void Update(Artist artist);
            public void Remove(int id);
            // Task<PagedList<Artist>> GetArtists();
            Task<List<Artist>> GetArtists();
    }
}