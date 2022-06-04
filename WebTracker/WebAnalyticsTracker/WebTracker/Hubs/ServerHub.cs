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
        public Task AddNewUser(string url, string deviceType, string browser)
        {
            Console.WriteLine("New User Connected to " + url + " from " + browser + " with " + deviceType);
            User user = new()
            {
                VisitCount = 1,
                DeviceType = deviceType,
                Browser = browser,
                LastConnection = DateTime.Now,
                Website = new()
                {
                    Web = url,
                }
            };
            _userRepository.AddUser(user);
            return Clients.Caller.SendAsync("NewUser", "webtracker" + url, user.Id, 365);
        }
        public void ExistingUser(string url, string user, string deviceType, string browser)
        {
            User _user = _userRepository.GetUserById(int.Parse(user));
            if (_user != null)
            {
                _user.VisitCount++;
                _user.LastConnection = DateTime.Now;
                _userRepository.UpdateUser(_user.Id, _user);
            }
            Console.WriteLine("Existing User Connected to " + url + " from " + browser + " with " + deviceType);
        }
        public void SetLocation(string user, string location)
        {
            if(user == "")
            {
                return;
            }
            JObject userLocation = JObject.Parse(location);
            User _user = _userRepository.GetUserById(int.Parse(user));
            if (_user != null)
            {
                _user.Location = new()
                {
                CountryCode = userLocation["country_code"].ToString(),
                CountryName = userLocation["country_name"].ToString(),
                City = userLocation["city"].ToString(),
                Latitude = userLocation["latitude"].ToString(),
                Longitude = userLocation["longitude"].ToString(),
                IPv4 = userLocation["IPv4"].ToString(),
                state = userLocation["state"].ToString()
                };
                _userRepository.UpdateUser(_user.Id, _user);
                
            }
            Console.WriteLine("User location details are: " + userLocation);
        }
        public void NewFlow(string user, string web, string page)
        {
            if( user == "")
                return;
            User _user = _userRepository.GetUserById(int.Parse(user));
            if (_user != null)
            {
                try {
                _user.Website.Flows.Add(new Flow()
                {
                    Urls = new List<Url>()
                    {
                        new()
                        {
                            WebUrl = page
                        }
                    },
                    Actions = new List<Models.Action>()
                    {
                        new()
                        {
                            Type = "PageView",
                            Content = page
                        }
                    }
                });
                _userRepository.UpdateUser(_user.Id, _user);
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("New Flow: " + web);
            Console.WriteLine(page);
            
        }

        public void ExistingFlow(string user, string web, string page)
        {
            if (user == "")
                return;
            User _user = _userRepository.GetUserById(int.Parse(user));
            if(_user != null)
            {
                // get last flow

                List<Flow> flows = _user.Website.Flows;
                Flow lastFlow = _user.Website.Flows[_user.Website.Flows.Count - 1];
                // add new url
                lastFlow.Urls.Add(new Url()
                {
                    WebUrl = page
                });
                // add new action
                lastFlow.Actions.Add(new Models.Action()
                {
                    Type = "PageView",
                    Content = page
                });
                // update user
                _userRepository.UpdateUser(_user.Id, _user);
            }
            Console.WriteLine(page);
        }
    }
}
