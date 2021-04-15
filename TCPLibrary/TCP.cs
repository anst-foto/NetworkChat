using System;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPLibrary
{
    public abstract class TCP
    {
        protected readonly IPAddress _ip;
        protected readonly int _port;
        protected readonly IPEndPoint _ipEndPoint;
        protected readonly Socket _socket;

        protected TCP()
        {
            _ip = IPAddress.Parse("127.0.0.1");
            _port = 8005;
            _ipEndPoint = new IPEndPoint(_ip, _port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        protected TCP(string ipAddress, int port)
        {
            if (port <= 0 || port > 65535) //TODO Переработать условие на проверку зарезервированности порта
            {
                throw new Exception("Неправильный номер порта");
            }
            else
            {
                _port = port;
            }

            try
            {
                _ip = IPAddress.Parse(ipAddress); //TODO Проверка правильности IP-адреса
            }
            catch (ArgumentNullException)
            {
                throw new Exception("Пустая строка IP-адреса");
            }
            catch (FormatException)
            {
                throw new Exception("Неправильный IP-адрес");
            }

            _ipEndPoint = new IPEndPoint(_ip, _port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        protected TCP(Socket socket)
        {
            if (socket == null)
            {
                throw new Exception("Пустой сокет");
            }
            else
            {
                _socket = socket;
            }
        }

        public void Close()
        {
            try
            {
                _socket.Shutdown(SocketShutdown.Both);
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("Socket был закрыт");
            }
            catch (SocketException)
            {
                throw new Exception("Произошлп ошибка при попытке досткпа к сокету.");
            }
            _socket.Close();
        }

        public void SendMessage(string message)
        {

            try
            {
                var buffer = Encoding.Unicode.GetBytes(message);
                _socket.Send(buffer);
            }
            catch (ArgumentNullException)
            {
                throw new Exception("Пустое значение параметра");
            }
            catch (EncoderFallbackException)
            {
                throw new Exception("Ошибка кодировки строки");
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("Socket был закрыт");
            }
            catch (SocketException)
            {
                throw new Exception("Произошлп ошибка при попытке досткпа к сокету.");
            }
            
        }

        public string ReceiveMessage()
        {
            StringBuilder message;
            try
            {
                message = new StringBuilder();
                var buffer = new byte[256];

                do
                {
                    var bytes = _socket.Receive(buffer);
                    message.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
                } while (_socket.Available > 0);
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("Socket был закрыт");
            }
            catch (SocketException)
            {
                throw new Exception("Произошлп ошибка при попытке досткпа к сокету.");
            }
            catch (DecoderFallbackException)
            {
                throw new Exception("Ошибка кодировки строки");
            }
            catch (ArgumentException)
            {
                throw new Exception("Массив байтов содержит недопустимые кодовые точки Юникода");
            }

            return message.ToString();
        }
    }
}