using System.Collections.Generic;
using AsteroidsDemo.Scripts.Interfaces;

namespace AsteroidsDemo.Scripts.Startup.Chunks
{
    public class RunnablesChunk
    {
        private const int Capacity = 3;

        private readonly List<IRunnable> _runnables = new();

        public bool IsFull => _runnables.Count >= Capacity;

        public bool IsEmpty => _runnables.Count == 0;

        public void AddRunnable(IRunnable runnable)
        {
            _runnables.Add(runnable);
        }

        public void RemoveNonAlive()
        {
            _runnables.RemoveAll(x => !x.IsAlive);
        }

        public void RunUpdate()
        {
            for (int i = 0; i < _runnables.Count; i++)
            {
                _runnables[i].RunInUpdate();
            }
        }

        public void RunFixedUpdate()
        {
            for (int i = 0; i < _runnables.Count; i++)
            {
                _runnables[i].RunFixedUpdate();
            }
        }

        public int Count<T>() where T : IRunnable
        {
            var count = 0;
            for (int i = 0; i < _runnables.Count; i++)
            {
                var runnable = _runnables[i];
                if (runnable is T && runnable.IsAlive)
                {
                    count++;
                }
            }

            return count;
        }
    }
}