using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._project.CodeBase
{
    public class MainMenuGame : MonoBehaviour
    {
        public void OnExitButton() => 
            SceneManager.LoadScene("Menu");
    }
}

