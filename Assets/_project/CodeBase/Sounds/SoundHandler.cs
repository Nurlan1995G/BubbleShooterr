using UnityEngine;

namespace Assets._project.CodeBase.Sounds
{
    public class SoundHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _background;
        [SerializeField] private AudioSource _win;
        [SerializeField] private AudioSource _lose;
        [SerializeField] private AudioSource _buy;

        public static SoundHandler Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start() => PlayBackground();

        public void PlayWin()
        {
            if (_win != null)
                _win.Play();
            else
                Debug.LogWarning("Win sound not assigned!");
        }

        public void PlayLose()
        {
            if (_lose != null)
                _lose.Play();
            else
                Debug.LogWarning("Lose sound not assigned!");
        }

        public void PlayBuy()
        {
            if (_buy != null)
                _buy.Play();
            else
                Debug.LogWarning("Buy sound not assigned!");
        }

        private void PlayBackground()
        {
            if (_background != null)
                _background.Play();
            else
                Debug.LogWarning("Background sound not assigned!");
        }
    }
}
