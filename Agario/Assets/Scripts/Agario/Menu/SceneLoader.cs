using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agario.Menu
{
    public class SceneLoader : MonoBehaviour
    {
        public void ChangeScene(string SceneName)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
