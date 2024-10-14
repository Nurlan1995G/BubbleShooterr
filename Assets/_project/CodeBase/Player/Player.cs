using Assets._project.Config;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCountBall;
        [SerializeField] private TextMeshProUGUI _scoreText;

        private PlayerUI _playerUI;
        private PlayerShoot _playerShoot;
        private PlayerInput _input;

        private int _remainingBalls;
        private int _score;

        [field: SerializeField] public LineRenderer LineRenderer { get; private set; }
        [field: SerializeField] public Transform ShootPosition { get; private set; }
        [field: SerializeField] public Transform NextBallPosition { get; private set; }
        public PlayerData PlayerData { get; private set; }

        public void Construct(PlayerData playerData, PlayerInput input, ParticleSystem effectNextBall, BallManager ballManager, List<SideWall> sideWalls)
        {
            PlayerData = playerData;
            _input = input;

            _remainingBalls = playerData.TotalBall;

            InitializeComponents(input, effectNextBall, ballManager, sideWalls);
            InitializeBalls();
        }

        private void Update()
        {
            _input.Update();

            if (_playerShoot.CanShoot() && _remainingBalls > 0)
                _playerShoot.AimAndShoot();
        }

        public void AddScore(int score)
        {
            _score += score;
            _playerUI.UpdateScoreTextUI(_score);
        }

        public void DecreaseBallCount()
        {
            _remainingBalls--;
            _playerUI.UpdateBallCountUI(_remainingBalls);
        }

        public int GetRemainingBalls() =>
            _remainingBalls;

        private void InitializeComponents(PlayerInput input, ParticleSystem effectNextBall, BallManager ballManager, List<SideWall> sideWalls)
        {
            _playerUI = new PlayerUI(_textCountBall, _scoreText);
            _playerShoot = new PlayerShoot(this, ballManager, input, sideWalls, effectNextBall);
        }

        private void InitializeBalls()
        {
            if (_remainingBalls > 0)
                _playerShoot.PrepareCurrentBall();

            _playerUI.UpdateBallCountUI(_remainingBalls);
        }
    }
}
