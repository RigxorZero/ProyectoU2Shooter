using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Drawing;
using System.Threading.Tasks;

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
        private bool isSlowMotionActive = false;
        private float slowMotionTimer = 0f;

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

            // Si se está aplicando la ralentización del tiempo
            if (isSlowMotionActive)
            {
                // Reduce el valor del temporizador de ralentización
                slowMotionTimer -= Time.deltaTime;

                // Si el temporizador alcanza 0, desactiva la ralentización
                if (slowMotionTimer <= 0f)
                {
                    isSlowMotionActive = false;
                }
            }
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
            if (other is EnemigoPerseguidor || other is EnemigoEstatico || other is EnemigoMov)
            {
                if (playerBullet)
                {
                    Player.score += 1;
                    GameEngine.Destroy(other);
                    GameEngine.Destroy(this);
                    // Ralentizar el tiempo durante 2 segundos
                    Time.SetTimeScale(0.5f);
                    Task.Delay(TimeSpan.FromSeconds(2)).ContinueWith(_ => Time.SetTimeScale(1f));
                }
            }
        }
    }
}
