using NUnit.Framework;
using UnityEngine;
using MergeTower;

namespace MergeTower.Tests
{
    public class DailyGoalSystemTests
    {
        private DailyGoalSystem _goal;
        private GameConfig _config;

        [SetUp]
        public void SetUp()
        {
            _config = ScriptableObject.CreateInstance<GameConfig>();
            _config.DailyGoalTarget = 5;
            _goal = new DailyGoalSystem(null, _config);
        }

        [Test]
        public void RecordMerge_IncrementsProgress()
        {
            var save = new PlayerSaveData
                { LastGoalResetDateUtc = System.DateTime.UtcNow.ToString("yyyy-MM-dd") };
            _goal.Initialize(save);
            _goal.RecordMerge();
            Assert.AreEqual(1, _goal.GetCurrentProgress());
        }

        [Test]
        public void Initialize_SameDay_PreservesProgress()
        {
            string today = System.DateTime.UtcNow.ToString("yyyy-MM-dd");
            var save = new PlayerSaveData { DailyGoalProgress = 3, LastGoalResetDateUtc = today };
            _goal.Initialize(save);
            Assert.AreEqual(3, _goal.GetCurrentProgress());
        }

        [Test]
        public void Initialize_NewDay_ResetsProgress()
        {
            var save = new PlayerSaveData
                { DailyGoalProgress = 3, LastGoalResetDateUtc = "2000-01-01" };
            _goal.Initialize(save);
            Assert.AreEqual(0, _goal.GetCurrentProgress());
        }

        [Test]
        public void RecordMerge_ReachesTarget_IsCompleted()
        {
            var save = new PlayerSaveData
                { LastGoalResetDateUtc = System.DateTime.UtcNow.ToString("yyyy-MM-dd") };
            _goal.Initialize(save);
            for (int i = 0; i < 5; i++) _goal.RecordMerge();
            Assert.IsTrue(_goal.IsGoalCompleted());
        }
    }
}
