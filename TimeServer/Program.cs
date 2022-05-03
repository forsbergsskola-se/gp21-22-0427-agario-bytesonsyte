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
            const bool any = false;
            
            var anyEndpoint = new IPEndPoint(IPAddress.Any, 44444); // listen on any given socket
            var loopbackEndpoint = new IPEndPoint(IPAddress.Loopback, 44444); // listen on loopback... server IP = 127.0.0.1
            
            var tcpListener = new TcpListener(loopbackEndpoint);
            if (any) // toggle
                tcpListener = new TcpListener(anyEndpoint);
            tcpListener.Start();

            Console.WriteLine($"Server listening on : {tcpListener.LocalEndpoint}");
            
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

