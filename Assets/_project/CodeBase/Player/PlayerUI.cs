using TMPro;

namespace Assets._project.CodeBase
{
    public class PlayerUI
    {
        private TextMeshProUGUI _textCountBall;
        private TextMeshProUGUI _scoreText;

        public PlayerUI(TextMeshProUGUI textCountBall, TextMeshProUGUI scoreText)
        {
            _textCountBall = textCountBall;
            _scoreText = scoreText;
        }

        public void UpdateBallCountUI(int remainingBalls) =>
            _textCountBall.text = remainingBalls.ToString();

        public void UpdateScoreTextUI(int score) =>
            _scoreText.text = score.ToString();
    }
}
