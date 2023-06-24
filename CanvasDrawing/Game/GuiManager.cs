
using System.Collections.Generic;
using System.Drawing;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class GuiManager //Manejador de la interfaz
    {
        private static List<GuiElement> guiElements = new List<GuiElement>();

        public static void AddGuiElement(GuiElement guiElement)
        {
            guiElements.Add(guiElement);
        }

        public static void RemoveGuiElement(GuiElement guiElement)
        {
            guiElements.Remove(guiElement);
        }
        public static void Draw(Graphics graphics)
        {
            foreach (GuiElement guiElement in guiElements)
            {
                guiElement.Draw(graphics);
            }
        }
    }
}
