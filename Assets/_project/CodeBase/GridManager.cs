using Assets._project.Config;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GridManager : MonoBehaviour
    {
        private BallManager _ballManager;
        private GridZone _gridZone;
        private ManagerData _managerData;

        public void Construct(GridZone gridZone, ManagerData managerData, BallManager ballManager)
        {
            _gridZone = gridZone;
            _managerData = managerData;
            _ballManager = ballManager;

            FillGridWithRandomBalls();
        }

        private void FillGridWithRandomBalls()
        {
            for (int row = 0; row < _managerData.RowsToFill; row++)
            {
                for (int col = 0; col < _managerData.TotalColumns; col++)
                {
                    Ball ball = _ballManager.GetRandomBall();

                    if (ball != null)
                    {
                        Vector3 position = _gridZone.GetCellPosition(row, col);
                        ball.transform.position = position;
                        ball.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
