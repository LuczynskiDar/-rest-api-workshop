using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        
        // Scructor lib
        // public HomeController(ILogger<HomeController> logger, ITest test)
        public HomeController(ILogger<HomeController> logger, IEnumerable<ITest> test)
        {
            _logger = logger;
        }
        
        [HttpGet]
        [HttpHead]
        public IActionResult Index()
        {
            var list = new[] { new Artist{Id = 1, Name = "Metallica"}};
            // return Ok(new Artist {Id = 1, Name = "Metallica"});
            return Ok(list);
        }
        [HttpGet]
        [HttpHead]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(new Artist {Id = 1, Name = "Metallica"});
        }
    }
}