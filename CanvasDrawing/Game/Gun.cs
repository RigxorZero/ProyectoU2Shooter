using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class Gun
    {
        private Dictionary<string, Image> gunSprites;
        private Image currentSprite;
        private Player player;

        public Gun(Image spriteUp, Image spriteDown, Image spriteLeft, Image spriteRight, Player player)
        {
            gunSprites = new Dictionary<string, Image>();
            gunSprites.Add("Up", spriteUp);
            gunSprites.Add("Down", spriteDown);
            gunSprites.Add("Left", spriteLeft);
            gunSprites.Add("Right", spriteRight);

            currentSprite = spriteDown;
            this.player = player;
        }

        public void Update(string playerAnimation)
        {
            // Actualizar el sprite del arma basado en la animación del jugador
            if (gunSprites.ContainsKey(playerAnimation))
            {
                currentSprite = gunSprites[playerAnimation];
            }
        }

        public void Draw(Graphics graphics, Camera camera)
        {
            // Dibujar el sprite del arma en la posición correcta en relación al jugador
            int xOffset = (int)player.transform.position.x;
            int yOffset = (int)player.transform.position.y;
            int gunOffsetX = 0; // Ajusta este valor según la posición del arma con respecto al jugador
            int gunOffsetY = 0; // Ajusta este valor según la posición del arma con respecto al jugador

            int drawX = xOffset + gunOffsetX;
            int drawY = yOffset + gunOffsetY;

            graphics.DrawImage(currentSprite, drawX, drawY);
        }
    }
}


