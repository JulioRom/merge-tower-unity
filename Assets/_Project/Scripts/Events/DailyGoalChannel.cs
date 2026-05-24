using UnityEngine;
namespace MergeTower
{
    [CreateAssetMenu(menuName = "MergeTower/Events/DailyGoal")]
    public sealed class DailyGoalChannel : GameEventChannel<GoalProgress> { }
}
