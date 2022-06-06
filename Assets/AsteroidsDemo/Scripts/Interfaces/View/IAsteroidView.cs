namespace AsteroidsDemo.Scripts.Interfaces.View
{
    public interface IAsteroidView : IObjectView, IDestroyable
    {
        void SetScale(float scale);
    }
}