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
        //Dibuja la bala
        public void DrawBullet(Graphics graphics, Camera camera)
        {
            int xOffset = (int)transform.position.x;
            int yOffset = (int)transform.position.y;
            int rotation = (int)Rotation;

            spriteRenderer.Draw(graphics, camera, xOffset, yOffset, rotation);
        }
        //Actualiza la bala dentro del loop
        public override void Update()
        {
            base.Update();
            //Modifica la posición
            transform.position += Direction * Speed * Time.deltaTime;
            //Si supera el tiempo, desaparece
            if (timeToDie > 0)
            {
                timeToDie -= Time.deltaTime;
                if (timeToDie <= 0)
                {
                    GameEngine.Destroy(this);
                }
            }
        }
        //Gestor de colisiones
        public override void OnCollisionEnter(GameObject other)
        {
            //Comprobar si la bala colisiona con un muro u otra bala
            if (other is Wall || other is Bullet)
            {
                GameEngine.Destroy(this);
            }
            //Comprobar si colisiona con el jugador
            if(other is Player)
            {
                if(enemyBullet) //Si es bala de un enemigo
                {
                    GameEngine.Destroy(this);
                    Player player = (Player)other;
                    player.currentLifes -= 1;
                    if(player.currentLifes <= 0) //Verifica si le quedan vidas al jugador
                    {
                        GameEngine.playerLost = true;
                    }
                }
            }
            //Comprobar si colisiona con un NPC perseguidor
            if(other is EnemigoPerseguidor)
            {
                EnemigoPerseguidor enemy = (EnemigoPerseguidor)other;
                if(playerBullet)
                {
                    Player.score += 1;
                    enemy.DestroyGun();
                    GameEngine.Destroy(other);
                }
            }
        }
    }
}
