using Assets._project.CodeBase.Interface;
using Assets._project.Config;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Ball : MonoBehaviour, IBallControll
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private TriggerBall _triggerBall;

        private Point _currentPoint;

        [field: SerializeField] public TypeBallColor TypeBallColor { get; private set; }

        public void Construct(GridManager gridManager, LogicData logicData, BallManager ballManager, Player player)
        {
            _triggerBall.Construct(this, gridManager, ballManager, player, logicData);
        }

        public void MoveBall(Vector3 direction)
        {
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.velocity = direction;
        }

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public void StoppingMoving()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.isKinematic = true;
        }

        public void Activate() =>
            gameObject.SetActive(true);

        public void Deactivate() =>
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
        }

        public Vector2 GetBallSpeed() => 
            _rigidbody2D.velocity;
    }

    public enum TypeBallColor
    {
        Green,
        Red,
        Blue,
        Yellow
    }
}
