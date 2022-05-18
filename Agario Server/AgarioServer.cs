using System.Net;
using System.Net.Sockets;

namespace Agario_Server
{
    public class AgarioServer
    {
        private static int MaxPlayerCount { get; set; }
        private static int Port { get; set; }
        private static TcpListener? tcpListener;
        private readonly StreamWriter StreamWriter;
        private static readonly Dictionary<int, AgarioClient> clients = new Dictionary<int, AgarioClient>(); // stores clients using their IDs as  keys


        public static void ServerStart(int maxPlayers, int port)
        {
            MaxPlayerCount = maxPlayers;
            Port = port;
            Console.WriteLine("Booting up server...");

            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Server successfully started on port {Port}...");

            
        }

        private static void TCPConnectCallback(IAsyncResult ar)
        {
            var client = tcpListener?.EndAcceptTcpClient(ar); // accept tcp client
            var clientEP = client?.Client.RemoteEndPoint;
            tcpListener?.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null); // continue listening for connections

            Console.WriteLine($"Connection request from {clientEP}...");

            for (var i = 1; i <= MaxPlayerCount; i++)
            {
                if (clients[i].tcp.socket != null) continue; // if slots are all full proceed to server full message
                clients[i].tcp.ConnectClient(client); // connect new client to empty slot

                Console.WriteLine($"Succesfully connected {clientEP} to server...");
                return; // new client only takes up 1 open slot
            }
            Console.WriteLine($"{clientEP} failed to connect: server full...");
        }

        private static void InitializeServerData() // fill dictionary
        {
            for (var i = 1; i <= MaxPlayerCount; i++)
            {
                clients.Add(i, new AgarioClient(i));
            }
        }
    }
}
