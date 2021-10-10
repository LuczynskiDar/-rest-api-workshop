using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestMVC2.Models;

namespace TestMVC2.Controllers
{
    [ApiController]    
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        
        // Scructor lib
        // public HomeController(ILogger<HomeController> logger, ITest test)
        public HomeController(ILogger<HomeController> logger, IEnumerable<ITest> test)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return Ok(new Artist {Id = 1, Name = "Metallica"});
        }
    }
}