using System;
using System.Linq;
using Songify.Simple.Models;

namespace Songify.Simple.DAL
{
    public class ArtistsRepository: IRepository
    {
        private readonly SongifyDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ArtistsRepository(SongifyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        
        void Add(Artist artist)
        {
            _context.Artists.Add(artist);

        }

        Artist Get(int id)
        {
           return _context.Artists.FirstOrDefault(w => w.Id == id);
        }

        void Delete(int id)
        {
            var artist = Get(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);

            }
        }

        void Update(Artist artist)
        {
            var oldArtist = Get(artist.Id);
            if (oldArtist != null)
            {
                _context.Artists.Remove(oldArtist);
                _context.Artists.Add(artist);

            }
        }
    }
}