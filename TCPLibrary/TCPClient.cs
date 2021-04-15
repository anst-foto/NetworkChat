using System;
using System.Net.Sockets;

namespace TCPLibrary
{
    public class TCPClient : TCP
    {
        public TCPClient() {}

        public TCPClient(string ipAddress, int port) : base(ipAddress, port) {}
        
        public TCPClient(Socket socket) : base(socket) {}

        public void Connect()
        {
            try
            {
                _socket.Connect(_ipEndPoint);
            }
            catch (ArgumentNullException)
            {
                throw new Exception("Пустой аргумент при подключении к серверу");
            }
            catch (SocketException)
            {
                throw new Exception("Произошла ошибка при попытке доступа к сокету");
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("Сокет закрыт");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Сокет был переведен в состояние прослушивания с помощью вызова Listen");
            }

        }
    }
}