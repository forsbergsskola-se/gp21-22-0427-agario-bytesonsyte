using Agario.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agario.Networking
{
    public class ConnectToServer : MonoBehaviour
    {
        private static ConnectToServer instance;
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
        
        
        
        public void ConnectPlayerToServer(string SceneName)
        {
            var playerName = FindObjectOfType<NameManager>().PlayerName;
            if (playerName == "Enter Name...") return;
            
            Client.instance.tcp.ConnectPlayerToServer();
            SceneManager.LoadScene(SceneName);
        }
    }
}
