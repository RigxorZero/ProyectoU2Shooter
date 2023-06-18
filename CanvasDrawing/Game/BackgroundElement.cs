using CanvasDrawing.UtalEngine2D_2023_1;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public class BackgroundElement : GameObject
    {
        public BackgroundElement(Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0) : base(newSprite, newSize,false, xPos, yPos)
        {
        }
    }
}
