/*
using System;
using Fleck;
namespace Server
{
    public class Fleck
    {
        static void Main()
        {
            var server = new WebSocketServer("ws://127.0.0.1:8431");
            
            server.Start(socket =>
            {
                Console.WriteLine("Server Started!");
                socket.OnOpen = () => Console.WriteLine("Client Connected!");
                socket.OnMessage = (message) => Console.WriteLine(message);
                socket.OnClose = () => Console.WriteLine("Client Disconnected!");
                Console.ReadKey();
            });
           
        }
    }
}
*/