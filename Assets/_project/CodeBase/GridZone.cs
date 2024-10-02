using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GridZone
    {
        private int _rows;         
        private int _columns;      
        private float _cellSize;   
        private float _offsetX = 0.5f;  

        public GridZone(int rows, int columns, float cellSize)
        {
            _rows = rows;
            _columns = columns;
            _cellSize = cellSize;
        }

        public Vector3 GetCellPosition(int row, int col)
        {
            Vector3 startPos = new Vector3(-_columns / 2f * _cellSize, _rows / 2f * _cellSize, -1);
            float xOffset = (row % 2 == 0) ? 0 : _offsetX * _cellSize;
            float xPos = startPos.x + col * _cellSize + xOffset;
            float yPos = startPos.y - row * _cellSize;

            return new Vector3(xPos, yPos, -1);
        }
    }
}
