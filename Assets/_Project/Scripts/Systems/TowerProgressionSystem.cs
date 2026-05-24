namespace MergeTower
{
    public sealed class TowerProgressionSystem
    {
        private int _totalTowerLevel;
        private long _coinsPerSecond;
        private readonly EconomySystem _economy;
        private readonly GameConfig _config;

        public TowerProgressionSystem(EconomySystem economy, GameConfig config)
        {
            _economy = economy;
            _config = config;
        }

        public void AddMergedElement(int elementLevel)
        {
            _totalTowerLevel += elementLevel;
            RecalculateCoinsPerSecond();
        }

        private void RecalculateCoinsPerSecond()
        {
            _coinsPerSecond = (long)(_totalTowerLevel * _config.CoinsPerSecondMultiplier);
            _economy.SetCoinsPerSecond(_coinsPerSecond);
        }

        public long GetCoinsPerSecond() => _coinsPerSecond;
        public int GetTowerLevel() => _totalTowerLevel;

        public void LoadFromSave(int towerLevel)
        {
            _totalTowerLevel = towerLevel;
            RecalculateCoinsPerSecond();
        }
    }
}
