using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonsDirectoryApp.Repos;
using PersonsDirectoryApp.Repos.Infrastructure;
using PersonsDirectoryApp.Web.Models;
using PersonsDirectoryApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using PersonsDirectoryApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using PersonsDirectoryApp.Web.ViewModels;

namespace PersonsDirectoryApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            _logger.LogCritical($"ExceptionPath: {errorDetails.Path};" +
                $"\n ExceptionMessage: {errorDetails.Error.Message};" +
                $"\n Stacktrace: {errorDetails.Error.StackTrace}");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
