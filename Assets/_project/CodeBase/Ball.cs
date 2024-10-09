using System;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private TriggerBall _triggerBall;

        private Point _currentPoint;

        [field: SerializeField] public TypeBallColor TypeBallColor { get; private set; }

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
            if (_currentPoint != null)
            {
                Debug.Log("_currentPoint - " + _currentPoint.name + " - " + this.TypeBallColor);
                _currentPoint.FreeCell();
                _currentPoint = null;
            }
            else
            {
                Debug.LogWarning("Ball has no current point assigned!");
            }
        }
    }

    public enum TypeBallColor
    {
        Green,
        Red,
        Blue,
        Yellow
    }
}
