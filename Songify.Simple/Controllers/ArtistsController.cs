using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Songify.Simple.DAL;
using Songify.Simple.Dtos;
using Songify.Simple.Helpers;
using Songify.Simple.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
        
        /// <summary>
        /// Get a list of Artist 
        /// </summary>
        /// <remarks>
        /// Some more comment inside remarks tag
        /// </remarks>
        /// <returns>List of artists</returns>
        /// <response code="200">Returns existing artists entity</response>
        [HttpGet]
        [HttpHead]
        [Produces(System.Net.Mime.MediaTypeNames.Application.Json)]
        // [ProducesResponseType(typeof(PagedList<Artist>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<IActionResult> Get(int pageNumber, int pageSize)
        public async Task<IActionResult> Get([FromQuery]int pageNumber, [FromQuery]int pageSize)
        {
            var artists = await _repository.GetArtists(pageNumber, pageSize);
            var paginationMetaData = new
            {
                totalCount = artists.TotalCount,
                totalPages = artists.TotalPages,
                currentPage = artists.CurrentPage,
                pageSize = artists.PageSize
            };
            
            // Naglowki z b3 standard tracing, poprzedza sie je 'X-'
            // openzipkin/3-propagation
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            
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