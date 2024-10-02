using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Ball : MonoBehaviour
    {
        private bool _isBusy;

        [field: SerializeField] public string ColorBall { get; private set; }
        public bool IsBusy => _isBusy;

        public void SetBusy(bool isBusy) =>
            _isBusy = isBusy;
    }
}
