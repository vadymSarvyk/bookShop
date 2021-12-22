using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP_Meeting_20.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using ASP_Meeting_20.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ASP_Meeting_20.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookShopContext db;
        private readonly IWebHostEnvironment env;
        //private readonly ITimeService _timeService;
        public HomeController(BookShopContext context, IWebHostEnvironment environment)
        {
            db = context;
            env = environment;
            //_timeService = timeService;
        }
        public async Task<IActionResult> Index()
        {
            var authors = await db.Authors.ToListAsync();
            return View(authors);
        }

        public IActionResult GetContent()
        {
            return Content("Привет мир!", "text/plain; charset=utf-8");
        }

        public IActionResult GetUser(int age)
        {
            var user = new  { Name = "Сергей", age, IsMarried = true };
            return Ok(user);
        }


        public IActionResult GetCar()
        {
            var message = new { Message = "Запрошенный ресурс не может быть найден" };
            
            return NotFound(message);
        }

        public IActionResult GetFile()
        {
            string path = Path.Combine(env.ContentRootPath, "Files/ASP_DZ_27.pdf");
            string filename = "ASP_DZ_27.pdf";
            string contentType = "application/pdf";
            return PhysicalFile(path, contentType, filename);

        }

        public IActionResult GetFile2()
        {
            string path = Path.Combine(env.ContentRootPath, "Files/ASP_DZ_27.pdf");
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            string filename = "ASP_DZ_27_2.pdf";
            string contentType = "application/pdf";
            return File(bytes, contentType, filename);

        }

        //public IActionResult GetTime([FromServices]ITimeService _timeService)
        public IActionResult GetTime()
        {
            ITimeService _timeService = HttpContext.RequestServices.GetService<ITimeService>();
            return Content($"<h2>Current Time {_timeService.Time}</h2>", "text/html");
        }

        public VirtualFileResult GetVirtualFile()
        {
            var filepath = Path.Combine("~/Files", "hello.txt");
            return File(filepath, "text/plain", "hello.txt");
        }
        public IActionResult GetControllerInfo()
        {
            
            string controller = ControllerContext.RouteData.Values["controller"].ToString();
            string action = ControllerContext.RouteData.Values["action"].ToString();
            return Content($"Controller: {controller}, Action: {action}");
        }

        public IActionResult GetActionData()
        {
            Tuple<IHeaderDictionary, IQueryCollection, string> tuple = new Tuple<IHeaderDictionary, IQueryCollection, string>(
                HttpContext.Request.Headers,
                HttpContext.Request.Query,
                HttpContext.Connection.RemoteIpAddress.ToString());
            return View(tuple);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Create(IFormFile photo)
        {
            string path = Path.Combine(env.WebRootPath, "Files", photo.FileName);
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(fs);
            }
            return Created(path, new { message = "Файл создан!" });
        }
    }
}
