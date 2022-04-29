using System.Net;
using System.Net.Sockets;
namespace TimeServer
{
    public static class TCPListenerProgram
    {
        static void Main(string[] args)
        {
            var endpoint = new IPEndPoint(IPAddress.Loopback, 44444); // server IP = 127.0.0.1
            var tcpListener = new TcpListener(endpoint);
            if (true)
                tcpListener.Start();

            while (true)
            {
                var acceptTcpClient = tcpListener.AcceptTcpClient();
                acceptTcpClient.GetStream().Read(new byte[100], 0, 100);
            }
        }

    }
}

