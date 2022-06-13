using System.Collections.Generic;
using AsteroidsDemo.Scripts.Interfaces;

namespace AsteroidsDemo.Scripts.Startup.Chunks
{
    public class ChunkRunner
    {
        private readonly List<RunnablesChunk> _chunks = new();

        public void Add(IRunnable runnable)
        {
            for (int i = 0; i < _chunks.Count; i++)
            {
                var chunk = _chunks[i];
                if (!chunk.IsFull)
                {
                    chunk.AddRunnable(runnable);
                    return;
                }
            }

            var newChunk = new RunnablesChunk();
            newChunk.AddRunnable(runnable);
            _chunks.Add(newChunk);
        }

        public void RemoveNonAlive()
        {
            for (int i = 0; i < _chunks.Count; i++)
            {
                var chunk = _chunks[i];
                _chunks[i].RemoveNonAlive();

                if (chunk.IsEmpty)
                {
                    _chunks.Remove(chunk);
                }
            }
        }

        public void RunUpdate()
        {
            for (int i = 0; i < _chunks.Count; i++)
            {
                _chunks[i].RunUpdate();
            }
        }

        public void RunFixedUpdate()
        {
            for (int i = 0; i < _chunks.Count; i++)
            {
                _chunks[i].RunFixedUpdate();
            }
        }

        public int Count<T>() where T : IRunnable
        {
            int count = 0;

            for (int i = 0; i < _chunks.Count; i++)
            {
                count += _chunks[i].Count<T>();
            }

            return count;
        }
    }
}