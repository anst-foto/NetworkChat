using System;
using System.Threading.Tasks;
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

           while (true)
           {
               var messageReceive = client.ReceiveMessage();
               ConsoleMessage.ShowInfo($"Сообщение от клиента: {messageReceive}");

               if (messageReceive == @"\stop")
               {
                   ConsoleMessage.ShowLog("Клиент отключается");
                   break;
               }
               
               client.SendMessage("Ваше сообщение получено");
               ConsoleMessage.ShowLog("Клиенту отправлено подтвеждение получения сообщения");
           }
           client.Close();
           ConsoleMessage.ShowLog("Завершается отдельный поток для клиента");
       }
    }
}