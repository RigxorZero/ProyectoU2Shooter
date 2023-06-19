using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class SpriteRenderer
    {
        public Image Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float Rotation { get; set; }

        public void Draw(Graphics graphics, Camera camera, int xOffset, int yOffset)
        {
            if (Sprite == null)
            {
                return;
            }

            float scaledSizeX = Size.x / camera.scale;
            float scaledSizeY = Size.y / camera.scale;
            float scaledPositionX = (Position.x - camera.Position.x - Size.x / 2) * camera.scale + xOffset;
            float scaledPositionY = (Position.y - camera.Position.y - Size.y / 2) * camera.scale + yOffset;

            graphics.DrawImage(Sprite, scaledPositionX, scaledPositionY, scaledSizeX, scaledSizeY);
        }
    }
}

