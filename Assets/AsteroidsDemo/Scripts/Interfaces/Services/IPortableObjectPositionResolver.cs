using UnityEngine;

namespace AsteroidsDemo.Scripts.Interfaces.Services
{
    public interface IPortableObjectPositionResolver : IService
    {
        Vector3 Resolve(Vector3 position);
    }
}