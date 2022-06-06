namespace AsteroidsDemo.Scripts.Interfaces
{
    public interface IRunnable
    {
        void RunFixedUpdate();
        void RunInUpdate();
        bool IsAlive { get; }
    }
}