using System;
using System.Drawing;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public class CircleCollider : Collider
    {
        public float radius;
        public CircleCollider(Rigidbody rigidbody, float radius) : base(rigidbody)
        {
            this.radius = radius;
        }

        public override bool CheckCollision(Collider other)
        {
            CircleCollider otherC = other as CircleCollider;
            if(otherC != null)
            {
                Vector2 distVector = otherC.rigidbody.transform.position - rigidbody.transform.position;
                float squareDist = distVector.x * distVector.x + distVector.y * distVector.y;
                float dist = (float)Math.Sqrt(squareDist);

                if (squareDist < (radius + otherC.radius) * (otherC.radius + otherC.radius))
                {
                    return true;
                }

            }
            return false;
        }

        public override void DrawCollider(Graphics graphics, Camera camera)
        {
            int xOffset = 0;
            int yOffset = 0;

            // Calcular la posición y el radio del círculo en las coordenadas de la cámara
            float circleX = (rigidbody.transform.position.x - camera.Position.x) * camera.scale + xOffset;
            float circleY = (rigidbody.transform.position.y - camera.Position.y) * camera.scale + yOffset;
            float circleRadius = radius * camera.scale;

            // Dibujar el círculo en el objeto Graphics
            graphics.DrawEllipse(Pens.Red, circleX - circleRadius / 2, circleY - circleRadius / 2, circleRadius, circleRadius);
        }
    }
}
