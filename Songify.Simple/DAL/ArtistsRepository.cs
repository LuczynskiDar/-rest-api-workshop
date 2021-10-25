using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Songify.Simple.Helpers;
using Songify.Simple.Models;

namespace Songify.Simple.DAL
{
    public class ArtistsRepository: IRepository, IArtistRepository
    {
        private readonly SongifyDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ArtistsRepository(SongifyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public void Add(Artist artist)
        {
            _context.Artists.Add(artist);

        }

        // Artist Get(int id)
        // {
        //    return _context.Artists.FirstOrDefault(w => w.Id == id);
        //    
        // }
        
        public Task<Artist> Get(int id)
        {
            return _context.Artists
                .FirstAsync(x => x.Id == id);
        }

        // public void Delete(int id)
        // {
        //     var artist = Get(id);
        //     if (artist != null)
        //     {
        //         _context.Artists.Remove(artist);
        //
        //     }
        // }
        
        public void Remove(int id)
        {
            _context.Artists.Remove(new Artist {Id = id});
        }
        

        // void Update(Artist artist)
        // {
        //     var oldArtist = Get(artist.Id);
        //     if (oldArtist != null)
        //     {
        //         _context.Artists.Remove(oldArtist);
        //         _context.Artists.Add(artist);
        //
        //     }
        // }
        
        public void Update(Artist artist)
        {
            _context.Entry(artist).State = EntityState.Modified;
        }

        // public Task<PagedList<Artist>> GetArtists()
        public Task<List<Artist>> GetArtists()
        {
            // var colection = _context.Artists.AsQueryable();
            // var collection = _context.Artists as IQueryable<Artist>;
            return _context.Artists.ToListAsync();
        }
    }
}