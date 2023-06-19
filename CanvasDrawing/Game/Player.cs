using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CanvasDrawing.Game
{
    public class Player : Frame
    {
        private Dictionary<string, Animation> animations;
        private Animation currentAnimation;
        public Image sprite;

        public Player(float speed, Image newSprite, Vector2 newSize, float x = 0, float y = 0) : base(speed, newSprite, newSize, x, y)
        {
            // Inicializar el diccionario de animaciones
            animations = new Dictionary<string, Animation>();

            // Cargar las animaciones
            List<Image> upFrames = new List<Image>();
            upFrames.Add(Properties.Resources._1_north1);
            upFrames.Add(Properties.Resources._1_north2);
            upFrames.Add(Properties.Resources._1_north3);
            upFrames.Add(Properties.Resources._1_north4);
            Animation upAnimation = new Animation(upFrames, 0.25f);
            animations.Add("Up", upAnimation);

            List<Image> downFrames = new List<Image>();
            downFrames.Add(Properties.Resources._1_south1);
            downFrames.Add(Properties.Resources._1_south2);
            downFrames.Add(Properties.Resources._1_south3);
            downFrames.Add(Properties.Resources._1_south4);
            Animation downAnimation = new Animation(downFrames, 0.25f);
            animations.Add("Down", downAnimation);

            List<Image> leftFrames = new List<Image>();
            leftFrames.Add(Properties.Resources._1_left1);
            leftFrames.Add(Properties.Resources._1_left2);
            leftFrames.Add(Properties.Resources._1_left3);
            leftFrames.Add(Properties.Resources._1_left4);
            Animation leftAnimation = new Animation(leftFrames, 0.25f);
            animations.Add("Left", leftAnimation);

            List<Image> rightFrames = new List<Image>();
            rightFrames.Add(Properties.Resources._1_side1);
            rightFrames.Add(Properties.Resources._1_side2);
            rightFrames.Add(Properties.Resources._1_side3);
            rightFrames.Add(Properties.Resources._1_side4);
            Animation rightAnimation = new Animation(rightFrames, 0.25f);
            animations.Add("Right", rightAnimation);

            // Establecer la animación inicial
            SetAnimation("Down");
        }


        public override void OnCollisionEnter(GameObject other)
        {
            if (other is Frame)
            {
                Console.WriteLine("NPC");
                GameEngine.Destroy(this);
                GameEngine.playerLost = true;
            }
        }

        public void SetAnimation(string animationName)
        {
            if (animations.ContainsKey(animationName))
            {
                currentAnimation = animations[animationName];
                currentAnimation.Reset();
            }
        }

        public override void Update()
        {
            Vector2 auxLastPos = transform.position;
            bool moved = false;
            float moveSpeed = Speed * 100; // Velocidad de movimiento del jugador

            // Comprueba qué teclas están siendo presionadas y mueve al jugador en consecuencia
            if (InputManager.GetKey(Keys.W))
            {
                SetAnimation("Up");
                if (transform.position.y - (moveSpeed * Time.deltaTime) > 0)
                {
                    transform.position.y -= moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKey(Keys.S))
            {
                SetAnimation("Down");
                if (transform.position.y + (moveSpeed * Time.deltaTime) < 1080 * 2)
                {
                    transform.position.y += moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKey(Keys.A))
            {
                SetAnimation("Left");
                if (transform.position.x - (moveSpeed * Time.deltaTime) > 0)
                {
                    transform.position.x -= moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKey(Keys.D))
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
                currentAnimation.Update();
            }
        }

    }
}

