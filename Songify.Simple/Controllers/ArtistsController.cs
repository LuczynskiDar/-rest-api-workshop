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
        private readonly IArtistRepository _repository;
        // private static readonly List<Artist> _artists = new();
        // private readonly InMemoryRepository _repository;


        // public ArtistsController(IMapper mapper, InMemoryRepository repository)
        public ArtistsController(IMapper mapper, IArtistRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        // Create
        [HttpPost]
        public IActionResult Create(CreateArtistResource resource)
        {
            var artists = _mapper.Map<CreateArtistResource, Artist>(resource);
            
            _repository.Add(artists);
            // return Ok(new Artist {Id = 1, Name = "Metallica"});
            return Created($"api/artists/{artists.Id}", artists);
        }
        
        // Get
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
        
        // Remove
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
            _repository.Remove(id);
            return NoContent();
        }
        
        // Update
        [HttpPut]
        public IActionResult Update(UpdateArtistResource resource)
        {    
            var artist = _repository.Get(resource.Id);
            // var artist = _artists.FirstOrDefault(w => w.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            
            // _repository.Remove(resource.Id);
            // _repository.Add(_mapper.Map<UpdateArtistResource, Artist>(resource));
            
            _repository.Update(_mapper.Map<UpdateArtistResource, Artist>(resource));
            return Ok();
        }
        
    }
}