using System;

namespace MergeTower
{
    public sealed class EconomySystem : ITickable
    {
        private long _coins;
        private long _coinsPerSecond;
        private float _accumulatedTime;
        private readonly CoinsChangedChannel _coinsChangedChannel;
        private readonly GameConfig _config;

        public EconomySystem(CoinsChangedChannel coinsChangedChannel, GameConfig config)
        {
            _coinsChangedChannel = coinsChangedChannel;
            _config = config;
        }

        public void Tick(float deltaTime)
        {
            _accumulatedTime += deltaTime;
            if (_accumulatedTime < 1f) return;
            _accumulatedTime -= 1f;
            AddCoins(_coinsPerSecond);
        }

        public void AddCoins(long amount)
        {
            _coins += amount;
            _coinsChangedChannel?.Raise(_coins);
        }

        public bool SpendCoins(long amount)
        {
            if (_coins < amount) return false;
            _coins -= amount;
            _coinsChangedChannel?.Raise(_coins);
            return true;
        }

        public long GetUpgradeCost(int currentLevel) =>
            (long)(_config.UpgradeCostBase * Math.Pow(_config.CostMultiplier, currentLevel - 1));

        public long GetBalance() => _coins;
        public void SetCoinsPerSecond(long cps) => _coinsPerSecond = cps;
        public void LoadFromSave(long coins) => _coins = coins;
    }
}
