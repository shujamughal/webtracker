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
        IUserRepository _userRepository;
        public ServerHub(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User Connected");
            return base.OnConnectedAsync();
        }
        public void ReceiveFormData(string data)
        {
            JObject dataObj = JObject.Parse(data);
            Console.WriteLine("Form Data Submitted: " + dataObj);
        }
        public void ReceiveAction(string action, string data)
        {
            Console.WriteLine("Action Performed: " + action);
            Console.WriteLine("Action Data: " + data);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("User Disconnected: " + exception.Message);
            return base.OnDisconnectedAsync(exception);
        }
        public Task AddNewUser(string web, string url, string deviceType, string browser, string location)
        {
//            List<User> users = _userRepository.GetAllUsers();
            Console.WriteLine("New User Connected to " + url + " from " + browser + " with " + deviceType);
            JObject userLocation = JObject.Parse(location);
            
            ActionData actionData = new()
            {
                Name = "PageView",
                Data = url,
            };
            Models.Action action = new()
            {
                Type = "PageView",
                Content = url,
                Data = new List<ActionData>()
                {
                    actionData
                }
            };
            Url urlObj = new()
            {
                WebUrl = url,
                Actions = new List<Models.Action>()
                {
                    action
                }
            };
            Flow flow= new()
            {
                Urls = new List<Url>()
                {
                    urlObj
                }
            };
            Website website = new()
            {
                Web = web,
                VisitCount = 1,
                Flows = new List<Flow>()
                {
                    flow
                }
            };
            Address Location = new()
            {
                CountryCode = userLocation["country_code"].ToString(),
                CountryName = userLocation["country_name"].ToString(),
                City = userLocation["city"].ToString(),
                Postal = userLocation["postal"].ToString(),
                Latitude = userLocation["latitude"].ToString(),
                Longitude = userLocation["longitude"].ToString(),
                IPv4 = userLocation["IPv4"].ToString(),
                State = userLocation["state"].ToString()
            };
            User user = new()
            {
                DeviceType = deviceType,
                Browser = browser,
                LastConnection = DateTime.Now,
                Location = Location,
                Web = new ()
                {
                    website
                }
            };
            
            // action.Data.Add(actionData);
            // urlObj.Actions.Add(action);
            // flow.Urls.Add(urlObj);
            // website.Flows.Add(flow);
            // user.Web.Add(website);

            /*
            user.Web.Add(new Website()
            {
                Web = web,
                VisitCount = 1,
                Flows = new List<Flow>()
                {
                    new Flow()
                    {
                        Urls = new List<Url>()
                        {
                            new Url()
                            {
                                WebUrl = url,
                                Actions = new List<Models.Action>()
                                {
                                    new Models.Action()
                                    {
                                        Type = "PageView",
                                        Content = url,
                                        Data = new List<ActionData>()
                                        {
                                            new ActionData()
                                            {
                                                Name = "PageView",
                                                Data = url
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
            */
            _userRepository.AddUser(user);
            string functionName = "AddNewUser";
            string userCookie = "webtracker_user";
            string userIdValue = user.Id.ToString();
            string webCookie = "webtracker_web" + web;
            string webIdValue = website.Id.ToString();
            string flowSession = "webtracker_flow" + web;
            string flowIdValue = flow.Id.ToString();
            return Clients.Caller.SendAsync(functionName, userCookie, userIdValue, webCookie, webIdValue, flowSession, flowIdValue);
        }
        public Task NewWeb(string userId, string web, string url)
        {
            User user = _userRepository.GetUserById(Int16.Parse(userId));
            user.LastConnection = DateTime.Now;
            /*
            Website website = new()
            {
                Web = web,
                VisitCount = 1,
                Flows = new List<Flow>()
                {
                    new Flow()
                    {
                        Urls = new List<Url>()
                        {
                            new Url()
                            {
                                WebUrl = web,
                                Actions = new List<Models.Action>()
                                {
                                    new Models.Action()
                                    {
                                        Type = "PageView",
                                        Content = web,
                                        Data = new List<ActionData>()
                                        {
                                            new ActionData()
                                            {
                                                Name = "PageView",
                                                Data = web
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            */
            Website website = new()
            {
                Web = web,
                VisitCount = 1
            };
            Flow flow= new();
            Url urlObj = new()
            {
                WebUrl = url,
            };
            Models.Action action = new()
            {
                Type = "PageView",
                Content = url,
            };
            ActionData actionData = new()
            {
                Name = "PageView",
                Data = url,
            };
            action.Data.Add(actionData);
            urlObj.Actions.Add(action);
            flow.Urls.Add(urlObj);
            website.Flows.Add(flow);
            user.Web.Add(website);
            _userRepository.UpdateUser(int.Parse(userId), user);

            string functionName = "AddNewUser";
            string userCookie = "webtracker_user";
            string userIdValue = user.Id.ToString();
            string webCookie = "webtracker_web" + web;
            string webIdValue = website.Id.ToString();
            string flowSession = "webtracker_flow" + web;
            string flowIdValue = flow.Id.ToString();
            return Clients.Caller.SendAsync(functionName, userCookie, userIdValue, webCookie, webIdValue, flowSession, flowIdValue);
        
        }
        public Task ExistingWeb(string userId, string webId, string web)
        {
            User user = _userRepository.GetUserById(Int16.Parse(userId));
            Website website = user.Web.Find(x => x.Id == Int16.Parse(webId)); 
            Flow flow = new Flow()
            {
                Urls = new List<Url>()
                {
                    new Url()
                    {
                        WebUrl = web,
                        Actions = new List<Models.Action>()
                        {
                            new Models.Action()
                            {
                                Type = "PageView",
                                Content = web,
                                Data = new List<ActionData>()
                                {
                                    new ActionData()
                                    {
                                        Name = "PageView",
                                        Data = web
                                    }
                                }
                            }
                        }
                    }
                }
            };
            website.Web = web;
            website.Flows.Add(flow);
            website.VisitCount++;
            user.LastConnection = DateTime.Now;
            _userRepository.UpdateUser(int.Parse(userId), user);
            return Clients.Caller.SendAsync("SaveWebSession", "webtracker_flow" + web, flow.Id.ToString());
        }
        public void SetLocation(string user, string location)
        {
            List<User> users = _userRepository.GetAllUsers();
            if(user == "")
            {
                return;
            }
            JObject userLocation = JObject.Parse(location);
            User _user = _userRepository.GetUserById(int.Parse(user));
            if (_user != null)
            {
                _user.Location.CountryCode = userLocation["country_code"].ToString();
                _user.Location.CountryName = userLocation["country_name"].ToString();
                _user.Location.City = userLocation["city"].ToString();
                _user.Location.Postal = userLocation["postal"].ToString();
                _user.Location.Latitude = userLocation["latitude"].ToString();
                _user.Location.Longitude = userLocation["longitude"].ToString();
                _user.Location.IPv4 = userLocation["IPv4"].ToString();
                _user.Location.State = userLocation["state"].ToString();
                _userRepository.UpdateUser(_user.Id, _user);
                
            }
            Console.WriteLine("User location details are: " + userLocation);
        }
        public void ExistingFlow(string userId, string webId, string url)
        {
            List<User> users = _userRepository.GetAllUsers();
            if (userId == "")
                return;
            User user = _userRepository.GetUserById(int.Parse(userId));
            Website website = user.Web.Find(x => x.Id == Int16.Parse(webId));
            Flow flow = website.Flows.Find(x => x.Id == Int16.Parse(webId));
            Url newUrl = new Url()
            {
                WebUrl = url,
                Actions = new List<Models.Action>()
                {
                    new Models.Action()
                    {
                        Type = "PageView",
                        Content = url,
                        Data = new List<ActionData>()
                        {
                            new ActionData()
                            {
                                Name = "PageView",
                                Data = url
                            }
                        }
                    }
                }
            };
            flow.Urls.Add(newUrl);
            _userRepository.UpdateUser(int.Parse(userId), user);
            Console.WriteLine(url);
        }
    }
}
