using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodingExercise.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CodingExercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;
        public HomeController(ILoggerFactory loggerFactory, IHostingEnvironment hostEnvironment, IConfiguration config)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _hostEnvironment = hostEnvironment;
            _configuration = config;
        }

        #region Assembly Attributes

        private static string GetRuntimeVersion() => typeof(HomeController)
                                                     .GetTypeInfo().Assembly
                                                     .GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        private static string GetRuntimeFileVersion() => typeof(HomeController)
                                                         .GetTypeInfo().Assembly
                                                         .GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

        private static string GetRuntimeDescription() => typeof(HomeController)
                                                         .GetTypeInfo().Assembly
                                                         .GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

        private static string GetRuntimeCompany() => typeof(HomeController)
                                                     .GetTypeInfo().Assembly
                                                     .GetCustomAttribute<AssemblyCompanyAttribute>().Company;

        private static string GetRuntimeConfiguration() => typeof(HomeController)
                                                           .GetTypeInfo().Assembly
                                                           .GetCustomAttribute<AssemblyConfigurationAttribute>().Configuration;

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = GetRuntimeDescription();
            ViewData["Version"] = GetRuntimeVersion();
            ViewData["FileVersion"] = GetRuntimeFileVersion();
            ViewData["Company"] = GetRuntimeCompany();
            //ViewData["Author"] = GetRuntime();
            ViewData["Configuration"] = GetRuntimeConfiguration();
            ViewData["Server"] = GetRuntimeServer();
            ViewData["Database"] = GetRuntimeDatabase();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetRuntimeServer()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");//Server=(local);Database=Elgama;Trusted_Connection=True;MultipleActiveResultSets=true
            var items = connectionString.Split(';');
            var db = items[0];//Server=(local)
            var dbName = db.Split('=')[1];//(local)
            return dbName;
        }

        private string GetRuntimeDatabase()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");//Server=(local);Database=Elgama;Trusted_Connection=True;MultipleActiveResultSets=true
            var items = connectionString.Split(';');
            var db = items[1];//Database=Elgama
            var dbName = db.Split('=')[1];//Elgama
            return dbName;
        }

    }
}
