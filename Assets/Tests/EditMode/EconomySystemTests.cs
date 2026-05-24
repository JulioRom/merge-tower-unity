using NUnit.Framework;
using UnityEngine;
using MergeTower;

namespace MergeTower.Tests
{
    public class EconomySystemTests
    {
        private EconomySystem _economy;
        private GameConfig _config;

        [SetUp]
        public void SetUp()
        {
            _config = ScriptableObject.CreateInstance<GameConfig>();
            _config.UpgradeCostBase = 100;
            _config.CostMultiplier = 1.8f;
            _economy = new EconomySystem(null, _config);
        }

        [Test]
        public void GetUpgradeCost_Level1_ReturnsBase()
            => Assert.AreEqual(100L, _economy.GetUpgradeCost(1));

        [Test]
        public void GetUpgradeCost_Level2_ReturnsBaseTimesMultiplier()
            => Assert.AreEqual(180L, _economy.GetUpgradeCost(2));

        [Test]
        public void GetUpgradeCost_Level5_FollowsExponentialCurve()
        {
            long expected = (long)(100 * System.Math.Pow(1.8, 4));
            Assert.AreEqual(expected, _economy.GetUpgradeCost(5));
        }

        [Test]
        public void AddCoins_IncreasesBalance()
        {
            _economy.AddCoins(500L);
            Assert.AreEqual(500L, _economy.GetBalance());
        }

        [Test]
        public void SpendCoins_SufficientBalance_ReturnsTrue()
        {
            _economy.AddCoins(200L);
            Assert.IsTrue(_economy.SpendCoins(100L));
            Assert.AreEqual(100L, _economy.GetBalance());
        }

        [Test]
        public void SpendCoins_InsufficientBalance_ReturnsFalse()
        {
            _economy.AddCoins(50L);
            Assert.IsFalse(_economy.SpendCoins(100L));
            Assert.AreEqual(50L, _economy.GetBalance());
        }

        [Test]
        public void AddCoins_NearLongMax_DoesNotOverflow()
        {
            _economy.AddCoins(long.MaxValue - 1);
            Assert.AreEqual(long.MaxValue - 1, _economy.GetBalance());
        }

        [Test]
        public void Tick_AccumulatesCoinsPerSecond_AfterOneSecond()
        {
            _economy.SetCoinsPerSecond(10L);
            _economy.Tick(1.0f);
            Assert.AreEqual(10L, _economy.GetBalance());
        }

        [Test]
        public void Tick_SubSecondDeltas_DoNotAccumulateUntilOneSecond()
        {
            _economy.SetCoinsPerSecond(10L);
            _economy.Tick(0.5f);
            _economy.Tick(0.4f);
            Assert.AreEqual(0L, _economy.GetBalance());
            _economy.Tick(0.1f);
            Assert.AreEqual(10L, _economy.GetBalance());
        }
    }
}
