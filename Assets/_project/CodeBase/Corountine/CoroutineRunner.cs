using System.Collections;
using UnityEngine;

namespace Assets._project.CodeBase.Corountine
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static CoroutineRunner Instance { get; private set; }

        public void Initialize()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        public Coroutine RunCoroutine(IEnumerator coroutine) =>
            StartCoroutine(coroutine);

        public void Stop(Coroutine coroutine) =>
            StopCoroutine(coroutine);
    }
}
