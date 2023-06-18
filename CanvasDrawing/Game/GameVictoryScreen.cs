using CanvasDrawing.UtalEngine2D_2023_1;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CanvasDrawing.Game
{
    public class GameVictoryScreen
    {
        private readonly Form form;
        private Size formSize;
        private readonly SynchronizationContext synchronizationContext;
        private Button exitButton;
        private bool isInitialized;
        public static Camera MainCamera = new Camera();

        public GameVictoryScreen(Form engineDrawForm)
        {
            form = engineDrawForm;
            formSize = form.Size;
            synchronizationContext = SynchronizationContext.Current;
            engineDrawForm.Height = MainCamera.ySize;
            engineDrawForm.Width = MainCamera.xSize;

        }

        private void InitializeGameVictoryScreen()
        {
            if (isInitialized)
                return;

            // Desactiva temporalmente AutoSizeMode del formulario
            form.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Agrega el botón de exit
            exitButton = new Button();
            exitButton.Text = "EXIT";
            exitButton.Size = new Size(100, 30);
            exitButton.Click += (buttonSender, buttonArgs) => ExitGame();
            form.Controls.Add(exitButton);

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
            synchronizationContext.Send(state => DrawGameVictoryScreen(), null);
        }

        private void DrawGameVictoryScreen()
        {
            InitializeGameVictoryScreen();
            // Invalida el formulario para disparar el evento Paint
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            // Dibuja la imagen "Win"
            Image winImage = Properties.Resources.Win;
            Rectangle destinationRectangle = new Rectangle(0, 0, form.Width, form.Height);
            graphics.DrawImage(winImage, destinationRectangle);

            // Dibuja un texto en el centro del formulario
            string gameWinText = "VICTORY";
            Font font = new Font("Arial", 20, FontStyle.Bold);
            SizeF textSize = graphics.MeasureString(gameWinText, font);
            PointF textPosition = new PointF(form.Width / 2 - textSize.Width / 2, form.Height / 2 - textSize.Height / 2);

            // Dibuja el fondo del texto
            RectangleF textBackgroundRect = new RectangleF(textPosition.X, textPosition.Y, textSize.Width, textSize.Height);
            graphics.FillRectangle(Brushes.Yellow, textBackgroundRect);

            // Dibuja el texto
            graphics.DrawString(gameWinText, font, Brushes.Black, textPosition);

            // Posiciona el botón de reinicio
            exitButton.Location = new Point((form.Width - exitButton.Width) / 2, (int)(textBackgroundRect.Bottom + 20));


        }

        private void ExitGame()
        {
            Application.Exit();
        }
    }
}
