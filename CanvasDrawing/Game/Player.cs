using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;

namespace CanvasDrawing.Game
{
    public class Player : Frame
    {
        private Dictionary<string, List<Image>> animations;
        private string currentAnimation;
        private int currentSpriteIndex;
        public Image sprite;  // Agrega este campo a la clase Player


        public Player(float Speed, Image newsprite, Vector2 newSize, float x = 0, float y = 0) : base(Speed, newsprite, newSize, x, y)
        {
            // Inicializar el diccionario de animaciones
            animations = new Dictionary<string, List<Image>>();

            // Cargar las imágenes de las animaciones
            List<Image> upAnimation = new List<Image>();
            upAnimation.Add(Properties.Resources._1_north1);
            upAnimation.Add(Properties.Resources._1_north2);
            upAnimation.Add(Properties.Resources._1_north3);
            upAnimation.Add(Properties.Resources._1_north4);
            animations.Add("Up", upAnimation);

            List<Image> downAnimation = new List<Image>();
            downAnimation.Add(Properties.Resources._1_south1);
            downAnimation.Add(Properties.Resources._1_south2);
            downAnimation.Add(Properties.Resources._1_south3);
            downAnimation.Add(Properties.Resources._1_south4);
            animations.Add("Down", downAnimation);

            List<Image> leftAnimation = new List<Image>();
            leftAnimation.Add(Properties.Resources._1_left1);
            leftAnimation.Add(Properties.Resources._1_left2);
            leftAnimation.Add(Properties.Resources._1_left3);
            leftAnimation.Add(Properties.Resources._1_left4);
            animations.Add("Left", leftAnimation);

            List<Image> rightAnimation = new List<Image>();
            rightAnimation.Add(Properties.Resources._1_side1);
            rightAnimation.Add(Properties.Resources._1_side2);
            rightAnimation.Add(Properties.Resources._1_side3);
            rightAnimation.Add(Properties.Resources._1_side4);
            animations.Add("Right", rightAnimation);

            // Establecer la animación inicial
            SetAnimation("Down");
        }

        public override void OnCollisionEnter(GameObject other)
        {
            //renderer.sprite.Dispose();
            if (other is Frame)     /*  VERIFICA SI CHOCA CON UN FRAME, EN GENERAL UN NPC  */
            {
                Console.WriteLine("NPC");
                GameEngine.Destroy(this);
                GameEngine.playerLost = true; /* CAMBIA EL VALOR DE PLAYERLOST EN GameEngine (lINEA 150) */
            }
        }

        public void SetAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName))
            {
                currentAnimation = animationName;
                currentSpriteIndex = 0;
                sprite = animations[currentAnimation][currentSpriteIndex];

                // Actualizar el sprite del renderer
                renderer.sprite = sprite;
            }
        }


        public void Draw(Graphics g)
        {
            // Calcula el rectángulo del área de dibujo del sprite
            RectangleF spriteRect = new RectangleF(transform.position.x, transform.position.y, 20, 24);

            // Resto del código para dibujar el jugador
            g.DrawImage(sprite, spriteRect);
        }

        public Image GetCurrentAnimationSprite()
        {
            if (animations.ContainsKey(currentAnimation))
            {
                return animations[currentAnimation][currentSpriteIndex];
            }

            return null;
        }

        public override void Update()
        {
            Vector2 auxLastPos = transform.position;
            bool moved = false;
            float moveSpeed = Speed*100; // Velocidad de movimiento del jugador
            currentSpriteIndex = (currentSpriteIndex + 1) % animations[currentAnimation].Count;
            sprite = animations[currentAnimation][currentSpriteIndex];


            // Comprueba qué teclas están siendo presionadas y mueve al jugador en consecuencia
            if (InputManager.GetKeyDown(System.Windows.Forms.Keys.W))
            {
                SetAnimation("Up");
                if (transform.position.y - (moveSpeed * Time.deltaTime) > 0)
                {
                    transform.position.y -= moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKeyDown(System.Windows.Forms.Keys.S))
            {
                SetAnimation("Down");
                if (transform.position.y + (moveSpeed * Time.deltaTime) < 1080 * 2)
                {
                    transform.position.y += moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKeyDown(System.Windows.Forms.Keys.A))
            {
                SetAnimation("Left");
                if (transform.position.x - (moveSpeed * Time.deltaTime) > 0)
                {
                    transform.position.x -= moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKeyDown(System.Windows.Forms.Keys.D))
            {
                SetAnimation("Right");
                if (transform.position.x + (moveSpeed * Time.deltaTime) < 1920 * 2)
                {
                    transform.position.x += moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }

            if (moved)
            {
                lastPos = auxLastPos;

                sprite = animations[currentAnimation][currentSpriteIndex];
                renderer.sprite = sprite;

                // Obtener la posición del jugador en el espacio de la cámara
                Vector2 cameraPos = GameEngine.WorldToCameraPos(transform.position);

                // Verificar si el jugador se acerca a un borde
                bool nearLeftEdge = cameraPos.x <= (GameEngine.MainCamera.xSize * 0.25f);
                bool nearRightEdge = cameraPos.x >= (GameEngine.MainCamera.xSize * 0.75f);
                bool nearTopEdge = cameraPos.y <= (GameEngine.MainCamera.ySize * 0.25f);
                bool nearBottomEdge = cameraPos.y >= (GameEngine.MainCamera.ySize * 0.75f);

                // Ajustar la posición de la cámara si el jugador se acerca a un borde
                if (nearLeftEdge || nearRightEdge || nearTopEdge || nearBottomEdge)
                {
                    float cameraX = GameEngine.MainCamera.Position.x;
                    float cameraY = GameEngine.MainCamera.Position.y;

                    if (nearLeftEdge)
                    {
                        cameraX = transform.position.x - (GameEngine.MainCamera.xSize * 0.25f);
                    }
                    else if (nearRightEdge)
                    {
                        cameraX = transform.position.x - (GameEngine.MainCamera.xSize * 0.75f);
                    }

                    if (nearTopEdge)
                    {
                        cameraY = transform.position.y - (GameEngine.MainCamera.ySize * 0.25f);
                    }
                    else if (nearBottomEdge)
                    {
                        cameraY = transform.position.y - (GameEngine.MainCamera.ySize * 0.75f);
                    }

                    // Actualizar la posición de la cámara
                    GameEngine.MainCamera.Position = new Vector2(cameraX, cameraY);
                }
            }
        }
    }
}
