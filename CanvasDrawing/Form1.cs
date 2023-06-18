using CanvasDrawing.Game;
using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System;
using System.Windows.Forms;
using System.Drawing;
using CanvasDrawing.Game;

namespace CanvasDrawing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            GameEngine.MainCamera.xSize = 1920;
            GameEngine.MainCamera.ySize = 1080;
            GameEngine.MainCamera.scale = 1f;
            GameEngine.MainCamera.Position = new Vector2(0, 0);

            GameInitializer.InitializeGame(this);

            new FrameManager();
            new TimerBoard();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Lógica para el evento de clic en el PictureBox
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            // Lógica para el evento de finalización del cambio de tamaño del formulario
            GameEngine.MainCamera.xSize = Width;
            GameEngine.MainCamera.ySize = Height;
        }
    }
}

