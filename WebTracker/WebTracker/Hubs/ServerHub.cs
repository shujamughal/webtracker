using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class ServerHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("User Connected: " + Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public void ReceiveMessage(string data)
        {
            Console.WriteLine("Data Received: " + data);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("User Disconnected: " + Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}