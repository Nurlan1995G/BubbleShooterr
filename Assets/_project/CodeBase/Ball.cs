using System;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private TriggerBall _triggerBall;

        private Point _currentPoint;

        [field: SerializeField] public BallColorType BallColorType { get; private set; }
        [field: SerializeField] public string ColorBall { get; private set; }

        public void Construct(GridManager gridManager)
        {
            if (gridManager is null)
            {
                throw new ArgumentNullException(nameof(gridManager));
            }

            _triggerBall.Construct(this, gridManager);
        }

        public void MoveBall(Vector3 direction, float force)
        {
            _rigidbody2D.isKinematic = false;  
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.velocity = direction * force; 
        }

        public void StoppingMoving()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.isKinematic = true;
        }

        public void SetCurrentPoint(Point point) => 
            _currentPoint = point;

        public Point GetCurrentPoint() =>
            _currentPoint;

        public void RemoveFromCurrentPoint()
        {
            _currentPoint.FreeCell();
            _currentPoint = null;
        }
    }

    public enum BallColorType
    {
        Green,
        Red,
        Blue,
        Yellow
    }
}
