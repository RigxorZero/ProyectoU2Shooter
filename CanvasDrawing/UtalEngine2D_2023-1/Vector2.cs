
using System;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

        public static Vector2 Zero
        {
            get { return new Vector2(0f, 0f); }
        }

        public override string ToString()
        {
            return "x " + x + " y " + y;
        }

        public void Normalize()
        {
            float length = (float)Math.Sqrt(x * x + y * y);
            x /= length;
            y /= length;
        }

        public static Vector2 Rotate(Vector2 vector, float angle)
        {
            // Convertir el ángulo a radianes
            float angleRadians = MathHelper.ToRadians(angle);

            // Calcular seno y coseno del ángulo
            float sin = (float)Math.Sin(angleRadians);
            float cos = (float)Math.Cos(angleRadians);

            // Calcular las nuevas coordenadas x e y después de la rotación
            float newX = vector.x * cos - vector.y * sin;
            float newY = vector.x * sin + vector.y * cos;

            // Retornar el vector rotado
            return new Vector2(newX, newY);
        }

        public static float Distance(Vector2 value1, Vector2 value2)
        {
            float dx = value1.x - value2.x;
            float dy = value1.y - value2.y;

            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

    }
}

