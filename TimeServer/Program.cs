using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TimeServer
{
    public static class TCPListenerProgram
    {
        private static void Main(string[] args)
        {
            var endpoint = new IPEndPoint(IPAddress.Loopback, 44444); // server IP = 127.0.0.1
            var tcpListener = new TcpListener(endpoint);
            if (true)
                tcpListener.Start();

            while (true)
            {
                IterationOne(tcpListener);
            }
        }

        private static void IterationOne(TcpListener tcpListener)
        {
            var bufferSize = new byte[100];
            var acceptTcpClient = tcpListener.AcceptTcpClient();
            acceptTcpClient.GetStream().Read(bufferSize, 0, 100);
                
            var comment = Encoding.ASCII.GetString(bufferSize);
            Console.WriteLine($"New comment from Client: {comment}");
                
            var currentTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString(CultureInfo.CurrentCulture));
            acceptTcpClient.GetStream().Write(currentTime, 0, currentTime.Length);
                
            acceptTcpClient.Close();
        }
    }
}

