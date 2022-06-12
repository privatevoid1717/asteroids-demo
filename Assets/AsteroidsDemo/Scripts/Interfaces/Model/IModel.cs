using UnityEngine;

namespace AsteroidsDemo.Scripts.Interfaces.Model
{
    public interface IModel
    {
        public Vector3 Position { get; set; }
        Vector3 EulerAngles { get; set; }
    }
}