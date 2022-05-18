using Agario.Networking;
using UnityEngine;

namespace Agario.UI
{
    public class QuitGame : MonoBehaviour
    {
        public void QuitApplication()
        {
            Client.instance.tcp.Disconnect();
            Application.Quit();
        }
    }
}
