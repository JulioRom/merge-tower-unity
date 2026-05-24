using UnityEngine;

namespace MergeTower
{
    public sealed class ElementView : MonoBehaviour
    {
        public int CellIndex { get; private set; }
        public ElementData Data { get; private set; }

        public void Initialize(ElementData data, int cellIndex)
        {
            Data = data;
            CellIndex = cellIndex;
            gameObject.SetActive(true);
        }

        public void ReturnToPool(ElementPool pool)
        {
            gameObject.SetActive(false);
            pool.Release(this);
        }
    }
}
