using Assets._project.CodeBase.Sounds;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class TriggerBall : MonoBehaviour
    {
        private Ball _currentBall;
        private GridManager _gridManager;
        private BallManager _ballManager;
        private Point _point;
        private Player _player;

        public void Construct(Ball ball, GridManager gridManager, BallManager ballManager, Player player)
        {
            _currentBall = ball ?? throw new System.ArgumentNullException(nameof(ball));
            _gridManager = gridManager ?? throw new System.ArgumentNullException(nameof(gridManager));
            _ballManager = ballManager;
            _player = player;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Ball otherBall))
            {
                if (_currentBall.TypeBallColor == otherBall.TypeBallColor)
                    CheckForMatch(otherBall);
                else
                {
                    if (_currentBall.GetCurrentPoint() == null)
                        SetPointToBall(_currentBall);
                }
            }
        }

        private void CheckForMatch(Ball otherBall)
        {
            List<Ball> matchingBalls = new List<Ball>();
            FindMatchingBalls(_currentBall, ref matchingBalls);

            if (matchingBalls.Count == 2)
                SetPointToBall(_currentBall);

            if (matchingBalls.Count > 2)
            {
                foreach (Ball ball in matchingBalls)
                {
                    _player.AddScore(10);
                    ball.RemoveFromCurrentPoint();
                    _ballManager.AddAfterReset(ball);
                }

                SoundHandler.Instance.PlayBurst();
            }
        }

        private void SetPointToBall(Ball ball)
        {
            if(ball != null)
            {
                ball.StoppingMoving();
                _gridManager.PlaceBallAtNearestFreePoint(ball);
            }
        }

        private void FindMatchingBalls(Ball currentBall, ref List<Ball> matchingBalls)
        {
            if (!matchingBalls.Contains(currentBall))
            {
                matchingBalls.Add(currentBall);
                Collider2D[] nearbyBalls = Physics2D.OverlapCircleAll(currentBall.transform.position, 1.0f);

                foreach (var collider in nearbyBalls)
                {
                    Ball nextBall = collider.GetComponent<Ball>();

                    if (nextBall != null && nextBall.TypeBallColor == currentBall.TypeBallColor)
                        FindMatchingBalls(nextBall, ref matchingBalls);
                }
            }
        }
    }
}
