using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class TriggerBall : MonoBehaviour
    {
        private Ball _ball;
        private GridManager _gridManager;

        private Point _point;

        public void Construct(Ball ball, GridManager gridManager)
        {
            _ball = ball ?? throw new System.ArgumentNullException(nameof(ball));
            _gridManager = gridManager ?? throw new System.ArgumentNullException(nameof(gridManager));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Ball otherBall))
            {
                if (_ball.ColorBall == otherBall.ColorBall)
                    CheckForMatch(otherBall);
                else
                    SetPointToBall(otherBall);
            }
        }

        private void CheckForMatch(Ball otherBall)
        {
            List<Ball> matchingBalls = new List<Ball>();
            FindMatchingBalls(_ball, ref matchingBalls);
            otherBall.RemoveFromCurrentPoint();

            if (matchingBalls.Count > 2)
            {
                foreach (Ball ball in matchingBalls)
                    Destroy(ball.gameObject);
            }
        }

        private void SetPointToBall(Ball ball)
        {
            _gridManager.PlaceBallAtNearestFreePoint(ball);
            _ball.StoppingMoving();
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

                    if (nextBall != null && nextBall.ColorBall == currentBall.ColorBall)
                        FindMatchingBalls(nextBall, ref matchingBalls);
                }
            }
        }
    }
}
