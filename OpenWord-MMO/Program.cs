using System.Net;
using System.Net.Sockets;

namespace OpenWord_MMO;

internal static class OpenGameServer
{
    private static void Main()
    {
        var endpoint = new IPEndPoint(IPAddress.Loopback, 3333);
        var server = new UdpClient(endpoint);
        
        Console.WriteLine("The server is starting...");
    }
}