using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class BallManager : MonoBehaviour
    {
        private Dictionary<string, List<Ball>> _ballsByColor; 
        private List<Ball> _balls;

        public void Construct(List<Ball> balls)
        {
            _balls = balls;

            PopulateBallsByColor();
        }

        public Ball GetRandomBall()
        {
            if (_balls.Count > 0)
            {
                int randomIndex = Random.Range(0, _balls.Count);
                Ball ball = _balls[randomIndex];
                _balls.RemoveAt(randomIndex);
                return ball;
            }

            return null;
        }

        public void AddAfterReset(Ball ball)
        {

        }

        private void PopulateBallsByColor()
        {
            _ballsByColor = new Dictionary<string, List<Ball>>();

            foreach (Ball ball in _balls)
            {
                if (!_ballsByColor.ContainsKey(ball.ColorBall))
                    _ballsByColor.Add(ball.ColorBall, new List<Ball>());

                _ballsByColor[ball.ColorBall].Add(ball);
            }
        }
    }
}
