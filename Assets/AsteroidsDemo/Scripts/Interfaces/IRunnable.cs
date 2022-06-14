namespace AsteroidsDemo.Scripts.Interfaces
{
    public interface IRunnable
    {
        void RunFixedUpdate();
        void RunUpdate();
        bool IsAlive { get; }
    }
}