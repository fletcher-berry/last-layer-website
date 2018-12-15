using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Last_layer_website_dotnet.Models;
using Last_layer_website_dotnet.Database;
using Cubing;

namespace Last_layer_website_dotnet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAlgorithmService _algorithmService;

        public HomeController(IAlgorithmService algorithmService)
        {
            _algorithmService = algorithmService;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Route("algs/oll")]
        public IActionResult GetOll()
        {
            var model = _algorithmService.GetOll();
            return View("AlgPage", model);
        }

        // right now, just checking that I can use the Cubing library
        [Route("img/{id}")]
        public IActionResult GetImage(int id)
        {
            var cube = new OllCube();
            cube.SetUpPosition(1);
            return new JsonResult("it works!");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
