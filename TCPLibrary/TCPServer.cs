using System;
using System.Net.Sockets;

namespace TCPLibrary
{
    public class TCPServer : TCP
    {
        public TCPServer() { }

        public TCPServer(string ipAddress, int port) : base(ipAddress, port) { }

        public void Start()
        {
            try
            {
                _socket.Bind(_ipEndPoint);
                _socket.Listen(10);
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("Socket был закрыт");
            }
            catch (SocketException)
            {
                throw new Exception("Произошлп ошибка при попытке досткпа к сокету");
            }
        }

        public TCP NewClient()
        {
            TCPClient tcp;
            try
            {
                var socket = _socket.Accept();
                tcp = new TCPClient(socket);
            }
            catch (Exception)
            {
                throw new Exception("Ошибка при установлении соединения с клиентом");
            }

            return tcp;
        }
    }
}