using System;

namespace MergeTower
{
    public sealed class DailyGoalSystem
    {
        private int _mergeCount;
        private bool _goalCompleted;
        private readonly DailyGoalChannel _goalChannel;
        private readonly GameConfig _config;

        public DailyGoalSystem(DailyGoalChannel goalChannel, GameConfig config)
        {
            _goalChannel = goalChannel;
            _config = config;
        }

        public void Initialize(PlayerSaveData saveData)
        {
            string todayUtc = DateTime.UtcNow.ToString("yyyy-MM-dd");
            if (saveData.LastGoalResetDateUtc != todayUtc)
            {
                saveData.DailyGoalProgress = 0;
                saveData.LastGoalResetDateUtc = todayUtc;
            }
            _mergeCount = saveData.DailyGoalProgress;
            _goalCompleted = _mergeCount >= _config.DailyGoalTarget;
        }

        public void RecordMerge()
        {
            if (_goalCompleted) return;
            _mergeCount++;
            var progress = new GoalProgress(_mergeCount, _config.DailyGoalTarget);
            _goalChannel?.Raise(progress);
            if (_mergeCount >= _config.DailyGoalTarget)
                _goalCompleted = true;
        }

        public int GetCurrentProgress() => _mergeCount;
        public bool IsGoalCompleted() => _goalCompleted;
    }
}
