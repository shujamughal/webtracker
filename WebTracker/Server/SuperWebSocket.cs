//using System;
//using SuperWebSocket;
//namespace Server
//{
//    public class SuperWebSocket
//    {
//        private static WebSocketServer server;
//        static void Main()
//        {
//            server = new WebSocketServer();
//            server.Setup(5000);
//            server.NewSessionConnected += Server_NewSessionConnected;
//            server.NewMessageReceived += Server_NewMessageReceived;
//            server.NewDataReceived += Server_NewDataReceived;
//            server.SessionClosed += Server_SessionClosed;
//            server.Start();
//            Console.WriteLine("Server Started!");
//            Console.ReadKey();
//            server.Stop();
//            Console.WriteLine("Server Stopped!");
//        }

//        private static void Server_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
//        {
//            Console.WriteLine("Server_SessionClosed: " + value);
//        }

//        private static void Server_NewDataReceived(WebSocketSession session, byte[] value)
//        {
//            Console.WriteLine("Server_NewDataReceived: " + value);
//        }

//        private static void Server_NewMessageReceived(WebSocketSession session, string value)
//        {
//            Console.WriteLine("Server_NewMessageReceived: " + value);
//        }

//        private static void Server_NewSessionConnected(WebSocketSession session)
//        {
//            Console.WriteLine("Server_NewSessionConnected!");
//        }
//    }
//}
