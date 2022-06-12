namespace AsteroidsDemo.Scripts.Interfaces
{
    public interface IServiceLocator
    {
        T GetService<T>() where T : IService;

        static IServiceLocator Instance { get; }

        IServiceLocator WithService(IService service);
    }
}