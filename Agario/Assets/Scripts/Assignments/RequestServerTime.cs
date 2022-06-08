using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

public class RequestServerTime : MonoBehaviour
{
    private TMP_Text Output;
    private const int port = 3333;
    private readonly string loopback = IPAddress.Loopback.ToString();

    public void Awake()
    {
        Output = GameObject.Find("Time Output Text").GetComponent<TMP_Text>();
    }

    public void SendRequest()
    {
        var TcpClient = new TcpClient(loopback, port);
        TcpClient.Connect(loopback, port+1);
        
        var clientID = TcpClient.Client.RemoteEndPoint;
        Debug.Log(clientID != null ? $"Client {clientID} connected" : "Error connecting to client");

        //var streamWriter = new StreamWriter(Stream);
        var Stream = TcpClient.GetStream();
        var BufferSize = new byte[TcpClient.ReceiveBufferSize];
        Stream.Read(BufferSize, 0, BufferSize.Length);
        
        var Response = Encoding.ASCII.GetString(BufferSize);
        Debug.Log(Response);
        Output.text = $"Current time: {Response}";
        
        TcpClient.Close();
    }
}
