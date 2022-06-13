using System.Collections.Generic;
using System.Linq;
using AsteroidsDemo.Scripts.Interfaces.Services;

namespace AsteroidsDemo.Scripts.ServiceResolving
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly List<IService> _services = new();

        public T GetService<T>() where T : IService
        {
            var service = _services.Single(x => x is T);
            return (T) service;
        }

        public IServiceLocator WithService(IService service)
        {
            _services.Add(service);
            return this;
        }
    }
}