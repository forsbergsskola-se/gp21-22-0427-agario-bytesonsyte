using Agario.Menu;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void ChangeScene(string SceneName)
    {
        var playerName = FindObjectOfType<NameManager>().PlayerName;
        
        if (playerName != "Enter Name...")
            SceneManager.LoadScene(SceneName);
    }
}
