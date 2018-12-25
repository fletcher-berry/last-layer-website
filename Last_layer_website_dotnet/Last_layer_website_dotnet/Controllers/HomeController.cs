using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Last_layer_website_dotnet.Models;
using Last_layer_website_dotnet.Database;
using Cubing;
using System.Drawing;
using Last_layer_website_dotnet.Models.Cubes;
using System.IO;
using System.Drawing.Imaging;
using System.Net.Http;

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

        [Route("algs/{caseTypeStr}/{id}")]
        public IActionResult GetLastLayer(string caseTypeStr, int id)
        {
            if (!Enum.TryParse<CaseType>(caseTypeStr, true, out CaseType caseType))
            {
                return Error("Invalid case type");
            }
            var model = _algorithmService.GetLastLayer(caseType, id);
            return View("AlgPage", model);
        }

        [Route("img/{caseTypeStr}/{id:int}")]
        public IActionResult GetImage(string caseTypeStr, int id)
        {
            if (!Enum.TryParse<CaseType>(caseTypeStr, true, out CaseType caseType))
            {
                return Error("Invalid case type");
            }
            ICubeCore cube;
            if (caseType == CaseType.OLL)
                cube = new OllCubeCore();
            else if (caseType == CaseType.OLLCP)
                cube = new OllcpCubeCore();
            else if (caseType == CaseType.OneLookLL)
                cube = new OneLookLLCubeCore();
            else
                cube = new OllCubeCore();

            if (id >= cube.GetNumPositions() || id < 0)
                return Error("Invalid position number");

            cube.SetUpPosition(id);
            var bitmap = cube.DrawCore();
            var stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            return new FileStreamResult(stream, "image/png");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message = "")
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }
    }
}
