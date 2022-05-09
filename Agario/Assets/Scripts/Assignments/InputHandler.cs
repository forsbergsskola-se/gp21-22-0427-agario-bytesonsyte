using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assignments
{
    public class InputHandler : MonoBehaviour
    {
        private TMP_Text OutputField;
        private TMP_InputField InputField;
        private void Awake()
        {
            OutputField = GameObject.FindGameObjectWithTag("Output").GetComponent<TMP_Text>();
            InputField = FindObjectOfType<TMP_InputField>();
        }

        public void OutputText()
        {
            //var remoteEndpoint = new IPEndPoint(IPAddress.Loopback, 3333);
            //var udpClient = new UdpClient(remoteEndpoint);
            //udpClient.Connect(remoteEndpoint);

            var input = InputField.text;
            //var inputBytes = Encoding.ASCII.GetBytes(InputField.text);
            //udpClient.Send(input, input.Length);

            OutputField.text = input.ToString();
            InputField.text = ""; // clear field
        }
    }
}
