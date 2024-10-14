using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._project.CodeBase
{
    public class MainMenuGameUI : MonoBehaviour
    {
        public void OnExitButton() => 
            SceneManager.LoadScene("Menu");
    }
}

