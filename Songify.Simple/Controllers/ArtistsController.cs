using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Songify.Simple.DAL;
using Songify.Simple.Dtos;
using Songify.Simple.Models;

namespace Songify.Simple.Controllers
{
    
    [ApiController]  
    [Route("/api/[controller]/")]
    [Consumes(System.Net.Mime.MediaTypeNames.Application.Json)]
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
        /// <summary>
        /// Creates an Artist
        /// </summary>
        /// <remarks>
        /// Some more comments inside remark.
        ///
        /// POST:
        /// {
        ///     "key": "value"
        /// }
        /// </remarks>
        /// <param name="resource"></param>
        /// <response code="200">Creates an Artist</response>
        /// <response code="400">Bad request format</response>
        /// <response code="422">Bad data inside the entity</response>
        /// <returns></returns>
        [HttpPost]
        [Produces(System.Net.Mime.MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(CreateArtistResource resource)
        {
            var artists =  _mapper.Map<CreateArtistResource, Artist>(resource);
            
            _repository.Add(artists);
            await _repository.UnitOfWork.SaveChangesAsync();
            // return Ok(new Artist {Id = 1, Name = "Metallica"});
            return Created($"api/artists/{artists.Id}", artists);
        }
        
        // Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var artists = await _repository.GetArtists();
            return Ok(artists);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var artist = await _repository.Get(id);
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
        public async Task<IActionResult> Remove(int id)
        {
            // var artist = _repository.Get(id);
            var artist = await _repository.Get(id);
            // var artist = _artists.FirstOrDefault(w => w.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            _repository.Remove(id);
            await _repository.UnitOfWork.SaveChangesAsync();
            return NoContent();
        }
        
        // Update
        [HttpPut]
        public async Task<IActionResult> Update(UpdateArtistResource resource)
        {    
            var artist = await _repository.Get(resource.Id);
            // var artist = _artists.FirstOrDefault(w => w.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            
            // _repository.Remove(resource.Id);
            // _repository.Add(_mapper.Map<UpdateArtistResource, Artist>(resource));
            // _repository.Update(_mapper.Map<UpdateArtistResource, Artist>(resource));
            _mapper.Map(resource, artist);
            _repository.Update(artist);
            await _repository.UnitOfWork.SaveChangesAsync();
            return Ok();
        }
        
    }
}