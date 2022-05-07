using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RequestServerTime : MonoBehaviour
{
    public void SendRequest()
    {
        var Endpoint = new IPEndPoint(IPAddress.Loopback, 44);
        var TcpClient = new TcpClient(Endpoint);
        var Stream = TcpClient.GetStream();
        
        var bufferSize = new byte[TcpClient.ReceiveBufferSize];
        Stream.Read(bufferSize, 0, bufferSize.Length);
        
        var response = Encoding.ASCII.GetString(bufferSize);
        Debug.Log(response);
        // print to text
        TcpClient.Close();
    }
}
