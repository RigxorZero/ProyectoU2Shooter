using CanvasDrawing.UtalEngine2D_2023_1;
using System;
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
        public bool playerBullet { get; set; }
        public bool enemyBullet { get; set; }
        public bool isDead { get; private set; } = false;

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
                    GameEngine.Destroy(this);
                }
            }
        }

        public override void OnCollisionEnter(GameObject other)
        {
            // Comprobar si la bala colisiona con un muro
            if (other is Wall || other is Bullet)
            {
                GameEngine.Destroy(this);
            }
            if(other is Player)
            {
                if(enemyBullet)
                {
                    GameEngine.Destroy(this);
                    Player player = (Player)other;
                    player.currentLifes -= 1;
                }
            }
            if(other is EnemigoPerseguidor)
            {
                EnemigoPerseguidor enemy = (EnemigoPerseguidor)other;
                if(playerBullet)
                {
                    enemy.DestroyGun();
                    GameEngine.Destroy(other);
                }
            }
        }

    }
}
