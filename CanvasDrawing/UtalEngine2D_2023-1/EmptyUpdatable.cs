
namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public abstract class EmptyUpdatable
    {
        public EmptyUpdatable()
        {
            GameObjectManager.AllEmptyUpdatables.Add(this);
        }
        public abstract void Update();
    }
}
