using CanvasDrawing.Game;
using System.Drawing;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class HealthBar : GuiElement
    {
        private int maxHealth;
        private int currentHealth;
        private Image heartImage;
        private Image greyHeartImage;
        private Image hpImage; // Nueva variable para la imagen de "hp"

        public HealthBar(Vector2 position, Player player, Image heartImage, Image greyHeartImage, Image hpImage)
            : base(position, new Size(100, 100)) // Tamaño de la barra de salud ajustado a 100x100
        {
            this.maxHealth = player.lifes;
            this.currentHealth = player.currentLifes;
            this.heartImage = ResizeImage(heartImage, 11 * 4, 9 * 4); // Redimensionar las imágenes de corazón a 44x36
            this.greyHeartImage = ResizeImage(greyHeartImage, 11 * 4, 9 * 4); // Redimensionar las imágenes de corazón gris a 44x36
            this.hpImage = ResizeImage(hpImage, 16 * 4, 10 * 4); // Redimensionar la imagen "hp" a 64x40
        }

        public void UpdateCurrentHealth(int currentHealth)
        {
            this.currentHealth = currentHealth;
        }

        public override void Draw(Graphics graphics)
        {
            // Dibujar la imagen "hp"
            graphics.DrawImage(hpImage, (int)position.x, (int)position.y);

            for (int i = 0; i < maxHealth; i++)
            {
                Image heart = i < currentHealth ? heartImage : greyHeartImage;
                int heartX = (int)position.x + hpImage.Width + i * heartImage.Width; // Ajustar la posición en función de la imagen "hp"
                int heartY = (int)position.y;
                graphics.DrawImage(heart, heartX, heartY);
            }
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            // Redimensionar la imagen al tamaño especificado
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }
    }
}

