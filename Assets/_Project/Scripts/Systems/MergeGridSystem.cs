namespace MergeTower
{
    public sealed class MergeGridSystem
    {
        private readonly ElementData[] _grid = new ElementData[16];
        private readonly MergeCompletedChannel _mergeChannel;
        private readonly TowerProgressionSystem _tower;
        private readonly EconomySystem _economy;

        public MergeGridSystem(
            MergeCompletedChannel mergeChannel,
            TowerProgressionSystem tower,
            EconomySystem economy)
        {
            _mergeChannel = mergeChannel;
            _tower = tower;
            _economy = economy;
        }

        public void PlaceElement(int cellIndex, ElementData data) =>
            _grid[cellIndex] = data;

        public bool ValidateMerge(int cellA, int cellB)
        {
            if (cellA == cellB) return false;
            if (_grid[cellA].Level == 0 || _grid[cellB].Level == 0) return false;
            return _grid[cellA].Level == _grid[cellB].Level;
        }

        public MergeResult ExecuteMerge(int cellA, int cellB)
        {
            bool anyRare = _grid[cellA].IsRare || _grid[cellB].IsRare;
            long bonus = anyRare ? _grid[cellA].Level * 3L : _grid[cellA].Level * 1L;
            var result = new MergeResult(_grid[cellA].Level + 1, bonus);

            _grid[cellB] = new ElementData(result.ResultLevel, false);
            _grid[cellA] = default;

            _economy.AddCoins(result.BonusCoins);
            _tower.AddMergedElement(result.ResultLevel);
            _mergeChannel?.Raise(result);

            return result;
        }

        public int GetMergeResult(int cellLevel) => cellLevel + 1;

        public bool HasEmptyCell(out int cellIndex)
        {
            for (int i = 0; i < _grid.Length; i++)
            {
                if (_grid[i].Level == 0) { cellIndex = i; return true; }
            }
            cellIndex = -1;
            return false;
        }

        public ElementData GetCell(int index) => _grid[index];

        public void LoadFromSave(int[] gridState)
        {
            for (int i = 0; i < gridState.Length && i < _grid.Length; i++)
                _grid[i] = new ElementData(gridState[i], false);
        }
    }
}
