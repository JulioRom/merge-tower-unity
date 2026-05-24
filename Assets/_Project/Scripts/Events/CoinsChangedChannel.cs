using UnityEngine;
namespace MergeTower
{
    [CreateAssetMenu(menuName = "MergeTower/Events/CoinsChanged")]
    public sealed class CoinsChangedChannel : GameEventChannel<long> { }
}
