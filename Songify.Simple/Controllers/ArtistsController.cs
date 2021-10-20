using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Songify.Simple.Dtos;

namespace Songify.Simple.Controllers
{
    public class ArtistsController
    {
        private readonly IMapper _mapper;
        
        public ArtistsController(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        IActionResult Create(CreateArtistResource request)
        {
            
        }

        IActionResult Get(int id)
        {
            
        }

        IActionResult Remove(int id)
        {
            
        }

        IActionResult Update(UpdateArtistResource request)
        {
            
        }
    }
}