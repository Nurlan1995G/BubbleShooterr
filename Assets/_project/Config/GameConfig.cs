using System;
using UnityEngine;

namespace Assets._project.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config")]
    public class GameConfig : ScriptableObject
    {
        public ManagerData ManagerData;
    }

    [Serializable]
    public class ManagerData
    {
        public int TotalRows = 10;
        public int TotalColumns = 8;
        public int RowsToFill = 5;
        public float CellSize = 1.0f;
    }
}
