using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.Game
{
    public class Gun : GameObject
    {
        private Dictionary<string, Image> gunSprites;
        private Image currentSprite;
        private Player player;
        private float gunOffsetX;
        private float gunOffsetY;


        public Gun(Image spriteUp, Image spriteDown, Image spriteLeft, Image spriteRight, Player playerP)
            : base(spriteUp, new Vector2(12,14), false, playerP.transform.position.x, playerP.transform.position.y)
        {
            gunSprites = new Dictionary<string, Image>();
            gunSprites.Add("Up", spriteUp);
            gunSprites.Add("Down", spriteDown);
            gunSprites.Add("Left", spriteLeft);
            gunSprites.Add("Right", spriteRight);

            currentSprite = spriteDown;
            player = playerP;
            gunOffsetX = player.transform.position.x;
            gunOffsetY = player.transform.position.y;
        }

        public void Update(string playerAnimation)
        {
            // Actualizar el sprite del arma basado en la animación del jugador
            if (gunSprites.ContainsKey(playerAnimation))
            {
                currentSprite = gunSprites[playerAnimation];
            }
        }

        public override void Draw(Graphics graphics, Camera camera)
        {

            // Calcular la posición de la pistola en relación al jugador
            float gunPositionX = gunOffsetX;
            float gunPositionY = gunOffsetY;

            // Dibujar el sprite del arma en la posición calculada en la pantalla
            graphics.DrawImage(currentSprite, gunPositionX, gunPositionY);
        }

    }
}



