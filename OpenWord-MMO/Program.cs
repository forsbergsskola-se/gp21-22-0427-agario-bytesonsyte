using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OpenWord_MMO;

internal static class OpenGameServer
{
    private const byte MaxMessageCharSize = 20;

    private static void Main()
    {
        Console.WriteLine("Server initializing");
        var remoteEP = new IPEndPoint(IPAddress.Loopback, 3333);
        var udpClient = new UdpClient(remoteEP);

        Console.WriteLine($"Server set up at port {remoteEP.Port.ToString()}");
        Console.WriteLine("Server ready & waiting to receive");

        while (true)
        {
            IPEndPoint? remoteEndpoint = default;
            var data = udpClient.Receive(ref remoteEndpoint);
            var messageString = Encoding.ASCII.GetString(data).Trim();
            Console.WriteLine($"Number of characters of received message: {data.Length}");

            if (data.Length > MaxMessageCharSize || messageString.Any(char.IsWhiteSpace))
            {
                Console.WriteLine("Error: message is over 20 characters or contained a whitespace");
                continue;
            }
        }
    }
}