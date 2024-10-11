using Assets._project.Config;
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
        [SerializeField] private CameraRotate _cameraRotate;

        private void Awake()
        {
            PlayerInput input = new();

            _ballManager.Construct(_balls);
            _player.Construct(_ballManager, input, _cameraRotate);
            _cameraRotate.Construct(_player);

            GridZone gridZone = new (_gameConfig.ManagerData, _startPosition);

            _gridManager.Construct(gridZone, _gameConfig.ManagerData, _ballManager, _cells);
            InitBall();
        }

        private void InitBall()
        {
            foreach (var ball in _balls)
                ball.Construct(_gridManager, _gameConfig.BallData, _ballManager);
        }
    }
}
