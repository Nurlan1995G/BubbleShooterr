using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class BallManager : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;

        private Dictionary<TypeBallColor, List<Ball>> _ballsByColor;
        private List<Ball> _balls;
        private List<Ball> _reserveBalls;  

        public void Construct(List<Ball> balls)
        {
            _balls = new List<Ball>(balls);  
            _reserveBalls = new List<Ball>(); 

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
            else if (_reserveBalls.Count > 0)  
            {
                int randomIndex = Random.Range(0, _reserveBalls.Count);
                Ball ball = _reserveBalls[randomIndex];
                _reserveBalls.RemoveAt(randomIndex);
                return ball;
            }

            return null;  
        }

        public void AddAfterReset(Ball ball)
        {
            ball.transform.position = _startPosition;
            ball.Deactivate();

            if (!_balls.Contains(ball) && !_reserveBalls.Contains(ball))
            {
                _reserveBalls.Add(ball);  
            }
        }

        private void PopulateBallsByColor()
        {
            _ballsByColor = new Dictionary<TypeBallColor, List<Ball>>();

            foreach (Ball ball in _balls)
            {
                if (!_ballsByColor.ContainsKey(ball.TypeBallColor))
                    _ballsByColor.Add(ball.TypeBallColor, new List<Ball>());

                _ballsByColor[ball.TypeBallColor].Add(ball);
            }
        }
    }
}
