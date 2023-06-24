
using System.Drawing;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public abstract class GuiElement //Elemento de interfaz gráfica
    {
        protected Vector2 position;
        protected Size size;

        public GuiElement(Vector2 position, Size size)
        {
            this.position = position;
            this.size = size;
        }
        public abstract void Draw(Graphics graphics);
    }
}

