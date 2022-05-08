using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OpenWord_MMO;

internal static class OpenGameServer
{
    private const byte MaxMessageCharSize = 20;
    static IPEndPoint remoteEP = new IPEndPoint(IPAddress.Loopback, 3333);
    static UdpClient udpClient = new UdpClient(remoteEP);
    static string response = "";

    private static void Main()
    {
        Console.WriteLine("Server initializing");

        Console.WriteLine($"Server set up at port {remoteEP.Port.ToString()}");
        Console.WriteLine("Server ready & waiting to receive");


        while (true)
        {
            IPEndPoint? remoteEndpoint = default;
            var data = udpClient.Receive(ref remoteEndpoint);
            var messageString = Encoding.ASCII.GetString(data).Trim();


            if (data.Length is > MaxMessageCharSize or 0 || messageString.Any(char.IsWhiteSpace))
            {
                Console.WriteLine($"Denied message:\n{udpClient}'s message is empty, over 20 characters or contained a whitespace");
                
                var errorMessage = Encoding.ASCII.GetBytes("Error. Please adhere to the formatting rules when sending your message");
                udpClient.Send(errorMessage);
            }
            else
            {
                Console.WriteLine($"Accepted message");
                
                var successMessage = Encoding.ASCII.GetBytes($"Accepted message\nNumber of characters of received message: {data.Length}");
                udpClient.Send(successMessage);
            }

            response += " " + messageString;
            udpClient.Send(Encoding.ASCII.GetBytes(response), response.Length, remoteEndpoint);
            Console.WriteLine("Message returned to sender.");
            
        }
        
        Console.WriteLine("Closing...");
        udpClient.Close();
        Console.WriteLine("Closed");
        // ReSharper disable once FunctionNeverReturns
        // Endless loop was the intent
    }
}