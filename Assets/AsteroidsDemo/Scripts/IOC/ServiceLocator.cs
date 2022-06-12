using System.Collections.Generic;
using System.Linq;
using AsteroidsDemo.Scripts.Interfaces;

namespace AsteroidsDemo.Scripts.IOC
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly List<IService> _services = new();

        public T GetService<T>() where T : IService
        {
            var service = _services.SingleOrDefault(x => x is T);
            return (T) service;
        }

        public IServiceLocator WithService(IService service)
        {
            _services.Add(service);
            return this;
        }
    }
}