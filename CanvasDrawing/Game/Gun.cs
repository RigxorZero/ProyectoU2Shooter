using CanvasDrawing.UtalEngine2D_2023_1;
using System.Collections.Generic;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public class Gun : GameObject
    {
        private readonly Dictionary<string, Image> gunSprites;
        public Image currentSprite;
        private GameObject owner;
        private float gunOffsetX;
        private float gunOffsetY;

        public Gun(Image spriteUp, Image spriteDown, Image spriteLeft, Image spriteRight, GameObject owner)
            : base(spriteUp, CalculateSize(spriteUp), false, owner.transform.position.x, owner.transform.position.y)
        {
            //Diccionario de sprite del arma
            gunSprites = new Dictionary<string, Image>
            {
                { "Up", ResizeImage(spriteUp, CalculateWidth(spriteUp), CalculateHeight(spriteUp)) },
                { "Down", ResizeImage(spriteDown, CalculateWidth(spriteDown), CalculateHeight(spriteDown)) },
                { "Left", ResizeImage(spriteLeft, CalculateWidth(spriteLeft), CalculateHeight(spriteLeft)) },
                { "Right", ResizeImage(spriteRight, CalculateWidth(spriteRight), CalculateHeight(spriteRight)) }
            };

            currentSprite = gunSprites["Down"];
            this.owner = owner;
            gunOffsetX = 0;
            gunOffsetY = -currentSprite.Height;
        }
        public void Update(string ownerAnimation)
        {
            if (gunSprites.ContainsKey(ownerAnimation))
            {
                currentSprite = gunSprites[ownerAnimation];
                spriteRenderer.Sprite = currentSprite;
            }
            //A veces tira error, aun no comprobamos que lo provoca
            switch (ownerAnimation)
            {
                case "Up":
                    gunOffsetX = 0;
                    gunOffsetY = -currentSprite.Height;
                    break;
                case "Down":
                    gunOffsetX = 0;
                    gunOffsetY = currentSprite.Height;
                    break;
                case "Left":
                    gunOffsetX = -currentSprite.Width;
                    gunOffsetY = 0;
                    break;
                case "Right":
                    gunOffsetX = currentSprite.Width;
                    gunOffsetY = 0;
                    break;
                default:
                    gunOffsetX = 0;
                    gunOffsetY = 0;
                    break;
            }
        }
        //Dibuja el arma
        public override void Draw(Graphics graphics, Camera camera)
        {
            int gunPositionX = (int)(owner.transform.position.x + gunOffsetX);
            int gunPositionY = (int)(owner.transform.position.y + gunOffsetY);

            spriteRenderer.Draw(graphics, camera, gunPositionX, gunPositionY);
        }
        //Redimensiona el sprite según el arma
        private Image ResizeImage(Image image, int width, int height)
        {
            Image resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }
        //Calcula el nuevo tamaño
        private static Vector2 CalculateSize(Image image)
        {
            return new Vector2(CalculateWidth(image), CalculateHeight(image));
        }
        private static int CalculateWidth(Image image)
        {
            return image.Width;
        }
        private static int CalculateHeight(Image image)
        {
            return image.Height;
        }
    }
}






