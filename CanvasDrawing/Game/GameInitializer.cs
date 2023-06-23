﻿using CanvasDrawing.UtalEngine2D_2023_1;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public static class GameInitializer
    {

        public static void InitializeGame(Form form)
        {
            Image piso = Properties.Resources.Piso;
            Image muro = Properties.Resources.Muro;
            Image jugador = Properties.Resources._1_south1;
            Image NPC = Properties.Resources._3_south1;

            Random random = new Random();

            int playerX = random.Next(1, 11) * 50 + 25;
            int playerY = random.Next(1, 11) * 50 + 25;


            for (int i = 0; i < 76; i++)
            {
                for (int j = 0; j < 42; j++)
                {
                    if ((i == 0 || i == 75 || (i == 37 && (j != 10 && j != 11))) || j == 0 || j == 41 || (j == 20 && (i != 18 && i != 17)))
                    {
                        Wall wall = new Wall(muro, new Vector2(50, 50), i * 50 + 25, j * 50 + 25);
                        wall.rigidbody.isStatic = true;
                    }
                    else
                    {
                        new BackgroundElement(piso, new Vector2(50, 50), i * 50 + 25, j * 50 + 25);
                    }
                }
            }

            Player player = Player.GetInstance(2, jugador, new Vector2(40, 48), playerX, playerY);

            new EnemigoPerseguidor(2, NPC, new Vector2(40, 48), player, 500, 500);



            GameEngine.InitEngine(form);
        }
    }
}

