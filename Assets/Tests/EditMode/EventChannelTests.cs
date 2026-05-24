using NUnit.Framework;
using UnityEngine;
using MergeTower;

namespace MergeTower.Tests
{
    public class EventChannelTests
    {
        [Test]
        public void CoinsChangedChannel_Raise_NotifiesSubscriber()
        {
            var channel = ScriptableObject.CreateInstance<CoinsChangedChannel>();
            long received = -1;
            channel.OnEventRaised += val => received = val;
            channel.Raise(500L);
            Assert.AreEqual(500L, received);
        }

        [Test]
        public void CoinsChangedChannel_Raise_NoSubscribers_DoesNotThrow()
        {
            var channel = ScriptableObject.CreateInstance<CoinsChangedChannel>();
            Assert.DoesNotThrow(() => channel.Raise(100L));
        }
    }
}
