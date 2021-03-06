using System.Net.Sockets;

namespace Agario_Server
{
    public class AgarioClient
    {
        public static int dataBufferSize = 4096; // 4MB
        public int playerId;
        public TCP tcp;

        public AgarioClient(int clientId)
        {
            playerId = clientId;
            tcp = new TCP(playerId);
        }
        
        
        public class TCP
        {
            public TcpClient socket;
            private readonly int playerID;
            private NetworkStream stream;
            private byte[] receiveBuffer;

            public TCP(int id) => playerID = id; // constructor 

            public void ConnectClient(TcpClient clientSocket)
            {
                socket = clientSocket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();
                receiveBuffer = new byte[dataBufferSize];
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }

            public void Disconnect()
            {
                socket.Close();
                stream = null;
                receiveBuffer = null;
                socket = null;
                Console.WriteLine($"Player with ID '{playerID}' disconnected");
                AgarioServer.clients[playerID].TCPDisconnect();
            }

            private void ReceiveCallback(IAsyncResult ar)
            {
                try
                {
                    var byteLength = stream.EndRead(ar); // no of bytes read from the stream
                    if (byteLength <= 0)
                        AgarioServer.clients[playerID].TCPDisconnect();


                    var data = new byte[byteLength]; // bytes stored here if received
                    Array.Copy(receiveBuffer, data, byteLength);
                    
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null); // continue reading data from the stream

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error receiving TCP data: {e}");
                    AgarioServer.clients[playerID].TCPDisconnect();
                }
            }


        }

        public void TCPDisconnect()
        {
            Console.WriteLine($"{tcp.socket.Client.RemoteEndPoint} has disconnected.");
            tcp.Disconnect();
        }

    }

}
