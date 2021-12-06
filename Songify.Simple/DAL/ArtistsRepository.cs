using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Songify.Simple.Dtos;
using Songify.Simple.Helpers;
using Songify.Simple.Models;

namespace Songify.Simple.DAL
{
    // da sie robic repozytorium generyczne ze snipeta
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
            // .Add  dodajemy encje do change trackera, encjia nie jest zinsertowana do db
            // w sql sa 3 sposoby ne gen id: 1. id column (db), 2. po stronie klienta (guid),
            // 3. Po stronie serwera, tzw sekwencje high-low
            // EF moze poprosic o paczke id z serwera, i dodawac id do entity, jak sie skoncza
            // to ponownie prosi baze o paczke, wtedy ma sens uzywanie .AddAsync(model)
            _context.Artists.Add(artist);

        }

        // Artist Get(int id)
        // {
        //    return _context.Artists.FirstOrDefault(w => w.Id == id);
        //    
        // }
        
        // public ValueTask<Artist> Get(int id)
        public Task<Artist> Get(int id)
        {
            return _context.Artists
                .FirstAsync(x => x.Id == id);
            // _context.Artists.Find(id)
            // _context.Artists.FindAsync(id)
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
            // zmiana stanu change trackera z EF
            _context.Entry(artist).State = EntityState.Modified;
        }

        public Task<PagedList<Artist>> GetArtists(ArtistResourceParameters parameters)
        // public Task<PagedList<Artist>> GetArtists(int pageNumber, int pageSize)
        // public Task<List<Artist>> GetArtists()
        {            
            // var collection = _context.Artists as IQueryable<Artist>;
            var collection = _context.Artists.AsQueryable();
            
            //Filtrowanie i przeszukiwanie
            if (parameters.IsActive.HasValue)
            {
                collection = collection.Where(x => x.IsActive == parameters.IsActive);
            }
            
            // Zewnetrzne przeszukiwanie
            // Mechanizm Full text search
            // Elastic search
            // Apache lucid/lucin
            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                var searchQuery = parameters.SearchQuery.Trim();
                collection = collection.Where(x => x.Name.Contains(searchQuery) || x.Origin.Contains(searchQuery));
            }
            
            return PagedList<Artist>.Create(collection, parameters.PageNumber, parameters.PageSize);

            // return PagedList<Artist>.Create(collection, pageNumber, pageSize);
            // return _context.Artists.ToListAsync();
        }
    }
}