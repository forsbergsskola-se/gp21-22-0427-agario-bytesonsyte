﻿using System.Net;
using System.Net.Sockets;
using System.Text;

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
                var bufferSize = new byte[100];
                var acceptTcpClient = tcpListener.AcceptTcpClient();
                acceptTcpClient.GetStream().Read(bufferSize, 0, 100);
                
                var comment = Encoding.ASCII.GetString(bufferSize);
                Console.WriteLine($"New comment from Client: {comment}");

            }
        }

    }
}

