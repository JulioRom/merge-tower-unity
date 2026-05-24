using System;
using UnityEngine;

namespace MergeTower
{
    public abstract class GameEventChannel<T> : ScriptableObject
    {
        public event Action<T> OnEventRaised;
        public void Raise(T value) => OnEventRaised?.Invoke(value);
    }
}
