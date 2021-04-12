using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var ip = IPAddress.Parse("127.0.0.1");
            const int port = 8005;
            
            ShowInfo("Сервер запущен");

            var ipServer = new IPEndPoint(ip, port);
            var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipServer);
            listenSocket.Listen(10);

            ShowInfo("Сервер ожидает подключение");
            
            while (true)
            {
                var client = listenSocket.Accept();
                ShowInfo("Клиент подключился");

                var message = new StringBuilder();
                var buffer = new byte[256];
                do
                {
                    var bytes = client.Receive(buffer);
                    message.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                } while (client.Available > 0);
                
                ShowMessage($"Сообщение от клиента - {message}");
                
                message.Clear();
                message.Append("Сообщение получено");
                var bufferSend = Encoding.Unicode.GetBytes(message.ToString());
                client.Send(bufferSend);
                ShowInfo("Сообщение клиенту отправлено");

                client.Shutdown(SocketShutdown.Both);
                client.Close();
                
                ShowInfo("Соединение с клиентом разорвано");
            }
        }

        static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void ShowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}