using NUnit.Framework;
using MergeTower;

namespace MergeTower.Tests
{
    public class TickableTests
    {
        private class FakeTickable : ITickable
        {
            public float LastDelta;
            public int TickCount;
            public void Tick(float deltaTime) { LastDelta = deltaTime; TickCount++; }
        }

        [Test]
        public void ITickable_Tick_ReceivesDeltaTime()
        {
            var t = new FakeTickable();
            t.Tick(0.016f);
            Assert.AreEqual(0.016f, t.LastDelta, 0.0001f);
            Assert.AreEqual(1, t.TickCount);
        }
    }

    public class CustomUpdateManagerTests
    {
        private class FakeTickable : ITickable
        {
            public float LastDelta;
            public int TickCount;
            public void Tick(float deltaTime) { LastDelta = deltaTime; TickCount++; }
        }

        [Test]
        public void Register_ThenTick_CallsTickable()
        {
            var manager = new CustomUpdateManager();
            var fake = new FakeTickable();
            manager.Register(fake);
            manager.Tick(0.016f);
            Assert.AreEqual(1, fake.TickCount);
        }

        [Test]
        public void Unregister_ThenTick_DoesNotCallTickable()
        {
            var manager = new CustomUpdateManager();
            var fake = new FakeTickable();
            manager.Register(fake);
            manager.Unregister(fake);
            manager.Tick(0.016f);
            Assert.AreEqual(0, fake.TickCount);
        }

        [Test]
        public void Tick_MultipleTickables_AllReceiveDelta()
        {
            var manager = new CustomUpdateManager();
            var a = new FakeTickable();
            var b = new FakeTickable();
            manager.Register(a);
            manager.Register(b);
            manager.Tick(0.033f);
            Assert.AreEqual(0.033f, a.LastDelta, 0.0001f);
            Assert.AreEqual(0.033f, b.LastDelta, 0.0001f);
        }
    }
}
