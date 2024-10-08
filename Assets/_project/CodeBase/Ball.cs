using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Ball : MonoBehaviour
    {
        private Vector2 _velocity;

        [field: SerializeField] public Rigidbody2D Rigidbody2D;
        [field: SerializeField] public string ColorBall { get; private set; }

        public void MoveBall(Vector3 direction, float force)
        {
            _velocity = direction * force;
            Rigidbody2D.isKinematic = false;  
            Rigidbody2D.gravityScale = 0;
            Rigidbody2D.velocity = direction * force; 
        }
    }
}
