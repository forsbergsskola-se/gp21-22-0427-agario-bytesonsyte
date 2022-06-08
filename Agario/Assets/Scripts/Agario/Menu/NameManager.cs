using TMPro;
using UnityEngine;

namespace Agario.Menu
{
    public class NameManager : MonoBehaviour
    {
        private TMP_InputField inputField;
        public string PlayerName;

        private void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
        }

        private void Update()
        {
            PlayerName = inputField.text;
            PlayerPrefs.SetString("Player Name", PlayerName);
        }
    }
}
