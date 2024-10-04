using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private bool _isBusy;
        private Vector2 _velocity;

        [field: SerializeField] public string ColorBall { get; private set; }
        public bool IsBusy => _isBusy;

        public void SetBusy(bool isBusy) =>
            _isBusy = isBusy;

        public void MoveBall(Vector3 direction, float force)
        {
            _velocity = direction * force;
            _rigidbody2D.isKinematic = false;  
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.velocity = direction * force; 
        }
    }
}
