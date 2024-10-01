using System.Collections.Generic;
using UnityEngine;

namespace Assets._project.CodeBase
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int _totalRows = 10;   // Общее количество строк
        [SerializeField] private int _totalColumns = 8; // Количество столбцов
        [SerializeField] private int _rowsToFill = 5;   // Количество строк, которые нужно заполнить
        [SerializeField] private float _cellSize = 1.0f;

        [SerializeField] private Ball _redPrefab;
        [SerializeField] private Ball _greenPrefab;
        [SerializeField] private Ball _yellowPrefab;
        [SerializeField] private Ball _bluePrefab;

        private Dictionary<string, List<Ball>> _ballDictionary;
        private GridZone _gridZone;

        private void Start()
        {
            _gridZone = new GridZone(_totalRows, _totalColumns, _cellSize);
            _ballDictionary = new Dictionary<string, List<Ball>>();

            PopulateBallDictionary();
            FillGridWithRandomBalls();
        }

        private void PopulateBallDictionary()
        {
            _ballDictionary.Add("Red", new List<Ball>());
            _ballDictionary.Add("Green", new List<Ball>());
            _ballDictionary.Add("Yellow", new List<Ball>());
            _ballDictionary.Add("Blue", new List<Ball>());

            int ballsPerColor = (_rowsToFill * _totalColumns) / 4;
            for (int i = 0; i < ballsPerColor; i++)
            {
                _ballDictionary["Red"].Add(Instantiate(_redPrefab));
                _ballDictionary["Green"].Add(Instantiate(_greenPrefab));
                _ballDictionary["Yellow"].Add(Instantiate(_yellowPrefab));
                _ballDictionary["Blue"].Add(Instantiate(_bluePrefab));
            }
        }

        private void FillGridWithRandomBalls()
        {
            for (int row = 0; row < _rowsToFill; row++)
            {
                for (int col = 0; col < _totalColumns; col++)
                {
                    string randomColor = GetRandomColor();
                    Ball ball = GetBallFromDictionary(randomColor);

                    if (ball != null)
                    {
                        Vector3 position = _gridZone.GetCellPosition(row, col);
                        ball.transform.position = position;
                        ball.gameObject.SetActive(true);
                    }
                }
            }
        }

        private string GetRandomColor()
        {
            List<string> colors = new List<string>(_ballDictionary.Keys);
            int randomIndex = Random.Range(0, colors.Count);
            return colors[randomIndex];
        }

        private Ball GetBallFromDictionary(string color)
        {
            if (_ballDictionary[color].Count > 0)
            {
                Ball ball = _ballDictionary[color][0];
                _ballDictionary[color].RemoveAt(0);
                return ball;
            }

            return null;
        }
    }
}
