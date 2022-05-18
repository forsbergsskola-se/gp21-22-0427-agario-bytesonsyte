using System;
using System.Net.Sockets;
using UnityEngine;

namespace Agario.Networking
{
    public class Client : MonoBehaviour
    {
        public static Client instance;
        public static int dataBufferSize = 4096; //4MB
        
        public string localIP = "127.0.0.1";
        public int port = 5757; // random port value used in Server
        public int playerID = 0;
        public TCP tcp;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            
            else if (instance != this)
            {
                Debug.Log("This instance already exits, so is being destroyed.");
                Destroy(this); // only one instance can exist
            }
        }

        private void Start()
        {
            tcp = new TCP();
        }


        public class TCP
        {
            private TcpClient socket;
            private NetworkStream stream;
            private byte[] receiveBuffer;

            public void ConnectPlayerToServer()
            {
                socket = new TcpClient
                {
                    ReceiveBufferSize = dataBufferSize, 
                    SendBufferSize = dataBufferSize
                };

                receiveBuffer = new byte[dataBufferSize];
                socket.BeginConnect(instance.localIP, instance.port, TCPConnectCallback, socket);
            }

            
            
            private void TCPConnectCallback(IAsyncResult ar)
            {
                socket.EndConnect(ar); // connect with client
                if (!socket.Connected)
                    return;

                stream = socket.GetStream(); // start streaming if successfully connected
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveServerCallback, null);
            }

            
            
            private void ReceiveServerCallback(IAsyncResult ar)
            {
                try
                {
                    var byteLength = stream.EndRead(ar); // no of bytes read from the stream
                    if (byteLength <= 0)
                        return;

                    var data = new byte[byteLength]; // bytes stored here if received
                    Array.Copy(receiveBuffer, data, byteLength);
                    
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveServerCallback, null); // continue reading data from the stream

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error receiving TCP data: {e}");
                    throw;
                }
            }
        }
    }
}
