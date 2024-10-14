using Assets._project.CodeBase.Sounds;
using Assets._project.Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private BallManager _ballManager;
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private List<Ball> _balls;
        [SerializeField] private List<Point> _cells;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private List<SideWall> _sideWalls;
        [SerializeField] private SoundHandler _soundHandler;

        private void Awake()
        {
            PlayerInput input = new(_player);

            _ballManager.Construct(_balls);
            _player.Construct(_gameConfig.PlayerData, _ballManager, input, _sideWalls);

            GridZone gridZone = new (_gameConfig.ManagerData, _startPosition);

            _gridManager.Construct(gridZone, _gameConfig.ManagerData, _ballManager, _cells);
            InitBall();
            InitSound();
        }

        private void Start()
        {
            _soundHandler.PlayBurst();
            _soundHandler.PlayBackground();
        }

        private void InitSound()
        {
            _soundHandler.Initialize();
        }

        private void InitBall()
        {
            foreach (var ball in _balls)
                ball.Construct(_gridManager, _gameConfig.BallData, _ballManager, _player);
        }
    }
}
