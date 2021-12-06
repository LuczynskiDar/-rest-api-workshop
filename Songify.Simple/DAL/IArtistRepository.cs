using System.Collections.Generic;
using System.Threading.Tasks;
using Songify.Simple.Dtos;
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
            public Task<PagedList<Artist>> GetArtists(ArtistResourceParameters parameters);
            // public Task<PagedList<Artist>> GetArtists(int pageNumber, int pageSize);
            // Task<List<Artist>> GetArtists();
    }
}