using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MergeTower;

namespace MergeTower.Tests
{
    public class GameLoopIntegrationTests
    {
        private EconomySystem _economy;
        private MergeGridSystem _grid;
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

        [UnityTest]
        public IEnumerator MergeLoop_TenMerges_TowerLevelIncreases()
        {
            for (int i = 0; i < 10; i++)
            {
                _grid.PlaceElement(0, new ElementData(1, false));
                _grid.PlaceElement(1, new ElementData(1, false));
                _grid.ExecuteMerge(0, 1);
                yield return null;
            }
            Assert.Greater(_tower.GetTowerLevel(), 0);
        }

        [UnityTest]
        public IEnumerator EconomyTick_ManualOneSecond_AccumulatesCoins()
        {
            _tower.AddMergedElement(5);
            _economy.Tick(1.0f);
            yield return null;
            Assert.AreEqual(5L, _economy.GetBalance());
        }

        [UnityTest]
        public IEnumerator MergeLoop_NoGCAllocation_DuringHundredMerges()
        {
            // Pre-calentar para evitar falsos positivos de JIT
            for (int i = 0; i < 5; i++)
            {
                _grid.PlaceElement(0, new ElementData(1, false));
                _grid.PlaceElement(1, new ElementData(1, false));
                _grid.ExecuteMerge(0, 1);
            }
            yield return null;

            long gcBefore = System.GC.GetTotalMemory(false);

            for (int i = 0; i < 100; i++)
            {
                _grid.PlaceElement(0, new ElementData(1, false));
                _grid.PlaceElement(1, new ElementData(1, false));
                _grid.ExecuteMerge(0, 1);
                yield return null;
            }

            long gcAfter = System.GC.GetTotalMemory(false);
            long allocated = gcAfter - gcBefore;

            Assert.AreEqual(0L, allocated,
                $"GC allocated {allocated} bytes during 100 merges. " +
                "Open Profiler > Deep Profile to find the allocation source.");
        }
    }
}
