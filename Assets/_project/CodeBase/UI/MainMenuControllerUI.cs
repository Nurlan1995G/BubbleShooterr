using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._project.CodeBase
{
    public class MainMenuControllerUI : MonoBehaviour
    {
        [SerializeField] private GameObject _descriptionPanel;

        public void OnNewGameButton() =>
            SceneManager.LoadScene("Gameplay");

        public void OnDescriptionButton() =>
            _descriptionPanel.SetActive(true);

        public void OnBackToMenuButton() =>
            _descriptionPanel.SetActive(false);

        public void OnExitButton()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}

