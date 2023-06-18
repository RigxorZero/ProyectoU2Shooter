using CanvasDrawing.UtalEngine2D_2023_1;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CanvasDrawing.Game
{
    public class GameOverScreen
    {
        private readonly Form form;
        private Size formSize;
        private readonly SynchronizationContext synchronizationContext;
        private Button restartButton;
        private bool isInitialized;
        public static Camera MainCamera = new Camera();

        public GameOverScreen(Form engineDrawForm)
        {
            form = engineDrawForm;
            formSize = form.Size;
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

            // Agrega el botón de reinicio
            restartButton = new Button();
            restartButton.Text = "Restart";
            restartButton.Size = new Size(100, 30);
            restartButton.Click += (buttonSender, buttonArgs) => RestartGame();
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
            // Aquí puedes agregar tu lógica de dibujo

            // Ejemplo: Dibujar un texto en el centro del formulario
            string gameOverText = "Game Over";
            Font font = new Font("Arial", 20, FontStyle.Bold);
            SizeF textSize = graphics.MeasureString(gameOverText, font);
            PointF textPosition = new PointF(form.Width / 2 - textSize.Width / 2, form.Height / 2 - textSize.Height / 2);

            // Dibuja el fondo del texto
            RectangleF textBackgroundRect = new RectangleF(textPosition.X, textPosition.Y, textSize.Width, textSize.Height);
            graphics.FillRectangle(Brushes.Yellow, textBackgroundRect);

            // Dibuja el texto
            graphics.DrawString(gameOverText, font, Brushes.Black, textPosition);

            // Posiciona el botón de reinicio
            restartButton.Location = new Point((form.Width - restartButton.Width) / 2, (int)(textBackgroundRect.Bottom + 20));


        }

        private void RestartGame()
        {
            // Lógica para reiniciar el juego, vuelve las variables a false
            GameEngine.playerWin = false;
            GameEngine.playerLost = false;

            form.Controls.Clear(); // Elimina todos los controles agregados al formulario

            // Reinicia el juego utilizando GameInitializer
            GameInitializer.InitializeGame(form);
            
        }
    }
}