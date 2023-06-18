using CanvasDrawing.UtalEngine2D_2023_1;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public static class GameInitializer
    {
        public static void InitializeGame(Form form)
        {
            Image madera = Properties.Resources.Madera;
            Image muro = Properties.Resources.Muro;
            Image portalwin = Properties.Resources.Portal;
            Image jugador = Properties.Resources.MainChar;
            Image NPC = Properties.Resources.Ghost;

            Random random = new Random();

            int playerX = random.Next(1, 11) * 50 + 25;
            int playerY = random.Next(1, 11) * 50 + 25;


            for (int i = 0; i < 76; i++)
            {
                for (int j = 0; j < 42; j++)
                {
                    if ((i == 0 || i == 75 || (i == 37 && (j != 10 && j != 11))) || j == 0 || j == 41 || (j == 20 && (i != 18 && i != 17)))
                    {
                        Wall wall = new Wall(muro, new Vector2(50, 50), i * 49 + 25, j * 49 + 25);
                        wall.rigidbody.isStatic = true;
                    }
                    else
                    {
                        new BackgroundElement(madera, new Vector2(50, 50), i * 49 + 25, j * 49 + 25);
                        int aux = random.Next(0, 2);
                    }
                }
            }

            new Player(2, jugador, new Vector2(50, 50), playerX, playerY);

            GameEngine.InitEngine(form);
        }
    }
}

