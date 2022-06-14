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
        public async Task<IActionResult> Index()
        {
            IEnumerable<Website> websites = await _websiteRepository.GetAllWebsites();
            for(int i = 0; i < websites.Count(); i++)
            {
                websites.ElementAt(i).Users = await _userRepository.GetUsersByWebsiteId(websites.ElementAt(i).WebsiteId);
            }
            return View(websites);
        }
        public async Task<IActionResult> WebsiteDetails(int id)
        {
            Website website = await _websiteRepository.GetWebsiteById(id);
            website.Users = await _userRepository.GetUsersByWebsiteId(website.WebsiteId);
            List <Tuple<string?, int>> urls = new List<Tuple<string?, int>>();
            foreach(User user in website.Users)
            {
                user.Flows = await _flowRepository.GetFlowsByUserId(user.UserId);
                urls.Add(Tuple.Create(Convert.ToString(user.Flows), 0));
                foreach(Flow flow in user.Flows)
                {
                    flow.Urls = await _urlRepository.GetUrlsByFlowId(flow.FlowId);
                }
            }
            for(int i = 0; i < urls.Count(); i++)
            {
                for(int j = i + 1; j <urls.Count(); j++ )
                {
                    if(urls[i].Item1 == urls[j].Item1)
                    {
                        urls[i] = Tuple.Create(urls[i].Item1, urls[i].Item2 + 1);
                        urls.RemoveAt(j);
                        j--;
                    }
                }
            }
            Console.WriteLine(urls);
            return View(website);
        }
        
        public async Task<IActionResult> UrlDetails(int id)
        {
            Url url = await _urlRepository.GetUrlById(id);
            url.Actions = await _actionRepository.GetActionsByUrlId(url.UrlId);
            foreach (Models.Action action in url.Actions)
            {
                action.Data = await _actionDataRepository.GetActionDataByActionId(action.ActionId);
            }

            return View(url);
        }
        public async Task<IActionResult> UniqueFlows(int id)
        {
            Flow flow = await _flowRepository.GetFlowById(id);

            return View(flow);
        }
        [HttpGet]
        public async Task<JsonResult> GetWebsiteById(int id)
        {
            Website website = await _websiteRepository.GetWebsiteById(id);
            foreach(var user in website.Users)
            {
                user.Website = null;
                foreach(var flow in user.Flows)
                {
                    flow.User = null;
                    foreach(var url in flow.Urls)
                    {
                        url.Flow = null;
                        foreach(var action in url.Actions)
                        {
                            action.Url = null;
                            action.Data = await _actionDataRepository.GetActionDataByActionId(action.ActionId);
                            foreach(var data in action.Data)
                            {
                                data.Action = null;
                            }
                        }
                    }
                }
            }
            return Json(website);
        }
        [HttpGet]
        public async Task<string> GetWebsiteByName(string name)
        {
            int websiteId = await _websiteRepository.GetWebsiteIdByName(name);
            Website website = await _websiteRepository.GetWebsiteById(websiteId);
            foreach(var user in website.Users)
            {
                user.Website = null;
                foreach(var flow in user.Flows)
                {
                    flow.User = null;
                    foreach(var url in flow.Urls)
                    {
                        url.Flow = null;
                        foreach(var action in url.Actions)
                        {
                            action.Url = null;
                            action.Data = await _actionDataRepository.GetActionDataByActionId(action.ActionId);
                            foreach(var data in action.Data)
                            {
                                data.Action = null;
                            }
                        }
                    }
                }
            }
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