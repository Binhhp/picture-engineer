using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PictureEngineer.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PictureEngineer.Core.Controllers.Admin
{
    [Authorize]
    public class HomeAdminController : BaseController
    {
        private readonly PictureEngineerDbContext _context;

        public HomeAdminController(PictureEngineerDbContext context)
        {
            _context = context;
        }

        [Route("/admin/dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/GetFile")]
        [HttpGet]
        public JsonResult GetFileByWeek()
        {
            try
            {
                var files = _context.Files.ToList();
                var list = new List<FilesByDay>() { 
                    new FilesByDay{ Day = Day.Monday, Quantity = 0 },
                    new FilesByDay{ Day = Day.Tuesday, Quantity = 0 },
                    new FilesByDay{ Day = Day.Wednesday, Quantity = 0 },
                    new FilesByDay{ Day = Day.Thursday, Quantity = 0 },
                    new FilesByDay{ Day = Day.Friday, Quantity = 0 },
                    new FilesByDay{ Day = Day.Saturday, Quantity = 0 },
                    new FilesByDay{ Day = Day.Sunday, Quantity = 0 },
                };

                foreach (var f in files)
                {
                    var date = f.DateCreate;
                    var dayOfWeek = date.DayOfWeek.ToString();
                    var day = list.FirstOrDefault(x => x.Day.Equals(dayOfWeek));
                    day.Quantity += 1;
                }
                return Responsed(list);
            }
            catch (Exception ex)
            {
                return ResponsedError(ex.Message);
            }
        }

        [Route("/Unauthorized")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }

    public class FilesByDay
    {
        public string Day { get; set; }
        public int Quantity { get; set; }
    }

    public class Day
    {
        public const string Monday = "Monday";
        public const string Tuesday = "Tuesday";
        public const string Wednesday = "Wednesday";
        public const string Thursday = "Thursday";
        public const string Friday = "Friday";
        public const string Saturday = "Saturday";
        public const string Sunday = "Sunday";
    }
}
