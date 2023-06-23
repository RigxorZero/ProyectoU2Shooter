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
        public int score { get; set; }

        public HealthBar(Vector2 position, Player player, Image heartImage, Image greyHeartImage)
            : base(position, new Size(100, 100)) // Tamaño de la barra de salud ajustado a 100x100
        {
            this.maxHealth = player.lifes;
            this.currentHealth = player.currentLifes;
            this.heartImage = ResizeImage(heartImage, 11 * 4, 9 * 4); // Redimensionar las imágenes de corazón a 44x36
            this.greyHeartImage = ResizeImage(greyHeartImage, 11 * 4, 9 * 4); // Redimensionar las imágenes de corazón gris a 44x36
        }

        public void UpdateCurrentHealth(int currentHealth, int scorePlayer)
        {
            this.currentHealth = currentHealth;
            this.score = scorePlayer;
        }

        public override void Draw(Graphics graphics)
        {
            int heartsOffsetX = 100; // Desplazamiento adicional para los corazones
            int heartsTotalWidth = maxHealth * heartImage.Width; // Ancho total de todos los corazones

            int startX = (int)position.x + size.Width - heartsTotalWidth + heartsOffsetX; // Calcular la posición de inicio de los corazones

            for (int i = 0; i < maxHealth; i++)
            {
                Image heart = i < currentHealth ? heartImage : greyHeartImage;
                int heartX = startX + i * heartImage.Width; // Ajustar la posición horizontal del corazón
                int heartY = (int)position.y;
                graphics.DrawImage(heart, heartX, heartY);
            }

            // Calcular la posición horizontal del texto
            string healthText = $"HP: ";
            Font font = new Font("Monogram", 50);
            Brush brush = Brushes.White;

            int totalHeartsWidth = maxHealth * heartImage.Width;
            int textWidth = (int)graphics.MeasureString(healthText, font).Width;
            int textX = (int)position.x - 50 + totalHeartsWidth - textWidth; // Ajustar la posición horizontal del texto
            int textY = (int)position.y + (heartImage.Height - font.Height) / 2; // Centrar verticalmente el texto con los corazones

            // Dibujar el valor de la barra de salud como texto
            graphics.DrawString(healthText, font, brush, textX, textY);

            // Dibujar el puntaje
            string scoreText = $"Score: {score}";
            int scoreTextWidth = (int)graphics.MeasureString(scoreText, font).Width;
            int scoreTextX = (int)position.x + 1700 + size.Width - scoreTextWidth + heartsTotalWidth - heartsOffsetX; // Calcular la posición del puntaje
            int scoreTextY = (int)position.y + (heartImage.Height - font.Height) / 2; // Centrar verticalmente el puntaje con los corazones
            graphics.DrawString(scoreText, font, brush, scoreTextX, scoreTextY);
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


