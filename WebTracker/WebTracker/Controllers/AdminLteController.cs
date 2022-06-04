using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebTracker.Models;
using WebTracker.Repositories;
using Newtonsoft.Json;
namespace WebTracker.Controllers
{
    public class AdminLteController : Controller
    {
        private readonly ILogger<AdminLteController> _logger;
        private readonly IWebsiteRepository _websiteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFlowRepository _flowRepository;
        private readonly IUrlRepository _urlRepository;
        private readonly IActionRepository _actionRepository;
        private readonly IActionDataRepository _actionDataRepository;

        public AdminLteController(ILogger<AdminLteController> logger, IWebsiteRepository websiteRepository, IUserRepository userRepository, IFlowRepository flowRepository, IUrlRepository urlRepository, IActionRepository actionRepository, IActionDataRepository actionDataRepository)
        {
            _logger = logger;
            _websiteRepository = websiteRepository;
            _userRepository = userRepository;
            _flowRepository = flowRepository;
            _urlRepository = urlRepository;
            _actionRepository = actionRepository;
            _actionDataRepository = actionDataRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<Website> websites = _websiteRepository.GetAllWebsites();
            ViewBag.Websites = JsonConvert.SerializeObject(websites);
            return View(websites);
        }
        [HttpGet]
        public JsonResult GetWebsiteById(int id)
        {
            Website website = _websiteRepository.GetWebsiteById(id);
            return Json(website);
        }
        [HttpGet]
        public string GetWebsiteByName(string name)
        {
            int websiteId = _websiteRepository.GetWebsiteIdByName(name);
            Website website = _websiteRepository.GetWebsiteById(websiteId);
            string result = JsonConvert.SerializeObject(website);
            return result;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}