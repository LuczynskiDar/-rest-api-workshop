using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Songify.Simple.DAL;
using Songify.Simple.Dtos;
using Songify.Simple.Models;

namespace Songify.Simple.Controllers
{
    
    [ApiController]  
    [Route("/api/[controller]/")]
    public class ArtistsController : ControllerBase
    {
        private readonly IMapper _mapper;
        // private static readonly List<Artist> _artists = new();
        private readonly InMemoryRepository _repository;


        public ArtistsController(IMapper mapper, InMemoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        [HttpPost]
        public IActionResult Create(CreateArtistResource resource)
        {
            var artists = _mapper.Map<CreateArtistResource, Artist>(resource);
            
            _repository.Add(artists);
            // return Ok(new Artist {Id = 1, Name = "Metallica"});
            return Created($"api/artists/{artists.Id}", artists);
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var artist = _repository.Get(id);
            // var artist = _artists.FirstOrDefault(w => w.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(artist);
                
            }
        
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Remove(int id)
        {
            var artist = _repository.Get(id);
            // var artist = _artists.FirstOrDefault(w => w.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }
        
        [HttpPut]
        public IActionResult Update(UpdateArtistResource resource)
        {    
            var artist = _repository.Get(resource.Id);
            // var artist = _artists.FirstOrDefault(w => w.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            
            _repository.Delete(resource.Id);
            _repository.Add(_mapper.Map<UpdateArtistResource, Artist>(resource));
            return Ok();
        }
        
    }
}