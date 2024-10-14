using Assets._project.Config;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GridZone
    {
        private ManagerData _managerData;
        private Vector3 _startPosition;
        private float _offsetX = 0.5f;

        public GridZone(ManagerData managerData, Vector3 startPosition)
        {
            _managerData = managerData;
            _startPosition = startPosition;
        }

        public Vector3 GetCellPosition(int row, int col)
        {
            Vector3 startPos = new Vector3(_startPosition.x - _managerData.TotalColumns / 2f * _managerData.CellSize,
              _startPosition.y + _managerData.RowsToFill / 2f * _managerData.CellSize, _startPosition.z);

            float xOffset = (row % 2 == 0) ? 0 : _offsetX * _managerData.CellSize;
            float xPos = startPos.x + col * _managerData.CellSize + xOffset;
            float yPos = startPos.y - row * _managerData.CellSize;

            return new Vector3(xPos, yPos, _startPosition.z);
        }
    }
}
