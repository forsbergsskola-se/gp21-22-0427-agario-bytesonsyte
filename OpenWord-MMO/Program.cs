using System.Net;
using System.Net.Sockets;

namespace OpenWord_MMO;

internal static class OpenGameServer
{
    private static void Main()
    {
        Console.WriteLine("Server initializing");
        var endpoint = new IPEndPoint(IPAddress.Loopback, 3333);
        var udpClient = new UdpClient(endpoint);

        Console.WriteLine($"Server set up at port {endpoint.Port.ToString()}");
        Console.WriteLine("Server ready & waiting to receive");

        while (true)
        {
            IPEndPoint? remoteEndpoint = default;
            var udpData = udpClient.Receive(ref remoteEndpoint);
        }
    }
}