using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class BallManager : MonoBehaviour
    {
        private Dictionary<string, List<Ball>> _ballsByColor; 
        private List<Ball> _availableBalls;

        public void Construct(List<Ball> balls)
        {
            _availableBalls = balls;

            PopulateBallsByColor();
        }

        private void PopulateBallsByColor()
        {
            _ballsByColor = new Dictionary<string, List<Ball>>();

            foreach (Ball ball in _availableBalls)
            {
                if (!_ballsByColor.ContainsKey(ball.ColorBall))
                    _ballsByColor.Add(ball.ColorBall, new List<Ball>());

                _ballsByColor[ball.ColorBall].Add(ball);
            }
        }

        public Ball GetRandomBall()
        {
            if (_availableBalls.Count > 0)
            {
                int randomIndex = Random.Range(0, _availableBalls.Count);
                Ball ball = _availableBalls[randomIndex];
                _availableBalls.RemoveAt(randomIndex);
                return ball;
            }

            return null;
        }

        public Ball GetBallByColor(string color)
        {
            if (_ballsByColor.ContainsKey(color) && _ballsByColor[color].Count > 0)
            {
                Ball ball = _ballsByColor[color][0];
                _ballsByColor[color].RemoveAt(0);
                _availableBalls.Remove(ball);
                return ball;
            }

            return null;
        }

        public void ReturnBallToManager(Ball ball)
        {
            if (!_ballsByColor.ContainsKey(ball.ColorBall))
                _ballsByColor.Add(ball.ColorBall, new List<Ball>());

            _ballsByColor[ball.ColorBall].Add(ball);
            _availableBalls.Add(ball);
            ball.gameObject.SetActive(false); 
        }
    }
}
