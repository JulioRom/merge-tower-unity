using UnityEngine;

namespace MergeTower
{
    public sealed class AutoSaveController : MonoBehaviour, ITickable
    {
        private float _autoSaveTimer;
        private const float AutoSaveInterval = 60f;

        private SaveSystem _saveSystem;
        private PlayerSaveData _saveData;
        private EconomySystem _economy;
        private TowerProgressionSystem _tower;
        private DailyGoalSystem _dailyGoal;

        public void Construct(
            SaveSystem saveSystem,
            PlayerSaveData saveData,
            EconomySystem economy,
            TowerProgressionSystem tower,
            DailyGoalSystem dailyGoal)
        {
            _saveSystem = saveSystem;
            _saveData = saveData;
            _economy = economy;
            _tower = tower;
            _dailyGoal = dailyGoal;
        }

        public void Tick(float deltaTime)
        {
            _autoSaveTimer += deltaTime;
            if (_autoSaveTimer < AutoSaveInterval) return;
            _autoSaveTimer = 0f;
            SaveGame();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) SaveGame();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus) SaveGame();
        }

        public void SaveGame()
        {
            if (_saveSystem == null) return;
            _saveData.Coins = _economy.GetBalance();
            _saveData.TowerLevel = _tower.GetTowerLevel();
            _saveData.CoinsPerSecond = _tower.GetCoinsPerSecond();
            _saveData.DailyGoalProgress = _dailyGoal.GetCurrentProgress();
            _saveSystem.Save(_saveData);
        }
    }
}
