using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using WebTracker.Models;
using WebTracker.Repositories;
using System.Collections.Generic;
namespace WebTracker.Hubs
{
    public class ServerHub : Hub
    {
        IWebsiteRepository _websiteRepository;
        IUserRepository _userRepository;
        IFlowRepository _flowRepository;
        IUrlRepository _urlRepository;
        IActionRepository _actionRepository;
        IActionDataRepository _actionDataRepository;
        public ServerHub(IWebsiteRepository websiteRepository, IUserRepository userRepository, IFlowRepository flowRepository, IUrlRepository urlRepository, IActionRepository actionRepository, IActionDataRepository actionDataRepository)
        {
            _websiteRepository = websiteRepository;
            _userRepository = userRepository;
            _flowRepository = flowRepository;
            _urlRepository = urlRepository;
            _actionRepository = actionRepository;
            _actionDataRepository = actionDataRepository;
        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User Connected");
            return base.OnConnectedAsync();
        }
        public void ReceiveFormData(string urlId, string data)
        {
            // add new action in the database
            Models.Action action = new Models.Action()
            {
                Type = "Form",
                Content = "Form Data",
                UrlId = Convert.ToInt32(urlId)
            };
            _actionRepository.AddAction(action);

            // add new action data in the database
            JObject dataObj = JObject.Parse(data);

            foreach (var item in dataObj)
            {
                ActionData actionData = new ActionData()
                {
                    Name = item.Key,
                    Data = item.Value.ToString(),
                    ActionId = action.ActionId
                };
                _actionDataRepository.AddActionData(actionData);
            }
            Console.WriteLine("Form Data Submitted: " + dataObj);
        }
        public void ReceiveAction(string urlId, string action, string data)
        {
            // add new action in the database
            Models.Action actionObj = new Models.Action()
            {
                Type = action,
                Content = data,
                UrlId = Convert.ToInt32(urlId)
            };
            _actionRepository.AddAction(actionObj);

            // add new action data in the database
            ActionData actionData = new ActionData()
            {
                Name = action,
                Data = data,
                ActionId = actionObj.ActionId
            };
            _actionDataRepository.AddActionData(actionData);

            Console.WriteLine("Action Performed: " + action);
            Console.WriteLine("Action Data: " + data);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("User Disconnected");
            return base.OnDisconnectedAsync(exception);
        }
        public async Task AddNewUser(string web, string url, string deviceType, string browser, string location)
        {
            Console.WriteLine("New User Connected to " + web + " at " + url + " with " + browser + " using " + deviceType + " from " + location);
            // check if website is in the database
            int websiteId = await _websiteRepository.GetWebsiteIdByName(web);

            // add or update website in database
            Website website;
            if(websiteId == -1)
            {
                website = new Website()
                {
                    Web= web,
                    VisitCount = 1
                };
                websiteId = website.WebsiteId;

                try{
                    _websiteRepository.AddWebsite(website);
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.CompletedTask;
                    return;
                }
            }
            else
            {
                website = await _websiteRepository.GetWebsiteById(websiteId);
                website.VisitCount++;
                
                try{
                    _websiteRepository.UpdateWebsite(websiteId, website);
                } catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.CompletedTask;
                    return;
                }
                
            }

            // add new user in the database
            JObject userLocation = JObject.Parse(location);
            User user = new User()
            {
                DeviceType = deviceType,
                Browser = browser,
                LastConnection = DateTime.Now,
                Address = new Address()
                {
                    CountryCode = userLocation["country_code"].ToString(),
                    CountryName = userLocation["country_name"].ToString(),
                    City = userLocation["city"].ToString(),
                    Postal = userLocation["postal"].ToString(),
                    Latitude = userLocation["latitude"].ToString(),
                    Longitude = userLocation["longitude"].ToString(),
                    IPv4 = userLocation["IPv4"].ToString(),
                    State = userLocation["state"].ToString()
                },
                WebsiteId = websiteId,
            };
            try{
                _userRepository.AddUser(user);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _websiteRepository.DeleteWebsite(websiteId);
                await Task.CompletedTask;
                return;
            }

            // add new flow in the database
            Flow flow = new Flow()
            {
                UserId = user.UserId
            };

            try{
                _flowRepository.AddFlow(flow);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _userRepository.DeleteUser(user.UserId);
                _websiteRepository.DeleteWebsite(websiteId);
                await Task.CompletedTask;
                return;
            }

            // add new url in the database
            Url urlObj = new Url()
            {
                WebUrl = url,
                FlowId = flow.FlowId
            };

            try{
                _urlRepository.AddUrl(urlObj);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _flowRepository.DeleteFlow(flow.FlowId);
                _userRepository.DeleteUser(user.UserId);
                _websiteRepository.DeleteWebsite(websiteId);
                await Task.CompletedTask;
                return;
            }

            // add new action in the database
            Models.Action action = new Models.Action()
            {
                Type = "Page Load",
                Content = url,
                UrlId = urlObj.UrlId
            };

            try{
                _actionRepository.AddAction(action);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _urlRepository.DeleteUrl(urlObj.UrlId);
                _flowRepository.DeleteFlow(flow.FlowId);
                _userRepository.DeleteUser(user.UserId);
                _websiteRepository.DeleteWebsite(websiteId);
                await Task.CompletedTask;
                return;
            }

            // add new action data in the database
            ActionData actionData = new ActionData()
            {
                Name = "Page Load",
                Data = url,
                ActionId = action.ActionId
            };
            
            try{
                _actionDataRepository.AddActionData(actionData);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _actionRepository.DeleteAction(action.ActionId);
                _urlRepository.DeleteUrl(urlObj.UrlId);
                _flowRepository.DeleteFlow(flow.FlowId);
                _userRepository.DeleteUser(user.UserId);
                _websiteRepository.DeleteWebsite(websiteId);
                await Task.CompletedTask;
                return;
            }

            // try {
            //     _userRepository.AddUser(user);
            // } catch(Exception e)
            // {
            //     Console.WriteLine(e.Message);
            //     return;
            // }
            
            string functionName = "AddNewUser";
            string userCookie = "webtracker_user";
            string userIdValue = user.UserId.ToString();
            string webCookie = "webtracker_web" + web;
            string webIdValue = website.WebsiteId.ToString();
            string flowSession = "webtracker_flow" + web;
            string flowIdValue = flow.FlowId.ToString();
            string urlSession = "webtracker_url" + url;
            string urlIdValue = urlObj.UrlId.ToString();

            Console.WriteLine("Calling " + functionName + " passing data: ");
            Console.WriteLine(userCookie + " : " + userIdValue);
            Console.WriteLine(webCookie + " : " + webIdValue);
            Console.WriteLine(flowSession + " : " + flowIdValue);
            Console.WriteLine(urlSession + " : " + urlIdValue);
            await Clients.Caller.SendAsync(functionName, userCookie, userIdValue, webCookie, webIdValue, flowSession, flowIdValue, urlSession, urlIdValue);
        }
        public async Task ExistingUser(string websiteId, string userId, string url)
        {
            Console.WriteLine("Website with id = " + websiteId + " user id = " + userId + " to url : " + url);
            // update visit count of the website
            Website website;
            try{
              website = await _websiteRepository.GetWebsiteById(Convert.ToInt32(websiteId));
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                await Task.CompletedTask;
                return;
            }
            website.VisitCount++;
            try{
                _websiteRepository.UpdateWebsite(Convert.ToInt32(websiteId), website);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                await Task.CompletedTask;
                return;
            }

            // create new flow
            Flow flow = new Flow()
            {
                UserId = Convert.ToInt32(userId)
            };
            try{
                _flowRepository.AddFlow(flow);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                await Task.CompletedTask;
                return;
            }

            // create new url
            Url urlObj = new Url()
            {
                WebUrl = url,
                FlowId = flow.FlowId
            };
            try{
                _urlRepository.AddUrl(urlObj);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _flowRepository.DeleteFlow(flow.FlowId);
                await Task.CompletedTask;
                return;
            }

            // create new action
            Models.Action action = new Models.Action()
            {
                Type = "Page Load",
                Content = url,
                UrlId = urlObj.UrlId
            };
            try{
                _actionRepository.AddAction(action);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _urlRepository.DeleteUrl(urlObj.UrlId);
                _flowRepository.DeleteFlow(flow.FlowId);
                await Task.CompletedTask;
                return;
            }

            // create new action data
            ActionData actionData = new ActionData()
            {
                Name = "Page Load",
                Data = url,
                ActionId = action.ActionId
            };
            try{
                _actionDataRepository.AddActionData(actionData);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _actionRepository.DeleteAction(action.ActionId);
                _urlRepository.DeleteUrl(urlObj.UrlId);
                _flowRepository.DeleteFlow(flow.FlowId);
                await Task.CompletedTask;
                return;
            }

            Console.WriteLine("Existing User Connected to " + website.Web);

            string functionName = "ExistingUser";
            string flowSession = "webtracker_flow" + website.Web;
            string flowIdValue = flow.FlowId.ToString();
            string urlSession = "webtracker_url" + url;
            string urlIdValue = urlObj.UrlId.ToString();

            Console.WriteLine("Calling " + functionName + " passing data: ");
            Console.WriteLine(flowSession + " : " + flowIdValue);
            Console.WriteLine(urlSession + " : " + urlIdValue);
            await Clients.Caller.SendAsync(functionName, flowSession, flowIdValue, urlSession, urlIdValue);
        }
        public async Task ExistingFlow(string flowId, string url)
        {
            Console.WriteLine("User continue to the flow with id = " + flowId + " to url : " + url);
            // create new url
            Url urlObj = new Url()
            {
                WebUrl = url,
                FlowId = Convert.ToInt32(flowId)
            };
            try{
                _urlRepository.AddUrl(urlObj);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                await Task.CompletedTask;
                return;
            }

            // create new action
            Models.Action action = new Models.Action()
            {
                Type = "Page Load",
                Content = url,
                UrlId = urlObj.UrlId
            };
            try{
                _actionRepository.AddAction(action);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _urlRepository.DeleteUrl(urlObj.UrlId);
                await Task.CompletedTask;
                return;
            }

            // create new action data
            ActionData actionData = new ActionData()
            {
                Name = "Page Load",
                Data = url,
                ActionId = action.ActionId
            };
            try{
                _actionDataRepository.AddActionData(actionData);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _actionRepository.DeleteAction(action.ActionId);
                _urlRepository.DeleteUrl(urlObj.UrlId);
                await Task.CompletedTask;
                return;
            }

            Console.WriteLine("Existing Flow Connected to " + url);

            string functionName = "ExistingFlow";
            string urlSession = "webtracker_url" + url;
            string urlIdValue = urlObj.UrlId.ToString();

            Console.WriteLine("Calling " + functionName + " passing data: ");
            Console.WriteLine(urlSession + " : " + urlIdValue);
            await Clients.Caller.SendAsync(functionName, urlSession, urlIdValue);
        }
    }
}
