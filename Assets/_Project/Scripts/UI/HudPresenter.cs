using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MergeTower
{
    public sealed class HudPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsLabel;
        [SerializeField] private TextMeshProUGUI _dailyGoalLabel;
        [SerializeField] private Button _rewardedAdButton;
        [SerializeField] private CoinsChangedChannel _coinsChangedChannel;
        [SerializeField] private DailyGoalChannel _dailyGoalChannel;
        // TODO: Restore after importing AppLovin MAX SDK (Task 16)
        // [SerializeField] private AdsService _adsService;

        private void OnEnable()
        {
            _coinsChangedChannel.OnEventRaised += OnCoinsChanged;
            _dailyGoalChannel.OnEventRaised += OnGoalProgress;
        }

        private void OnDisable()
        {
            _coinsChangedChannel.OnEventRaised -= OnCoinsChanged;
            _dailyGoalChannel.OnEventRaised -= OnGoalProgress;
        }

        private void Update()
        {
            // TODO: Restore after importing AppLovin MAX SDK (Task 16)
            // if (_rewardedAdButton != null && _adsService != null)
            //     _rewardedAdButton.interactable = _adsService.IsRewardedReady;
        }

        private void OnCoinsChanged(long coins) =>
            _coinsLabel.text = FormatCoins(coins);

        private void OnGoalProgress(GoalProgress progress) =>
            _dailyGoalLabel.text = $"{progress.Current}/{progress.Target}";

        public void OnRewardedAdButtonPressed()
        {
            // TODO: Restore after importing AppLovin MAX SDK (Task 16)
            // _adsService?.ShowRewarded(RewardType.ExtraElement);
        }

        private static string FormatCoins(long coins)
        {
            if (coins >= 1_000_000_000L) return $"{coins / 1_000_000_000f:F1}B";
            if (coins >= 1_000_000L) return $"{coins / 1_000_000f:F1}M";
            if (coins >= 1_000L) return $"{coins / 1_000f:F1}K";
            return coins.ToString();
        }
    }
}
