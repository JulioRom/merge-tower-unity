namespace MergeTower
{
    public sealed class SpawnSystem : ITickable
    {
        private float _timer;
        private readonly MergeGridSystem _grid;
        private readonly RareElementChannel _rareChannel;
        private readonly GameConfig _config;
        private readonly System.Random _random = new System.Random();

        public SpawnSystem(
            MergeGridSystem grid,
            RareElementChannel rareChannel,
            GameConfig config)
        {
            _grid = grid;
            _rareChannel = rareChannel;
            _config = config;
        }

        public void Tick(float deltaTime)
        {
            _timer += deltaTime;
            if (_timer < _config.SpawnIntervalSeconds) return;
            _timer -= _config.SpawnIntervalSeconds;
            TrySpawn();
        }

        public void TrySpawn()
        {
            if (!_grid.HasEmptyCell(out int cellIndex)) return;
            bool isRare = _random.NextDouble() < _config.RareSpawnChance;
            var data = new ElementData(1, isRare);
            _grid.PlaceElement(cellIndex, data);
            if (isRare) _rareChannel?.Raise(data);
        }
    }
}
