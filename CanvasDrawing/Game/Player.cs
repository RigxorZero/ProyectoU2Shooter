using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public class Player : Frame
    {        
        public Player(float Speed, Image newsprite, Vector2 newSize, float x = 0, float y = 0) : base(Speed, newsprite, newSize, x, y)
        {

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
        public override void Update()
        {
            Vector2 auxLastPos = transform.position;
            bool moved = false;
            float moveSpeed = Speed*100; // Velocidad de movimiento del jugador

            // Comprueba qué teclas están siendo presionadas y mueve al jugador en consecuencia
            if (InputManager.GetKeyDown(System.Windows.Forms.Keys.W))
            {
                if (transform.position.y - (moveSpeed * Time.deltaTime) > 0)
                {
                    transform.position.y -= moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKeyDown(System.Windows.Forms.Keys.S))
            {
                if (transform.position.y + (moveSpeed * Time.deltaTime) < 1080 * 2)
                {
                    transform.position.y += moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKeyDown(System.Windows.Forms.Keys.A))
            {
                if (transform.position.x - (moveSpeed * Time.deltaTime) > 0)
                {
                    transform.position.x -= moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }
            if (InputManager.GetKeyDown(System.Windows.Forms.Keys.D))
            {
                if (transform.position.x + (moveSpeed * Time.deltaTime) < 1920 * 2)
                {
                    transform.position.x += moveSpeed * Time.deltaTime;
                    moved = true;
                }
            }

            if (moved)
            {
                lastPos = auxLastPos;

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
