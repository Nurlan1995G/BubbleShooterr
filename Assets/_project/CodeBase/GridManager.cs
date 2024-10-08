using Assets._project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GridManager : MonoBehaviour
    {
        private BallManager _ballManager;
        private GridZone _gridZone;
        private ManagerData _managerData;
        private List<Point> _cells; 

        public void Construct(GridZone gridZone, ManagerData managerData, BallManager ballManager, 
            List<Point> cells)
        {
            _gridZone = gridZone;
            _managerData = managerData;
            _ballManager = ballManager;
            _cells = cells;

            InitializeCells();
            FillGridWithRandomBalls();
        }

        private void InitializeCells()
        {
            int index = 0;

            for (int row = 0; row < _managerData.TotalRows; row++)
            {
                for (int col = 0; col < _managerData.TotalColumns; col++)
                {
                    if (index >= _cells.Count)
                        break;

                    Vector3 position = _gridZone.GetCellPosition(row, col);
                    _cells[index].transform.position = position;
                    index++;
                }
            }
        }

        private void FillGridWithRandomBalls()
        {
            foreach (Point cell in _cells)
            {
                if (!cell.IsBusy)  
                {
                    Ball ball = _ballManager.GetRandomBall();

                    if (ball != null)
                        cell.PlaceBall(ball);
                }
            }
        }
    }
}
