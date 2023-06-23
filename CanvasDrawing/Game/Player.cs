﻿using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CanvasDrawing.Game
{
    public class Player : Frame
    {
        private readonly Dictionary<string, Animation> animations;
        private KeyValuePair <string, Animation> currentAnimation;
        public Image sprite;
        private readonly Gun gun;
        public int lifes = 3;
        public int currentLifes;
        private float timeSinceLastShot = 0f;
        private readonly float shotCooldown = 0.3f; // Tiempo de espera entre cada disparo
        private static Player instance; // Instancia única de Player


        public Player(float speed, Image newSprite, Vector2 newSize, float x = 0, float y = 0) : base(speed, newSprite, newSize, x, y)
        {
            // Inicializar el diccionario de animaciones
            animations = new Dictionary<string, Animation>();
            currentAnimation = new KeyValuePair<string, Animation>();
            // Cargar las animaciones
            List<Image> upFrames = new List<Image>
            {
                Properties.Resources._1_north1,
                Properties.Resources._1_north2,
                Properties.Resources._1_north3,
                Properties.Resources._1_north4
            };
            Animation upAnimation = new Animation(upFrames, 0.25f);
            animations.Add("Up", upAnimation);

            List<Image> downFrames = new List<Image>
            {
                Properties.Resources._1_south1,
                Properties.Resources._1_south2,
                Properties.Resources._1_south3,
                Properties.Resources._1_south4
            };
            Animation downAnimation = new Animation(downFrames, 0.25f);
            animations.Add("Down", downAnimation);

            List<Image> leftFrames = new List<Image>
            {
                Properties.Resources._1_left1,
                Properties.Resources._1_left2,
                Properties.Resources._1_left3,
                Properties.Resources._1_left4
            };
            Animation leftAnimation = new Animation(leftFrames, 0.25f);
            animations.Add("Left", leftAnimation);

            List<Image> rightFrames = new List<Image>
            {
                Properties.Resources._1_side1,
                Properties.Resources._1_side2,
                Properties.Resources._1_side3,
                Properties.Resources._1_side4
            };
            Animation rightAnimation = new Animation(rightFrames, 0.25f);
            animations.Add("Right", rightAnimation);

            gun = new Gun(Properties.Resources.cannon_up, Properties.Resources.cannon_down, Properties.Resources.cannon_left, Properties.Resources.cannon_right, this);
            // Después de crear el objeto Player
            currentLifes = lifes;
            GameEngine.healthBar = new HealthBar(new Vector2(50, 10), this, Properties.Resources.redHeart, Properties.Resources.grayHeart, Properties.Resources.HP);

            // Establecer la animación inicial
            SetAnimation("Down");
        }


        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public override void OnCollisionEnter(GameObject other)
        {
        }

        public static Player GetInstance(float speed, Image newSprite, Vector2 newSize, float x = 0, float y = 0)
        {
            if (instance == null)
            {
                instance = new Player(speed, newSprite, newSize, x, y);
            }
            return instance;
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
            float moveSpeed = Speed * 100; // Velocidad de movimiento del jugador

            // Actualizar el tiempo transcurrido desde el último disparo
            timeSinceLastShot += Time.deltaTime;

            // Comprueba qué teclas están siendo presionadas y mueve al jugador en consecuencia
            if (InputManager.GetKeyUp(Keys.Up))
            {
                if (timeSinceLastShot >= shotCooldown)
                {
                    SetAnimation("Up");
                    Bullet bup = new Bullet(new Vector2(0, -1), 400f, Properties.Resources.bulletb, new Vector2(10, 8), transform.position.x, transform.position.y - 22)
                    {
                        Rotation = -90f,
                        Shooter = this,
                        playerBullet = true
                    };
                    timeSinceLastShot = 0f; // Reinicia el tiempo transcurrido
                }
            }
            if (InputManager.GetKeyUp(Keys.Down))
            {
                if (timeSinceLastShot >= shotCooldown)
                {
                    SetAnimation("Down");
                    Bullet bup = new Bullet(new Vector2(0, 1), 400f, Properties.Resources.bulletb, new Vector2(10, 8), transform.position.x, transform.position.y + 22)
                    {
                        Rotation = 90f,
                        Shooter = this,
                        playerBullet = true
                       
                    };
                    timeSinceLastShot = 0f; // Reinicia el tiempo transcurrido
                }
            }
            if (InputManager.GetKeyUp(Keys.Left))
            {
                if (timeSinceLastShot >= shotCooldown)
                {
                    SetAnimation("Left");
                    Bullet bup = new Bullet(new Vector2(-1, 0), 400f, Properties.Resources.bulletb, new Vector2(10, 8), transform.position.x - 22, transform.position.y)
                    {
                        Rotation = -180f,
                        Shooter = this,
                        playerBullet = true
                    };
                    timeSinceLastShot = 0f; // Reinicia el tiempo transcurrido
                }
            }
            if (InputManager.GetKeyUp(Keys.Right))
            {
                if (timeSinceLastShot >= shotCooldown)
                {
                    SetAnimation("Right");
                    Bullet bup = new Bullet(new Vector2(1, 0), 400f, Properties.Resources.bulletb, new Vector2(10, 8), transform.position.x + 22, transform.position.y)
                    {
                        Rotation = 0f,
                        Shooter = this,
                        playerBullet = true
                    };
                    timeSinceLastShot = 0f; // Reinicia el tiempo transcurrido
                }  
            }
            if (InputManager.GetKey(Keys.W))
            {
                SetAnimation("Up");
                if (transform.position.y - (moveSpeed * Time.deltaTime) > 0)
                {
                    transform.position.y -= moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            else if (InputManager.GetKey(Keys.S))
            {
                SetAnimation("Down");
                if (transform.position.y + (moveSpeed * Time.deltaTime) < 1080 * 2)
                {
                    transform.position.y += moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            else if (InputManager.GetKey(Keys.A))
            {
                SetAnimation("Left");
                if (transform.position.x - (moveSpeed * Time.deltaTime) > 0)
                {
                    transform.position.x -= moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            else if (InputManager.GetKey(Keys.D))
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
                currentAnimation.Value.Update();
            }
            spriteRenderer.Sprite = currentAnimation.Value.CurrentFrame;
            GameEngine.healthBar.UpdateCurrentHealth(currentLifes);
            gun.Update(currentAnimation.Key);
        }

        public override void Draw(Graphics graphics, Camera camera)
        {
            // Dibujar el objeto Gun debajo del jugador
            base.Draw(graphics, camera);
            gun.Draw(graphics, camera);
        }
    }
}

