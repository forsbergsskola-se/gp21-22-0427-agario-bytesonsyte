using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class RequestServerTime : MonoBehaviour
{
    readonly IPEndPoint Endpoint = new (IPAddress.Loopback, 4444); // originally used loopback
    public void SendRequest()
    {
        var tcpClient = new TcpClient(Endpoint);
        var stream = tcpClient.GetStream();
    }
}
