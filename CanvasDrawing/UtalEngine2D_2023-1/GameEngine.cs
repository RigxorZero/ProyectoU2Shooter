﻿
using CanvasDrawing.Game;
using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class GameEngine
    {
        private static Form engineDrawForm;
        private static Thread gameLoopThread = null;
        public static Camera MainCamera = new Camera();

        public static bool playerLost = false; // Variable para indicar si el jugador ha perdido
        public static bool playerWin = false; // Variable para indicar si el jugador gano
        private static GameInicio gameInicio; /*  CREA LA PANTALLA DE INICIO  */
        private static GameVictoryScreen gameVictoryScreen; /*  CREA LA PANTALLA DE VICTORIA  */
        private static GameOverScreen gameOverScreen; /*  CREA LA PANTALLA DE DERROTA  */

        private static float timeSinceLastNPC = 0f;
        private const float NPCGenerationInterval = 10f; // Intervalo de generación de NPC en segundos

        public static HealthBar healthBar;

        public static Image NPC { get; private set; }
        public static Image jugador { get; private set; }
        public static Player player { get; private set; }

        public static void Destroy(GameObject go)
        {
            GameObjectManager.AllDeadGameObjects.Add(go);
        }
        
        public static void Destroy(UtalText utalText)
        {
            GameObjectManager.AllDeadText.Add(utalText);
        }
        
        public static void Destroy(EmptyUpdatable empty)
        {
            GameObjectManager.AllDeadEmptyUpdatables.Add(empty);
        }
        
        public static void InitEngine(Form engineDrawForm)
        {

            GameEngine.engineDrawForm = engineDrawForm;
            gameLoopThread = new Thread(GameLoop);
            //EngineDrawForm.Paint += new System.Windows.Forms.PaintEventHandler(Paint);
            engineDrawForm.KeyPress += new KeyPressEventHandler(InputManager.KeyPressHandler);
            engineDrawForm.KeyDown += new KeyEventHandler(InputManager.KeyDownHandler);
            engineDrawForm.KeyUp += new KeyEventHandler(InputManager.KeyUpHandler);
            engineDrawForm.Height = MainCamera.ySize * 2;
            engineDrawForm.Width = MainCamera.xSize * 2;
            jugador = Properties.Resources._1_south1;
            NPC = Properties.Resources._3_south1;

            Random random = new Random();

            int playerX = random.Next(1, 11) * 50 + 25;
            int playerY = random.Next(1, 11) * 50 + 25;
            player = Player.GetInstance(2, jugador, new Vector2(40, 48), playerX, playerY);
            player.currentLifes = 3;
            gameInicio = new GameInicio(engineDrawForm); /*  ASIGNA EL FORMULARIO ACTUAL A LA PANTALLA DE INICIO  */
            gameOverScreen = new GameOverScreen(engineDrawForm); /*  ASIGNA EL FORMULARIO ACTUAL A LA PANTALLA DE DERROTA  */
            gameVictoryScreen = new GameVictoryScreen(engineDrawForm); /*  ASIGNA EL FORMULARIO ACTUAL A LA PANTALLA DE VICTORIA  */



            gameInicio.Show();
            gameLoopThread.Start();

            
        }

        public static void Start()
        {
            /*  MUESTRA PANTALLA DE INICIO  */
            gameInicio.Show();
        }

        private static void GameLoop()
        {
            while (!engineDrawForm.IsDisposed)
            {
                Thread.Sleep(1000 / 120);
                try
                {
                    engineDrawForm.Refresh();
                }
                catch
                {
                    engineDrawForm.Invalidate();
                }
                Time.UpdateDeltaTime();
                GameObjectManager.Update();
                PhysicsEngine.Update();
                timeSinceLastNPC += Time.deltaTime;


                if (playerLost) /*  VERIFICA SI EL JUGADOR PERDIO  */
                {
                    gameOverScreen.Show(); /* MUESTRA PANTALLA DERROTA  (lINEA 53/GameEngine)  */
                    break; // Salir del bucle de juego
                }

                if (playerWin) /*  VERIFICA SI EL JUGADOR GANO  */
                {
                    gameVictoryScreen.Show(); /* MUESTRA PANTALLA VICTORIA  (lINEA 54/GameEngine)  */
                    break; // Salir del bucle de juego
                }
                InputManager.Update();

                if (timeSinceLastNPC >= NPCGenerationInterval)
                {
                    GenerateNPC();
                    timeSinceLastNPC = 0f;
                }


                if (!gameInicio.IsActive && !playerLost && !playerWin)
                {
                    GameObjectManager.DeadUpdate();
                }
            }
        }


        public static Vector2 WorldToCameraPos(Vector2 pos)
        {
            float minX = MainCamera.Position.x + MainCamera.xSize * 0.5f;
            float maxX = MainCamera.Position.x + (1920 - 2 * 50) * MainCamera.scale - MainCamera.xSize * 0.5f;
            float minY = MainCamera.Position.y + MainCamera.ySize * 0.5f;
            float maxY = MainCamera.Position.y + (1080 - 2 * 50) * MainCamera.scale - MainCamera.ySize * 0.5f;

            float clampedX = Math.Max(minX, Math.Min(pos.x, maxX));
            float clampedY = Math.Max(minY, Math.Min(pos.y, maxY));

            return new Vector2((clampedX - MainCamera.Position.x) / MainCamera.scale + MainCamera.xSize * 0.5f,
                               (clampedY - MainCamera.Position.y) / MainCamera.scale + MainCamera.ySize * 0.5f);
        }

        public static void Paint(Object sender, PaintEventArgs e)
        {
            int newXSize = engineDrawForm.Width;
            int newYSize = engineDrawForm.Height;
            bool changed = false;
            if (e.Graphics.ClipBounds.Width < MainCamera.xSize)
            {
                newXSize = MainCamera.xSize + (MainCamera.xSize - (int)e.Graphics.ClipBounds.Width);
                changed = true;
            }
            if (e.Graphics.ClipBounds.Height < MainCamera.ySize)
            {
                newYSize = MainCamera.ySize + (MainCamera.ySize - (int)e.Graphics.ClipBounds.Height);
                changed = true;
            }
            if (e.Graphics.ClipBounds.Height > MainCamera.ySize)
            {
                newYSize = engineDrawForm.Height - ((int)e.Graphics.ClipBounds.Height - MainCamera.ySize);
                changed = true;
            }
            if (e.Graphics.ClipBounds.Width > MainCamera.xSize)
            {
                newXSize = engineDrawForm.Width - ((int)e.Graphics.ClipBounds.Width - MainCamera.xSize);
                changed = true;
            }
            if (changed)
            {
                engineDrawForm.Size = new Size(newXSize, newYSize);
            }

            Draw(e.Graphics);
        }

        private static void Draw(Graphics graphics)
        {


            for (int i = 0; i < GameObjectManager.AllGameObjects.Count; i++)
            {
                GameObject go = GameObjectManager.AllGameObjects[i];

                if (go is Bullet)
                {
                    Bullet bullet = (Bullet)go;
                    bullet.DrawBullet(graphics, MainCamera);
                }
                else
                {
                    go.Draw(graphics, MainCamera);


                }
            }
            for (int i = 0; i < GameObjectManager.AllText.Count; i++)
            {
                UtalText utext = GameObjectManager.AllText[i];
                utext.DrawString(graphics);
            }

            healthBar.Draw(graphics);
        }

        private static void GenerateNPC()
        {
            Random random = new Random();
            Vector2 npcPosition = GetRandomPositionWithoutCollision();
            Console.WriteLine("Creado en" + npcPosition.ToString());

            // Crea el NPC en la posición generada
            new EnemigoPerseguidor(2, NPC, new Vector2(40, 48), player, npcPosition.x, npcPosition.y);
        }

        private static Vector2 GetRandomPositionWithoutCollision()
        {
            Random random = new Random();
            Vector2 position;

            do
            {
                // Genera una posición aleatoria dentro de los límites del mundo
                int x = random.Next(0, 1910 * 2);
                int y = random.Next(0, 1070 * 2);
                position = new Vector2(x, y);
            }
            while (HasCollisionWithSolidSurface(position));

            return position;
        }

        private static bool HasCollisionWithSolidSurface(Vector2 position)
        {
            CollisionDetector collisionDetector = new CollisionDetector();

            foreach (GameObject go in GameObjectManager.AllGameObjects)
            {
                if (go is Wall)
                {
                    foreach (Collider collider in go.rigidbody.colliders)
                    {
                        if (collisionDetector.DetectCollisionWithPoint(collider, position))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }




    }
}

