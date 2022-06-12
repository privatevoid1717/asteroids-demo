using UnityEngine;

namespace AsteroidsDemo.Scripts.Interfaces
{
    public interface IPortableObjectPositionResolver : IService
    {
        Vector3 Resolve(Vector3 position);
    }
}