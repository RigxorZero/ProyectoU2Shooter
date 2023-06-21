using CanvasDrawing.UtalEngine2D_2023_1;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public class Bullet : GameObject
    {
        public Vector2 Direction;
        public float Speed;
        public float timeToDie = 5f;
        public float Rotation { get; set; }
        public GameObject Shooter { get; set; }
        public Bullet(Vector2 dir, float speed, Image newSprite, Vector2 newSize, float xPos, float yPos) : base(newSprite, newSize, xPos, yPos)
        {
            this.Direction = dir;
            this.Speed = speed;
            transform.position = new Vector2(xPos, yPos); // Asignar la posición inicial
        }

        public void DrawBullet(Graphics graphics, Camera camera)
        {
            int xOffset = (int)transform.position.x;
            int yOffset = (int)transform.position.y;
            int rotation = (int)Rotation;

            spriteRenderer.Draw(graphics, camera, xOffset, yOffset, rotation);
        }

        public override void Update()
        {
            base.Update();
            transform.position += Direction * Speed * Time.deltaTime;

            if (timeToDie > 0)
            {
                timeToDie -= Time.deltaTime;
                if (timeToDie <= 0)
                {
                    Dead();
                }
            }
        }
    }
}
