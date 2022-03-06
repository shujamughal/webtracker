using System;
using WebSocketSharp;
using WebSocketSharp.Server;
namespace Server
{
    public class Index : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Console.WriteLine("Cleint Conntected!");
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine("Data Received: " + e.Data);
        }
        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine("Client Disconnected!");
        }
    }
    class WebSocketSharp
    {
        static void Main(string[] args)
        {
            WebSocketServer server = new WebSocketServer("ws://127.0.0.1:5000");
            server.AddWebSocketService<Index>("/index");
            server.Start();
            Console.WriteLine("Server Started! Press Any key to end the server!");
            Console.ReadKey();
            server.Stop();
            Console.WriteLine("Server Stopped!");
        }
    }
}
