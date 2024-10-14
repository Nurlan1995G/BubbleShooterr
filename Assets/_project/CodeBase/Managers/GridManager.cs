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
            FillGridWithBalls();
        }

        public void PlaceBallAtNearestFreePoint(Ball ball)
        {
            Point nearestFreePoint = FindNearestFreePoint(ball.transform.position);

            if (nearestFreePoint != null)
                nearestFreePoint.PlaceBall(ball);  
        }

        public Point FindNearestFreePoint(Vector3 position)
        {
            Point nearestPoint = null;
            float minDistance = float.MaxValue;

            foreach (Point point in _cells)
            {
                if (!point.IsBusy)
                {
                    float distance = Vector3.Distance(position, point.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestPoint = point;
                    }
                }
            }

            return nearestPoint;
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

        private void FillGridWithBalls()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                if (i <= _managerData.TotalBallsToLoad)
                {
                    if (!_cells[i].IsBusy)
                    {
                        Ball ball = _ballManager.GetRandomBall();

                        if (ball != null)
                            _cells[i].PlaceBall(ball);
                    }
                }
            }
        }
    }
}
