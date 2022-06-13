namespace AsteroidsDemo.Scripts.Interfaces.Services
{
    public interface IServiceLocator
    {
        T GetService<T>() where T : IService;

        IServiceLocator WithService(IService service);
    }
}