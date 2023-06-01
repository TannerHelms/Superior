using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Superior_Cloud_Accounting.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Reflection;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Superior_Cloud_Accounting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SuperiorDbContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new SuperiorDbContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Submit(string name, string email, string message)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["status"] = "Success!";
            await _context.Forms.AddAsync(new Form
            {
                Name = name,
                Email = email,
                Message = message
            });
            _context.SaveChanges();

            /*
            await _formService.Create(new Form
            {
                Name = name,
                Email = email,
            });
           */
            return JsonConvert.SerializeObject(dict);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}