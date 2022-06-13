using System;

namespace AsteroidsDemo.Scripts.Interfaces.Services
{
    public interface IInputTracker : IService

    {
        event EventHandler Fire;
        bool RotatingLeft { get; }
        bool RotatingRight { get; }
        bool IsMoving { get; }
        bool IsLaserActive { get; }
    }
}