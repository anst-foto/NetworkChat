using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var ip = IPAddress.Parse("127.0.0.1");
            const int port = 8005;
            
            var ipServer = new IPEndPoint(ip, port);
            var connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connection.Connect(ipServer);
            
            ShowInfo("Соединение с сервером успешно установлено");

            var message = new StringBuilder();
            message.Append("Проверка связи...");
            var bufferSend = Encoding.Unicode.GetBytes(message.ToString());
            connection.Send(bufferSend);
            ShowInfo("Сообщение серверу отправлено");
            
            message.Clear();
            var buffer = new byte[256];
            do
            {
                var bytes = connection.Receive(buffer);
                message.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
            } while (connection.Available > 0);
                
            ShowMessage($"Сообщение от сервера - {message}");

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