using System;
using Messages;
using TCPLibrary;

namespace Client
{
    class Program
    {
        static void Main()
        {
            ConsoleMessage.ShowLog("Клиент запущен");
            var client = new TCPClient();

            try
            {
                client.Connect();
                ConsoleMessage.ShowLog("Клиент подключился к серверу");
                
                while (true)
                {
                    Console.Write("Сообщение: ");
                    var sendMessage = Console.ReadLine();
                    client.SendMessage(sendMessage);
                    ConsoleMessage.ShowLog("Сообщение серверу отправлено");

                    if (sendMessage == @"\stop")
                    {
                        ConsoleMessage.ShowLog("Клиент отключается от сервера");
                        break;
                    }

                    var receiveMessage = client.ReceiveMessage();
                    ConsoleMessage.ShowInfo($"Ответ от сервера: {receiveMessage}");
                }
            
                client.Close();
                ConsoleMessage.ShowLog("Клиент отключился от сервера");
            }
            catch (Exception e)
            {
                ConsoleMessage.ShowError(e.Message);
            }
        }

        
    }
}