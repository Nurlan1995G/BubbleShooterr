using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GridZone
    {
        private int _rows;         // Количество строк
        private int _columns;      // Количество столбцов
        private float _cellSize;   // Размер ячейки
        private float _offsetX = 0.5f;  // Смещение для четных строк

        public GridZone(int rows, int columns, float cellSize)
        {
            _rows = rows;
            _columns = columns;
            _cellSize = cellSize;
        }

        // Получение позиции ячейки на сцене по индексу строки и столбца
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
