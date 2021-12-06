using System.Collections.Generic;
using System.Linq;
using Songify.Simple.Models;

namespace Songify.Simple.DAL
{
    // DAL: Data Access Layer
    
    public class InMemoryRepository
    {
        private List<Artist> Artists { get; set; }

        public InMemoryRepository()
        {
            Artists = new List<Artist>();
        }
        public void Add(Artist artist)
        {
            artist.Id = Artists.Count + 1;
            Artists.Add(artist);
        }

        public Artist Get(int id)
        {
            return Artists.Where(w => w.Id == id)
                .Select(s=>s).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var art = Artists.Single(s => s.Id == id);
            if (art != null)
            {
                Artists.Remove(art);

            }
            // .RemoveAt();
        }

        public void Update(Artist artist)
        {
            var art = 
                Artists.Where(w => w.Id == artist.Id).Select(s => s).FirstOrDefault();
            if (art != null)
            {
                art = artist;
            }
           
        }
    }
    
}