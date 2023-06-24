using CanvasDrawing.UtalEngine2D_2023_1;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CanvasDrawing.Game
{
    public class GameOverScreen //Pantalla derrota
    {
        private readonly Form form;
        private readonly SynchronizationContext synchronizationContext;
        private Button restartButton;
        private bool isInitialized;
        public static Camera MainCamera = new Camera();

        public GameOverScreen(Form engineDrawForm)
        {
            form = engineDrawForm;
            synchronizationContext = SynchronizationContext.Current;
            engineDrawForm.Height = MainCamera.ySize;
            engineDrawForm.Width = MainCamera.xSize;
        }
        private void InitializeGameOverScreen()
        {
            if (isInitialized)
                return;

            // Desactiva temporalmente AutoSizeMode del formulario
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Agrega el botón de salida
            restartButton = new Button();
            restartButton.Text = "Salir";
            restartButton.Size = new Size(100, 30);
            restartButton.BackColor = SystemColors.Control;
            restartButton.Click += (buttonSender, buttonArgs) => ExitGame(); // Cambia el método a llamar a ExitGame()
            form.Controls.Add(restartButton);

            // Restablece AutoSizeMode del formulario
            //form.SizeChanged += StaticSize;
            form.AutoSize = false;

            isInitialized = true;

            form.Paint -= GameEngine.Paint;
            form.Paint += Form_Paint;
            form.Refresh();
            //form.Paint -= Form_Paint;
        }
        public void Show()
        {
            synchronizationContext.Send(state => DrawGameOverScreen(), null);
        }
        private void DrawGameOverScreen()
        {
            InitializeGameOverScreen();
            // Invalida el formulario para disparar el evento Paint
        }
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            // Dibuja la imagen "Lose"
            Image loseImage = Properties.Resources.Lose; // Asegúrate de que el recurso "Lose" esté agregado a los recursos del proyecto
            Rectangle destinationRectangle = new Rectangle(0, 0, form.Width, form.Height);
            graphics.DrawImage(loseImage, destinationRectangle);

            // Dibuja el contenido adicional de la pantalla de Game Over

            string gameOverText = "Game Over";
            Font font = new Font("Arial", 20, FontStyle.Bold);
            SizeF textSize = graphics.MeasureString(gameOverText, font);
            PointF textPosition = new PointF(form.Width / 2 - textSize.Width / 2, form.Height / 2 - textSize.Height / 2);

            // Calcula las dimensiones y posiciones del rectángulo de fondo del texto
            float textPadding = 10;
            float textBackgroundWidth = textSize.Width + textPadding * 2;
            float textBackgroundHeight = textSize.Height + textPadding * 2;
            PointF textBackgroundPosition = new PointF(form.Width / 2 - textBackgroundWidth / 2, form.Height / 2 - textBackgroundHeight / 2);

            // Dibuja el fondo del texto
            graphics.FillRectangle(Brushes.Blue, textBackgroundPosition.X, textBackgroundPosition.Y, textBackgroundWidth, textBackgroundHeight);

            // Dibuja el texto
            graphics.DrawString(gameOverText, font, Brushes.White, textPosition);

            // Dibuja el puntaje final
            string scoreText = "Score: " + Player.score.ToString();
            Font scoreFont = new Font("Arial", 16);
            SizeF scoreTextSize = graphics.MeasureString(scoreText, scoreFont);
            PointF scoreTextPosition = new PointF(form.Width / 2 - scoreTextSize.Width / 2, textBackgroundPosition.Y + textBackgroundHeight + textPadding);

            // Calcula las dimensiones y posiciones del rectángulo de fondo del puntaje final
            float scorePadding = 10;
            float scoreBackgroundWidth = scoreTextSize.Width + scorePadding * 2;
            float scoreBackgroundHeight = scoreTextSize.Height + scorePadding * 2;
            PointF scoreBackgroundPosition = new PointF(form.Width / 2 - scoreBackgroundWidth / 2, scoreTextPosition.Y);

            // Dibuja el fondo del puntaje final
            graphics.FillRectangle(Brushes.Blue, scoreBackgroundPosition.X, scoreBackgroundPosition.Y, scoreBackgroundWidth, scoreBackgroundHeight);

            // Dibuja el texto del puntaje
            graphics.DrawString(scoreText, scoreFont, Brushes.White, scoreTextPosition);

            // Posiciona el botón de reinicio
            restartButton.Location = new Point((form.Width - restartButton.Width) / 2, (int)(scoreBackgroundPosition.Y + scoreBackgroundHeight + 40));
        }

        private void ExitGame() // Agrega el método ExitGame()
        {
            // Lógica para salir del juego
            Application.Exit();
        }

    }
}