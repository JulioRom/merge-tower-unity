namespace MergeTower
{
    public interface ITickable
    {
        void Tick(float deltaTime);
    }

    public interface IFixedTickable
    {
        void FixedTick(float fixedDeltaTime);
    }
}
