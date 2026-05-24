using System;
using System.Collections.Generic;

namespace MergeTower
{
    [Serializable]
    public sealed class PlayerSaveData
    {
        public long Coins = 0L;
        public int[] GridState = new int[16];
        public int TowerLevel = 0;
        public long CoinsPerSecond = 0L;
        public int DailyGoalProgress = 0;
        public string LastGoalResetDateUtc = "";
        public bool AdsRemoved = false;
        public List<string> PurchasedProductIds = new List<string>();
    }
}
