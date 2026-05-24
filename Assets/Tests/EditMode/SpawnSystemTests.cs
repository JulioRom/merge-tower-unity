using NUnit.Framework;
using UnityEngine;
using MergeTower;

namespace MergeTower.Tests
{
    public class SpawnSystemTests
    {
        private SpawnSystem _spawn;
        private MergeGridSystem _grid;
        private GameConfig _config;

        [SetUp]
        public void SetUp()
        {
            _config = ScriptableObject.CreateInstance<GameConfig>();
            _config.SpawnIntervalSeconds = 3f;
            _config.RareSpawnChance = 0f;
            _config.UpgradeCostBase = 100;
            _config.CostMultiplier = 1.8f;
            _config.CoinsPerSecondMultiplier = 1f;
            var economy = new EconomySystem(null, _config);
            var tower = new TowerProgressionSystem(economy, _config);
            _grid = new MergeGridSystem(null, tower, economy);
            _spawn = new SpawnSystem(_grid, null, _config);
        }

        [Test]
        public void Tick_BeforeInterval_DoesNotSpawn()
        {
            _spawn.Tick(2.9f);
            Assert.AreEqual(default(ElementData), _grid.GetCell(0));
        }

        [Test]
        public void Tick_AfterInterval_SpawnsElement()
        {
            _spawn.Tick(3.0f);
            Assert.AreEqual(1, _grid.GetCell(0).Level);
        }

        [Test]
        public void Tick_FullGrid_DoesNotThrow()
        {
            for (int i = 0; i < 16; i++)
                _grid.PlaceElement(i, new ElementData(1, false));
            Assert.DoesNotThrow(() => _spawn.Tick(3.0f));
        }

        [Test]
        public void RareSpawnChance_Zero_NeverSpawnsRare()
        {
            int rareCount = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 16; j++) _grid.PlaceElement(j, default);
                _spawn.Tick(3.0f);
                if (_grid.GetCell(0).IsRare) rareCount++;
            }
            Assert.AreEqual(0, rareCount);
        }
    }
}
