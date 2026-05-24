using NUnit.Framework;
using UnityEngine;
using MergeTower;

namespace MergeTower.Tests
{
    public class TowerProgressionSystemTests
    {
        private TowerProgressionSystem _tower;
        private EconomySystem _economy;
        private GameConfig _config;

        [SetUp]
        public void SetUp()
        {
            _config = ScriptableObject.CreateInstance<GameConfig>();
            _config.CoinsPerSecondMultiplier = 1f;
            _config.UpgradeCostBase = 100;
            _config.CostMultiplier = 1.8f;
            _economy = new EconomySystem(null, _config);
            _tower = new TowerProgressionSystem(_economy, _config);
        }

        [Test]
        public void AddMergedElement_IncreasesTowerLevel()
        {
            _tower.AddMergedElement(3);
            Assert.AreEqual(3, _tower.GetTowerLevel());
        }

        [Test]
        public void AddMergedElement_UpdatesCoinsPerSecond()
        {
            _tower.AddMergedElement(5);
            Assert.AreEqual(5L, _tower.GetCoinsPerSecond());
        }

        [Test]
        public void AddMergedElement_Multiple_AccumulatesLevel()
        {
            _tower.AddMergedElement(2);
            _tower.AddMergedElement(3);
            Assert.AreEqual(5, _tower.GetTowerLevel());
            Assert.AreEqual(5L, _tower.GetCoinsPerSecond());
        }
    }
}
