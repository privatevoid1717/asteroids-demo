using AsteroidsDemo.Scripts.Interfaces.Model;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.Model
{
    public class SimpleModel : IModel
    {
        public Vector3 Position { get; set; }
        public Vector3 EulerAngles { get; set; }
    }
}