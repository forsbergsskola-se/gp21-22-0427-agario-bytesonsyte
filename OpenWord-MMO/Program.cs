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


            if (data.Length is > MaxMessageCharSize or 0 || messageString.Any(char.IsWhiteSpace))
            {
                Console.WriteLine($"Error: {udpClient}'s message is empty, over 20 characters or contained a whitespace");
                    
                var errorMessage = Encoding.ASCII.GetBytes("Error. Please adhere to the formatting rules when sending your message");
                udpClient.Send(errorMessage);
            }
            else
                Console.WriteLine($"Accepted message \nNumber of characters of received message: {data.Length} ");
        }
    }
}