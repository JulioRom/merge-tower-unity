using UnityEngine;

namespace MergeTower
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "MergeTower/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        [Header("Economy")]
        public long UpgradeCostBase = 100;
        public float CostMultiplier = 1.8f;
        public float CoinsPerSecondMultiplier = 1f;

        [Header("Spawn")]
        public float SpawnIntervalSeconds = 3f;
        [Range(0f, 1f)]
        public float RareSpawnChance = 0.05f;

        [Header("Daily Goal")]
        public int DailyGoalTarget = 20;

        [Header("Grid")]
        public int GridWidth = 4;
        public int GridHeight = 4;
        public int MaxElementLevel = 10;
    }
}
