using UnityEngine;
namespace MergeTower
{
    [CreateAssetMenu(menuName = "MergeTower/Events/MergeCompleted")]
    public sealed class MergeCompletedChannel : GameEventChannel<MergeResult> { }
}
