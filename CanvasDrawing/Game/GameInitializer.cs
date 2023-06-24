using CanvasDrawing.UtalEngine2D_2023_1;
using System.Windows.Forms;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public static class GameInitializer //Dibuja el tablero
    {
        public static void InitializeGame(Form form)
        {
            Image piso = Properties.Resources.Piso;
            Image muro = Properties.Resources.Muro;

            for (int i = 0; i < 76; i++)
            {
                for (int j = 0; j < 42; j++)
                {
                    if ((i == 0 || i == 75 || j == 0 || j == 41) // Paredes externas
                        || (i == 37 && (j != 10 && j != 11 && j != 28 && j != 29)) // Bloque central horizontal
                        || (j == 20 && (i != 17 && i != 18 && i != 53 && i != 54)) // Bloque central vertical
                        || (i == 30 && (j < 10 || j > 31)) // Bloque vertical izquierdo
                        || (i == 45 && (j < 10 || j > 31)) // Bloque vertical derecho
                        || (j == 5 && (i < 16 || i > 59)) // Bloque horizontal superior
                        || (j == 36 && (i < 16 || i > 59))) // Bloque horizontal inferior
                    {
                        Wall wall = new Wall(muro, new Vector2(50, 50), i * 50 + 25, j * 50 + 25);
                        wall.rigidbody.isStatic = true;
                        wall.CreateRectCollider(50, 50); // Asignar un RectCollider al muro con ancho y altura de 50
                    }
                    else
                    {
                        new BackgroundElement(piso, new Vector2(50, 50), i * 50 + 25, j * 50 + 25);
                    }
                }
            }
            GameEngine.InitEngine(form);
        }
    }
}


