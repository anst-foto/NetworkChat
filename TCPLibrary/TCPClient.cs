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
            _socket.Connect(_ipEndPoint);
        }
    }
}