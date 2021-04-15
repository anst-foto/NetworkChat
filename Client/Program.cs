using System;
using System.Text.Json;

using Commands;
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
                
                Console.Write("Введите ваше имя: ");
                var name = Console.ReadLine();
                var messageName = new ChatMessageType(TypeMessage.Welcome, name);
                var sendName = JsonSerializer.Serialize(messageName);
                client.SendMessage(sendName);
                
                while (true)
                {
                    Console.Write("Сообщение: ");
                    var sendMessage = Console.ReadLine();
                    if (sendMessage == @"\stop")
                    {
                        var message = new ChatMessageType(TypeMessage.Stop, sendMessage);
                        var send = JsonSerializer.Serialize(message);
                        client.SendMessage(send);
                        ConsoleMessage.ShowLog("Клиент отключается от сервера");
                        break;
                    }
                    else
                    {
                        var message = new ChatMessageType(TypeMessage.Message, sendMessage);
                        var send = JsonSerializer.Serialize(message);
                        client.SendMessage(send);
                        ConsoleMessage.ShowLog("Сообщение серверу отправлено");
                    }

                    var receiveMessage = client.ReceiveMessage();
                    var receive = JsonSerializer.Deserialize<ChatMessageType>(receiveMessage);
                    ConsoleMessage.ShowInfo($"Ответ от сервера: {receive.Message}");
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