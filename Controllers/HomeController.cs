using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Superior_Cloud_Accounting.Models;
using System.Diagnostics;
using SendGrid;
using SendGrid.Helpers.Mail;

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
                Message = message,
            });
            _context.SaveChanges();


            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tannerhelms01@gmail.com", "Tanner Helms");
            var subject = "New form submitted!";
            var to = new EmailAddress("superior.cloud.acctg@gmail.com", "Stephanie Helms");
            var plainTextContent = $"Name: {name}\r\nEmail: {email}\r\nMessage: {message}";
            var htmlContent = $"Name: {name}\r\nEmail: {email}\r\nMessage: {message}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return JsonConvert.SerializeObject(dict);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}