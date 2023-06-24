using CanvasDrawing.UtalEngine2D_2023_1;
using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using System.Drawing;

namespace CanvasDrawing.Game
{
    class Wall : GameObject
    {
        public Wall(Image newSprite, Vector2 newSize, float xPos, float yPos) : base(newSprite, newSize, xPos, yPos)
        {
        }

        public Wall(Image newSprite, Vector2 newSize, RectCollider rectCollider, float xPos, float yPos) : base(newSprite, newSize, xPos, yPos)
        {
            this.rectCollider = rectCollider;
        }

        public override void OnCollisionEnter(GameObject other)
        {
            base.OnCollisionEnter(other);
            Frame f = other as Player;
            f?.GoBack();
        }

        public void CreateRectCollider(float width, float height)
        {
            rectCollider = new RectCollider(width, height);
        }
    }
}

