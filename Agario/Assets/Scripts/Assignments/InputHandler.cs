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
            OutputField = GameObject.Find("Output Text").GetComponent<TMP_Text>();
            InputField = FindObjectOfType<TMP_InputField>();
        }
    }
}
