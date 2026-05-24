using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace MergeTower
{
    public sealed class CustomUpdateManager
    {
        private readonly List<ITickable> _tickables = new List<ITickable>(64);

        public void Register(ITickable tickable)
        {
            if (!_tickables.Contains(tickable))
                _tickables.Add(tickable);
        }

        public void Unregister(ITickable tickable) =>
            _tickables.Remove(tickable);

        public void Tick(float deltaTime)
        {
            for (int i = 0; i < _tickables.Count; i++)
                _tickables[i].Tick(deltaTime);
        }

        public void InjectIntoPlayerLoop()
        {
            var loop = PlayerLoop.GetCurrentPlayerLoop();
            for (int i = 0; i < loop.subSystemList.Length; i++)
            {
                if (loop.subSystemList[i].type == typeof(Update))
                {
                    var update = loop.subSystemList[i];
                    var list = new List<PlayerLoopSystem>(
                        update.subSystemList ?? new PlayerLoopSystem[0]);
                    list.Add(new PlayerLoopSystem
                    {
                        type = typeof(CustomUpdateManager),
                        updateDelegate = () => Tick(Time.deltaTime)
                    });
                    update.subSystemList = list.ToArray();
                    loop.subSystemList[i] = update;
                    break;
                }
            }
            PlayerLoop.SetPlayerLoop(loop);
        }
    }
}
