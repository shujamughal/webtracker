using Microsoft.AspNetCore.SignalR;
using System;
namespace SignalRCode
{
    public class ServerHub : Hub
    {
        public void SendMessage(string user, string message)
        {
            Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}