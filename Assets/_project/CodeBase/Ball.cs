using Assets._project.CodeBase.Interface;
using Assets._project.Config;
using System;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Ball : MonoBehaviour, IBallMovement, IBallPosition
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private TriggerBall _triggerBall;

        private BallData _ballData;
        private Point _currentPoint;
        private Vector3 _direction;
        private bool _isMove;

        [field: SerializeField] public TypeBallColor TypeBallColor { get; private set; }

        public void Construct(GridManager gridManager, BallData ballData, BallManager ballManager)
        {
            _ballData = ballData ?? throw new ArgumentNullException(nameof(ballData));

            _triggerBall.Construct(this, gridManager, ballManager);
        }

        public void MoveBall(Vector3 direction)
        {
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.velocity = direction * _ballData.MaxSpeed;
        }

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public void StoppingMoving()
        {
            _isMove = false;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.isKinematic = true;
        }

        public void Activate() =>
            gameObject.SetActive(true);

        public void Diactivate() =>
            gameObject.SetActive(false);

        public void SetCurrentPoint(Point point) => 
            _currentPoint = point;

        public Point GetCurrentPoint() =>
            _currentPoint;

        public void RemoveFromCurrentPoint()
        {
            if (_currentPoint != null)
            {
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
