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
        private KeyValuePair<string, Image> Sprite;
        private Dictionary<string, Size> gunSpriteSizes;
        private Image currentSprite;
        private Player player;
        private float gunOffsetX;
        private float gunOffsetY;

        public Gun(Image spriteUp, Image spriteDown, Image spriteLeft, Image spriteRight, Player playerP)
            : base(spriteUp, new Vector2(12, 14), false, playerP.transform.position.x, playerP.transform.position.y)
        {
            gunSprites = new Dictionary<string, Image>
            {
                { "Up", ResizeImage(spriteUp, 12, 14) },
                { "Down", ResizeImage(spriteDown, 12, 14) },
                { "Left", ResizeImage(spriteLeft, 17, 10) },
                { "Right", ResizeImage(spriteRight, 17, 10) }
            };

            currentSprite = gunSprites["Down"];
            player = playerP;
            gunOffsetX = 0;
            gunOffsetY = -20;
        }

        public void Update(string playerAnimation)
        {
            if (gunSprites.ContainsKey(playerAnimation))
            {
                currentSprite = gunSprites[playerAnimation];
                spriteRenderer.Sprite = currentSprite;
            }

            switch (playerAnimation)
            {
                case "Up":
                    gunOffsetX = 0;
                    gunOffsetY = -20;
                    break;
                case "Down":
                    gunOffsetX = 0;
                    gunOffsetY = 20;
                    break;
                case "Left":
                    gunOffsetX = -20;
                    gunOffsetY = 0;
                    break;
                case "Right":
                    gunOffsetX = 20;
                    gunOffsetY = 0;
                    break;
                default:
                    gunOffsetX = 0;
                    gunOffsetY = 0;
                    break;
            }
        }

        public override void Draw(Graphics graphics, Camera camera)
        {
            int gunPositionX = (int)(player.transform.position.x + gunOffsetX);
            int gunPositionY = (int)(player.transform.position.y + gunOffsetY);

            spriteRenderer.Draw(graphics, camera, gunPositionX, gunPositionY);
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            Image resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }
    }
}





