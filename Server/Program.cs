using System;
using System.Text.Json;
using System.Threading.Tasks;
using Commands;
using Messages;
using TCPLibrary;

namespace Server
{
   class Program
    {
       static void Main()
        {
            ConsoleMessage.ShowLog("Сервер запущен");

            var server = new TCPServer();
            try
            {
                server.Start();
                ConsoleMessage.ShowLog("Сервер ожидает подключения");

                while (true)
                {
                    var client = server.NewClient();
                    ConsoleMessage.ShowLog("Подключился новый клиент");

                    var task = Task.Run(() => NewClientTask(client));
                }
            }
            catch (Exception e)
            {
                ConsoleMessage.ShowError(e.Message);
            }
        }

       static void NewClientTask(TCP client)
       {
           ConsoleMessage.ShowLog("Запущен отдельный поток для нового подключения");
           var json = client.ReceiveMessage();
           var name = JsonSerializer.Deserialize<ChatMessageType>(json);
           string clientName;
           clientName = name.Type == TypeMessage.Welcome ? name.Message : "Unknown";

           while (true)
           {
               var messageReceive = client.ReceiveMessage();
               var receive = JsonSerializer.Deserialize<ChatMessageType>(messageReceive);
               if (receive.Type == TypeMessage.Stop)
               {
                   ConsoleMessage.ShowLog($"Клиент {clientName} отключается");
                   break;
               }
               else
               {
                   ConsoleMessage.ShowInfo($"Сообщение от {clientName}: {receive.Message}");
               }
              
               var message = new ChatMessageType(TypeMessage.Message, $"[{clientName}] Ваше сообщение получено");
               var send = JsonSerializer.Serialize(message);
               client.SendMessage(send);
               ConsoleMessage.ShowLog($"{clientName} отправлено подтвеждение получения сообщения");
           }
           client.Close();
           ConsoleMessage.ShowLog($"Завершается отдельный поток для клиента - {clientName}");
       }
    }
}