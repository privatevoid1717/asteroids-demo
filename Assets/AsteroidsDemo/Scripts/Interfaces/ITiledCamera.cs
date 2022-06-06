using UnityEngine;

namespace AsteroidsDemo.Scripts.Interfaces
{
    public interface ITiledCamera
    {
        Camera MainCamera { get; }
        int Height { get; }
        int Width { get; }
    }
}