using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class SpriteRenderer //Reemplaza a renderer de GameObject
    {
        public Image Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float Rotation { get; set; }

        public void Draw(Graphics graphics, Camera camera, int xOffset, int yOffset, float rotation = 0f)
        {
            if (Sprite == null)
            {
                return;
            }

            float scaledSizeX = Size.x / camera.scale;
            float scaledSizeY = Size.y / camera.scale;
            float scaledPositionX = (Position.x - camera.Position.x - Size.x / 2) * camera.scale + xOffset;
            float scaledPositionY = (Position.y - camera.Position.y - Size.y / 2) * camera.scale + yOffset;

            // Aplicar la rotación si se proporciona un valor distinto de cero
            if (rotation != 0f)
            {
                // Crear una matriz de transformación de rotación
                Matrix rotationMatrix = new Matrix();
                rotationMatrix.RotateAt(rotation, new PointF(scaledPositionX + scaledSizeX / 2, scaledPositionY + scaledSizeY / 2));

                // Guardar la transformación actual de la matriz de gráficos
                Matrix originalMatrix = graphics.Transform;

                // Aplicar la transformación de rotación a la matriz de gráficos
                graphics.Transform = rotationMatrix;

                // Dibujar la imagen rotada
                graphics.DrawImage(Sprite, scaledPositionX, scaledPositionY, scaledSizeX, scaledSizeY);

                // Restaurar la matriz de transformación original
                graphics.Transform = originalMatrix;
            }
            else
            {
                // Dibujar la imagen sin rotación
                graphics.DrawImage(Sprite, scaledPositionX, scaledPositionY, scaledSizeX, scaledSizeY);
            }
        }
    }
}

