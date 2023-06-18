using System;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    class RectCollider
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public RectCollider(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public bool Intersects(RectCollider other)
        {
            // Verificar si hay intersección entre dos colliders rectangulares

            // Calcular los bordes de cada collider
            float thisLeft = -Width / 2;
            float thisRight = Width / 2;
            float thisTop = -Height / 2;
            float thisBottom = Height / 2;

            float otherLeft = -other.Width / 2;
            float otherRight = other.Width / 2;
            float otherTop = -other.Height / 2;
            float otherBottom = other.Height / 2;

            // Verificar si hay intersección en el eje X
            bool intersectX = thisLeft <= otherRight && thisRight >= otherLeft;

            // Verificar si hay intersección en el eje Y
            bool intersectY = thisTop <= otherBottom && thisBottom >= otherTop;

            // Hay intersección si hay intersección en ambos ejes
            return intersectX && intersectY;
        }
    }
}

