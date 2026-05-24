using UnityEngine;
using UnityEngine.Pool;

namespace MergeTower
{
    public sealed class ElementPool
    {
        private readonly ObjectPool<ElementView> _pool;

        public ElementPool(ElementView prefab, Transform parent,
            int defaultCapacity = 20, int maxSize = 50)
        {
            _pool = new ObjectPool<ElementView>(
                createFunc: () => Object.Instantiate(prefab, parent),
                actionOnGet: view => view.gameObject.SetActive(true),
                actionOnRelease: view => view.gameObject.SetActive(false),
                actionOnDestroy: view => Object.Destroy(view.gameObject),
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            );
        }

        public ElementView Get() => _pool.Get();
        public void Release(ElementView view) => _pool.Release(view);
        public int CountActive => _pool.CountActive;
        public int CountInactive => _pool.CountInactive;
    }
}
