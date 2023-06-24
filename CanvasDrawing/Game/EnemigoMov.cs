using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using CanvasDrawing.UtalEngine2D_2023_1.Physics;

namespace CanvasDrawing.Game
{
    public class EnemigoMov : Frame
    {
        private Dictionary<string, Animation> animations;
        private KeyValuePair<string, Animation> currentAnimation;
        public Image sprite;
        private Gun gun;
        private float timeSinceLastShot = 0f;
        private float shootInterval = 2f; // Intervalo de tiempo entre disparos
        private Vector2 currentDirection;
        Random random;
        private Player player;
        public EnemigoMov(float Speed, Image newsprite, Vector2 newSize, Player target, float x = 0, float y = 0) : base(Speed, newsprite, newSize, x, y)
        {
            //Asigna jugador como objetivo
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

            //Crea el arma
            gun = new Gun(Properties.Resources.rocket_up, Properties.Resources.rocket_down, Properties.Resources.rocket_left, Properties.Resources.rocket_right, this);

            // Establecer la animación inicial
            SetAnimation("Down");

            // Generar una dirección inicial aleatoria
            Random random = new Random();
            int randomDirection = random.Next(0, 2);

            if (randomDirection == 0)
            {
                // Mover horizontalmente
                currentDirection = new Vector2(1, 0); // Puedes ajustar los valores según tus necesidades
            }
            else
            {
                // Mover verticalmente
                currentDirection = new Vector2(0, 1); // Puedes ajustar los valores según tus necesidades
            }

        }

        //Envia la animación actual
        public void SetAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName) && animationName != currentAnimation.Key)
            {
                currentAnimation = new KeyValuePair<string, Animation>(animationName, animations[animationName]);
                currentAnimation.Value.Reset();
            }
        }

        public override void OnCollisionEnter(GameObject other)
        {
            base.OnCollisionEnter(other);

            // Verificar si la colisión es con un objeto de tipo Muro
            Wall muro = other as Wall;
            if (muro != null)
            {
                // Invertir la dirección del enemigo
                currentDirection *= -1;
            }
        }
        public override void Update()
        {
            bool moved = false;
            float moveSpeed = Speed * 50; // Velocidad de movimiento del enemigo
            Vector2 auxLastPos = transform.position;

            // Verificar si ha pasado el tiempo suficiente para disparar
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= shootInterval)
            {
                timeSinceLastShot = 0f;

                Vector2 directionToPlayer = player.transform.position - transform.position;
                directionToPlayer.Normalize();

                // Calcular un pequeño desplazamiento desde la posición actual del NPC
                Vector2 bulletOffset = directionToPlayer * 25f;

                // Crear la bala en la posición actual del enemigo
                Bullet bullet = new Bullet(directionToPlayer, 400f, Properties.Resources.bulletb, new Vector2(6, 6), transform.position.x + bulletOffset.x, transform.position.y + bulletOffset.y);
                bullet.Shooter = this;
                bullet.enemyBullet = true;
            }

            Vector2 newPosition = transform.position + (currentDirection * moveSpeed * Time.deltaTime);
            moved = true;
            // Actualizar la posición del enemigo
            transform.position = newPosition;
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

        //Destruye el arma
        public void DestroyGun()
        {
            gun = null;
        }
        //Dibujo del enemigo
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