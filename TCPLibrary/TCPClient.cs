using System;
using System.Net.Sockets;

namespace TCPLibrary
{
    public class TCPClient : TCP
    {
        public TCPClient() : base() {}

        public TCPClient(string ipAddress, int port) : base(ipAddress, port) {}

        public TCPClient(Socket socket) : base(socket) { }
        
        public bool Connect()
        {
            try
            {
                _socket.Connect(_ipAddress);
            }
            catch (SocketException)
            {
                return false;
            }
        
            return true;
        }
    }
}