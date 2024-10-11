using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class TriggerBall : MonoBehaviour
    {
        private Ball _ball;
        private GridManager _gridManager;
        private BallManager _ballManager;
        private Point _point;

        public void Construct(Ball ball, GridManager gridManager, BallManager ballManager)
        {
            _ball = ball ?? throw new System.ArgumentNullException(nameof(ball));
            _gridManager = gridManager ?? throw new System.ArgumentNullException(nameof(gridManager));
            _ballManager = ballManager;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Ball otherBall))
            {
                if (otherBall.GetCurrentPoint() == null)
                {
                    Debug.LogWarning("У другого мяча нет назначенного очка!");
                    return;
                }

                if (_ball.TypeBallColor == otherBall.TypeBallColor)
                    CheckForMatch(otherBall);
                else
                    SetPointToBall();
            }
        }

        private void CheckForMatch(Ball otherBall)
        {
            List<Ball> matchingBalls = new List<Ball>();
            FindMatchingBalls(_ball, ref matchingBalls);

            if (matchingBalls.Count == 2)
                SetPointToBall();

            if (matchingBalls.Count > 2)
            {
                foreach (Ball ball in matchingBalls)
                {
                    ball.RemoveFromCurrentPoint();
                    _ballManager.AddAfterReset(ball);
                }
            }
        }

        private void SetPointToBall()
        {
            _ball.StoppingMoving();
            _gridManager.PlaceBallAtNearestFreePoint(_ball);
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
