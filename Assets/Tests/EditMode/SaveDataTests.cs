using NUnit.Framework;
using UnityEngine;
using MergeTower;

namespace MergeTower.Tests
{
    public class SaveDataTests
    {
        [Test]
        public void PlayerSaveData_DefaultValues_AreValid()
        {
            var data = new PlayerSaveData();
            Assert.AreEqual(0L, data.Coins);
            Assert.AreEqual(16, data.GridState.Length);
            Assert.IsFalse(data.AdsRemoved);
        }

        [Test]
        public void PlayerSaveData_JsonRoundTrip_PreservesCoins()
        {
            var data = new PlayerSaveData { Coins = 123456789L };
            string json = JsonUtility.ToJson(data);
            var loaded = JsonUtility.FromJson<PlayerSaveData>(json);
            Assert.AreEqual(123456789L, loaded.Coins);
        }

        [Test]
        public void PlayerSaveData_JsonRoundTrip_PreservesGridState()
        {
            var data = new PlayerSaveData();
            data.GridState[3] = 5;
            string json = JsonUtility.ToJson(data);
            var loaded = JsonUtility.FromJson<PlayerSaveData>(json);
            Assert.AreEqual(5, loaded.GridState[3]);
        }
    }
}
