using CanvasDrawing.UtalEngine2D_2023_1;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public class Bullet : GameObject
    {
        public Vector2 Direction;
        public float Speed;
        public float timeToDie = 5f;
        public Bullet(Vector2 dir, float speed, Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0) : base(newSprite, newSize, xPos, yPos)
        {
            this.Direction = dir;
            this.Speed = speed;
        }

        public override void Update()
        {
            base.Update();
            transform.position += Direction * Speed * Time.deltaTime;
            
            if (timeToDie > 0)
            {
                timeToDie -= Time.deltaTime;
                if (timeToDie <= 0) {
                    GameEngine.Destroy(this);
                }
            }
        }
    }
}
