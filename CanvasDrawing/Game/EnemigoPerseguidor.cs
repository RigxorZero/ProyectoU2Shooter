using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDrawing.Game
{
    public class EnemigoPerseguidor : Frame
    {
        private Dictionary<string, Animation> animations;
        private KeyValuePair<string, Animation> currentAnimation;
        public Image sprite;
        private Gun gun;
        private float timeSinceLastShot = 0f;
        private float timeSinceMovementChange = 0f;
        private bool isMovingTowardsPlayer = true;
        Player player;
        public EnemigoPerseguidor(float Speed, Image newsprite, Vector2 newSize, Player target, float x = 0, float y = 0)
            : base(Speed, newsprite, newSize, x, y)
        {

            player = target;

            // Inicializar el diccionario de animaciones
            animations = new Dictionary<string, Animation>();
            currentAnimation = new KeyValuePair<string, Animation>();
            // Cargar las animaciones
            List<Image> upFrames = new List<Image>();
            upFrames.Add(Properties.Resources._3_north1);
            upFrames.Add(Properties.Resources._3_north2);
            upFrames.Add(Properties.Resources._3_north3);
            upFrames.Add(Properties.Resources._3_north4);
            Animation upAnimation = new Animation(upFrames, 0.25f);
            animations.Add("Up", upAnimation);

            List<Image> downFrames = new List<Image>();
            downFrames.Add(Properties.Resources._3_south1);
            downFrames.Add(Properties.Resources._3_south2);
            downFrames.Add(Properties.Resources._3_south3);
            downFrames.Add(Properties.Resources._3_south4);
            Animation downAnimation = new Animation(downFrames, 0.25f);
            animations.Add("Down", downAnimation);

            List<Image> leftFrames = new List<Image>();
            leftFrames.Add(Properties.Resources._3_left1);
            leftFrames.Add(Properties.Resources._3_left2);
            leftFrames.Add(Properties.Resources._3_left3);
            leftFrames.Add(Properties.Resources._3_left4);
            Animation leftAnimation = new Animation(leftFrames, 0.25f);
            animations.Add("Left", leftAnimation);

            List<Image> rightFrames = new List<Image>();
            rightFrames.Add(Properties.Resources._3_right1);
            rightFrames.Add(Properties.Resources._3_right2);
            rightFrames.Add(Properties.Resources._3_right3);
            rightFrames.Add(Properties.Resources._3_right4);
            Animation rightAnimation = new Animation(rightFrames, 0.25f);
            animations.Add("Right", rightAnimation);


            gun = new Gun(Properties.Resources.rocket_up, Properties.Resources.rocket_down, Properties.Resources.rocket_left, Properties.Resources.rocket_right, this);

            // Establecer la animación inicial
            SetAnimation("Down");
        }

        public void SetAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName) && animationName != currentAnimation.Key)
            {
                currentAnimation = new KeyValuePair<string, Animation>(animationName, animations[animationName]);
                currentAnimation.Value.Reset();
            }
        }

        public override void Update()
        {
            Vector2 auxLastPos = transform.position;
            bool moved = false;
            float moveSpeed = Speed * 50; // Velocidad de movimiento del NPC

            // Actualizar el tiempo transcurrido desde el último disparo y movimiento
            timeSinceLastShot += Time.deltaTime;
            timeSinceMovementChange += Time.deltaTime;

            // Calcular la distancia en los ejes x e y entre el NPC y el jugador
            float distanceX = Math.Abs(transform.position.x - player.transform.position.x);
            float distanceY = Math.Abs(transform.position.y - player.transform.position.y);

            // Verificar si la distancia en ambos ejes es menor o igual a 1.5 veces el tamaño de la cámara
            if (distanceX <= 0.5f * player.myCamera.xSize && distanceY <= 0.5f * player.myCamera.ySize)
            {
                // Verificar si ha pasado el tiempo para cambiar el movimiento
                if (timeSinceMovementChange >= 2f && isMovingTowardsPlayer)
                {
                    isMovingTowardsPlayer = false;
                    timeSinceMovementChange = 0f;
                    // Disparar una bala hacia el jugador
                    Vector2 directionToPlayer = player.transform.position - transform.position;
                    directionToPlayer.Normalize();

                    // Calcular un pequeño desplazamiento desde la posición actual del NPC
                    Vector2 bulletOffset = directionToPlayer * 10f; // Ajusta el valor del desplazamiento según tus necesidades


                    // Crear la bala en la posición actual del NPC
                    Bullet bullet = new Bullet(directionToPlayer, 400f, Properties.Resources.bulleta, new Vector2(6, 6), transform.position.x + bulletOffset.x, transform.position.y + bulletOffset.y);
                    bullet.Shooter = this;
                    bullet.enemyBullet = true;
                }
                else if (timeSinceMovementChange >= 1.5f && !isMovingTowardsPlayer)
                {
                    isMovingTowardsPlayer = true;
                    timeSinceMovementChange = 0f;
                }
                // Calcular la dirección hacia el jugador o desde el jugador
                Vector2 direction = isMovingTowardsPlayer ? player.transform.position - transform.position : transform.position - player.transform.position;
                direction.Normalize();

                // Mover al enemigo hacia o desde el jugador
                transform.position += direction * moveSpeed * Time.deltaTime;
                moved = true;
            }

            if (moved)
            {
                lastPos = auxLastPos;
                currentAnimation.Value.Update();
            }

            // Establecer la animación según la dirección de movimiento
            if (Math.Abs(transform.position.x - auxLastPos.x) > Math.Abs(transform.position.y - auxLastPos.y))
            {
                if (transform.position.x > auxLastPos.x)
                    SetAnimation("Right");
                else
                    SetAnimation("Left");
            }
            else
            {
                if (transform.position.y > auxLastPos.y)
                    SetAnimation("Down");
                else
                    SetAnimation("Up");
            }

            spriteRenderer.Sprite = currentAnimation.Value.CurrentFrame;
            if (spriteRenderer.Sprite != null)
            {
                gun.Update(currentAnimation.Key);
            }
            else
            {
                // Si el sprite del enemigo está vacío, elimina el objeto Gun
                gun = null;
            }


        }

        public void DestroyGun()
        {
            gun = null;
        }


        public override void Draw(Graphics graphics, Camera camera)
        {

            if (gun != null)
            {
                // Dibujar el objeto Gun
                gun.Draw(graphics, camera);
            }

            // Dibujar el sprite del jugador
            base.Draw(graphics, camera);
        }
    }
}
