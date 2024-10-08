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

        private void Awake()
        {
            _ballManager.Construct(_balls);
            _player.Construct(_ballManager);

            GridZone gridZone = new (_gameConfig.ManagerData, _startPosition);

            _gridManager.Construct(gridZone, _gameConfig.ManagerData, _ballManager, _cells);
        }
    }
}
