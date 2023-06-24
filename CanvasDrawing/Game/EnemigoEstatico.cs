using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    class EnemigoEstatico : Frame
    {
        public Image sprite;
        private Gun gun;
        Image NPC = Properties.Resources._3_right1;
        private Dictionary<string, Animation> animations;
        private KeyValuePair<string, Animation> currentAnimation;
        private float timeSinceLastShot = 0f;
        private float shootInterval = 1f; // Intervalo entre los disparos en segundos
        private List<Vector2> shootDirections;
        private Player player;

        public EnemigoEstatico(float Speed, Image newsprite, Vector2 newSize, Player target, float x = 0, float y = 0) : base(Speed, newsprite, newSize, x, y)
        {
            // Asignar jugador como objetivo
            player = target;

            // Crear el arma
            gun = new Gun(Properties.Resources.rocket_up, Properties.Resources.rocket_down, Properties.Resources.rocket_left, Properties.Resources.rocket_right, this);

            // Inicializar las direcciones de disparo
            shootDirections = new List<Vector2>
            {
                new Vector2(1f, 0f),  // Derecha
                new Vector2(0f, 1f),  // Abajo
                new Vector2(-1f, 0f)  // Izquierda
            };

            // Iniciar las animaciones
            animations = new Dictionary<string, Animation>();
            currentAnimation = new KeyValuePair<string, Animation>();

            // Establecer la animación inicial
            SetAnimation("Right");
        }

        public override void Update()
        {
            base.Update();

            // Actualizar el tiempo transcurrido desde el último disparo
            timeSinceLastShot += Time.deltaTime;

            // Verificar si ha pasado el intervalo de tiempo para disparar
            if (timeSinceLastShot >= shootInterval)
            {
                Shoot();
                timeSinceLastShot = 0f; // Reiniciar el contador
            }
        }

        private void SetAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName) && animationName != currentAnimation.Key)
            {
                currentAnimation = new KeyValuePair<string, Animation>(animationName, animations[animationName]);
                currentAnimation.Value.Reset();
            }
        }

        private void Shoot()
        {
            // Calcular el ángulo del cono de disparo
            float coneAngle = 45f; // Ángulo del cono en grados

            // Obtener la dirección hacia el jugador
            Vector2 playerDirection = player.transform.position - transform.position;
            playerDirection.Normalize();

            // Calcular la dirección central del cono
            Vector2 coneCenterDirection = playerDirection;

            // Calcular la dirección de desviación hacia la izquierda del cono
            Vector2 coneLeftDirection = Vector2.Rotate(coneCenterDirection, coneAngle / 2f);

            // Calcular la dirección de desviación hacia la derecha del cono
            Vector2 coneRightDirection = Vector2.Rotate(coneCenterDirection, -coneAngle / 2f);

            // Crear las direcciones de disparo en el cono
            List<Vector2> shootDirections = new List<Vector2>
            {
                coneCenterDirection,
                coneLeftDirection,
                coneRightDirection
            };

            foreach (Vector2 direction in shootDirections)
            {
                // Calcular un pequeño desplazamiento desde la posición actual del enemigo
                Vector2 bulletOffset = direction * 25f;

                // Crear la bala con la dirección establecida
                Bullet bullet = new Bullet(direction, 400f, Properties.Resources.bulleta, new Vector2(6, 6), transform.position.x + bulletOffset.x, transform.position.y + bulletOffset.y);
                bullet.Shooter = this;
                bullet.enemyBullet = true;
            }
        }


        // Destruye el arma
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

