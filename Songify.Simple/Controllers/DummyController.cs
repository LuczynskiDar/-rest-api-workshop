using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Songify.Simple.DAL;
using Songify.Simple.Models;

namespace Songify.Simple.Controllers
{
    // [Route("/api/[controller]/")]
    // there can be special variables
    // such as:
    // [controller], [action], [area]
    // f.ex [Route("[controller]/[action]")]
    // Area is used for admin panel
    // f.ex [Route("[area]/[controller]/[action]")]
    [ApiController]  
    [Route("/api/[controller]/")]
    public class DummyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly InMemoryRepository _repository;

        public DummyController(IMapper mapper, InMemoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        [HttpGet]
        [HttpHead]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(new Artist {Id = 1, Name = "Metallica"});
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
    }
}