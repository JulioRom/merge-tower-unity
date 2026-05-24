using NUnit.Framework;
using UnityEngine;
using MergeTower;

namespace MergeTower.Tests
{
    public class MergeGridSystemTests
    {
        private MergeGridSystem _grid;
        private EconomySystem _economy;
        private TowerProgressionSystem _tower;
        private GameConfig _config;

        [SetUp]
        public void SetUp()
        {
            _config = ScriptableObject.CreateInstance<GameConfig>();
            _config.UpgradeCostBase = 100;
            _config.CostMultiplier = 1.8f;
            _config.CoinsPerSecondMultiplier = 1f;
            _economy = new EconomySystem(null, _config);
            _tower = new TowerProgressionSystem(_economy, _config);
            _grid = new MergeGridSystem(null, _tower, _economy);
        }

        [Test]
        public void ValidateMerge_SameLevel_ReturnsTrue()
        {
            _grid.PlaceElement(0, new ElementData(2, false));
            _grid.PlaceElement(1, new ElementData(2, false));
            Assert.IsTrue(_grid.ValidateMerge(0, 1));
        }

        [Test]
        public void ValidateMerge_DifferentLevel_ReturnsFalse()
        {
            _grid.PlaceElement(0, new ElementData(1, false));
            _grid.PlaceElement(1, new ElementData(2, false));
            Assert.IsFalse(_grid.ValidateMerge(0, 1));
        }

        [Test]
        public void ValidateMerge_EmptyCell_ReturnsFalse()
        {
            _grid.PlaceElement(0, new ElementData(1, false));
            Assert.IsFalse(_grid.ValidateMerge(0, 1));
        }

        [Test]
        public void ValidateMerge_SameCell_ReturnsFalse()
        {
            _grid.PlaceElement(0, new ElementData(1, false));
            Assert.IsFalse(_grid.ValidateMerge(0, 0));
        }

        [Test]
        public void ExecuteMerge_ProducesHigherLevel()
        {
            _grid.PlaceElement(0, new ElementData(2, false));
            _grid.PlaceElement(1, new ElementData(2, false));
            var result = _grid.ExecuteMerge(0, 1);
            Assert.AreEqual(3, result.ResultLevel);
        }

        [Test]
        public void ExecuteMerge_ClearsSourceCell()
        {
            _grid.PlaceElement(0, new ElementData(2, false));
            _grid.PlaceElement(1, new ElementData(2, false));
            _grid.ExecuteMerge(0, 1);
            Assert.IsFalse(_grid.ValidateMerge(0, 1));
        }

        [Test]
        public void ExecuteMerge_RareElement_AppliesBonus()
        {
            _grid.PlaceElement(0, new ElementData(2, true));
            _grid.PlaceElement(1, new ElementData(2, false));
            var result = _grid.ExecuteMerge(0, 1);
            Assert.AreEqual(2L * 3L, result.BonusCoins);
        }

        [Test]
        public void GetMergeResult_ReturnsLevelPlusOne()
            => Assert.AreEqual(4, _grid.GetMergeResult(3));

        [Test]
        public void HasEmptyCell_EmptyGrid_ReturnsTrue()
        {
            Assert.IsTrue(_grid.HasEmptyCell(out int idx));
            Assert.AreEqual(0, idx);
        }

        [Test]
        public void HasEmptyCell_FullGrid_ReturnsFalse()
        {
            for (int i = 0; i < 16; i++)
                _grid.PlaceElement(i, new ElementData(1, false));
            Assert.IsFalse(_grid.HasEmptyCell(out _));
        }
    }
}
