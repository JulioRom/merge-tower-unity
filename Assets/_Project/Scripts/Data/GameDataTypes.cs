namespace MergeTower
{
    public readonly struct ElementData
    {
        public readonly int Level;
        public readonly bool IsRare;
        public ElementData(int level, bool isRare) { Level = level; IsRare = isRare; }
    }

    public readonly struct MergeResult
    {
        public readonly int ResultLevel;
        public readonly long BonusCoins;
        public MergeResult(int resultLevel, long bonusCoins)
        { ResultLevel = resultLevel; BonusCoins = bonusCoins; }
    }

    public readonly struct GoalProgress
    {
        public readonly int Current;
        public readonly int Target;
        public GoalProgress(int current, int target) { Current = current; Target = target; }
    }

    public readonly struct AdRevenuePayload
    {
        public readonly double eCPM;
        public readonly string NetworkName;
        public readonly string AdUnitId;
        public AdRevenuePayload(double ecpm, string networkName, string adUnitId)
        { eCPM = ecpm; NetworkName = networkName; AdUnitId = adUnitId; }
    }

    public enum RewardType { ExtraElement, DoubleIncome2h }
}
