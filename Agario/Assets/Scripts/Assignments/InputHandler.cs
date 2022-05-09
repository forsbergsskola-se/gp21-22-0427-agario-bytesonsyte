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
        [SerializeField] private const int MaxCharSize = 20;
        private void Awake()
        {
            OutputField = GameObject.FindGameObjectWithTag("Output").GetComponent<TMP_Text>();
            InputField = FindObjectOfType<TMP_InputField>();
            SavedText = "";
        }

        public void OutputText()
        {
            //var remoteEndpoint = new IPEndPoint(IPAddress.Loopback, 3333);
            //var udpClient = new UdpClient(remoteEndpoint);
            //udpClient.Connect(remoteEndpoint);

            var input = InputField.text;
            //var inputBytes = Encoding.ASCII.GetBytes(InputField.text);
            //udpClient.Send(input, input.Length);

            if (input.Length is > MaxCharSize or 0 || input.Any(char.IsWhiteSpace))
                InputField.text = "Error";
            else
            {
                SavedText = SavedText + input + " ";
                OutputField.text = SavedText;
                InputField.text = ""; // clear field
            }
        }
    }
}
