using System;
using System.Linq;
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
        [SerializeField] private String SavedText;
        private const int MaxCharSize = 20;
        private void Awake()
        {
            OutputField = GameObject.FindGameObjectWithTag("Output").GetComponent<TMP_Text>();
            InputField = FindObjectOfType<TMP_InputField>();
            SavedText = "";
        }

        public void OutputText()
        {
            var remoteEndpoint = new IPEndPoint(IPAddress.Loopback, 3333);
            var udpClient = new UdpClient(remoteEndpoint);
            udpClient.Connect(remoteEndpoint);

            var input = InputField.text;
            var inputBytes = Encoding.ASCII.GetBytes(InputField.text);

            if (input.Length is > MaxCharSize or 0 || input.Any(char.IsWhiteSpace))
            {
                InputField.text = "Error";
                var errorMessage = Encoding.ASCII.GetBytes("Incorrect formatting. Must be under 20 characters and have no whitespaces");
                udpClient.Send(errorMessage, errorMessage.Length);
            }
            else
            {
                SavedText = SavedText + input + " ";
                OutputField.text = SavedText;
                InputField.text = ""; // clear field
                
                udpClient.Send(inputBytes, input.Length);
            }
        }
    }
}
