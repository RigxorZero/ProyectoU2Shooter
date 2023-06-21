using System;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public class RectCollider : Collider
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public RectCollider(Rigidbody rigidbody, float width, float height) : base(rigidbody)
        {
            Width = width;
            Height = height;
        }

        public override bool CheckCollision(Collider other)
        {
            if (other is CircleCollider circleCollider)
            {
                // Verificar la intersección entre un colisionador rectangular y uno circular
                // Implementa tu lógica de intersección de rectángulo-círculo aquí
                Vector2 rectPosition = rigidbody.transform.position;
                Vector2 circlePosition = circleCollider.rigidbody.transform.position;

                float rectHalfWidth = Width / 2;
                float rectHalfHeight = Height / 2;
                float circleRadius = circleCollider.radius;

                float deltaX = Math.Abs(circlePosition.x - rectPosition.x);
                float deltaY = Math.Abs(circlePosition.y - rectPosition.y);

                if (deltaX > rectHalfWidth + circleRadius)
                    return false;

                if (deltaY > rectHalfHeight + circleRadius)
                    return false;

                if (deltaX <= rectHalfWidth || deltaY <= rectHalfHeight)
                    return true;

                float cornerDeltaX = deltaX - rectHalfWidth;
                float cornerDeltaY = deltaY - rectHalfHeight;
                float cornerDistanceSquared = cornerDeltaX * cornerDeltaX + cornerDeltaY * cornerDeltaY;

                return cornerDistanceSquared <= circleRadius * circleRadius;
            }
            else if (other is RectCollider rectCollider)
            {
                // Verificar la intersección entre dos colisionadores rectangulares
                // Implementa tu lógica de intersección de rectángulos aquí
                Vector2 thisPosition = rigidbody.transform.position;
                Vector2 otherPosition = rectCollider.rigidbody.transform.position;

                float thisHalfWidth = Width / 2;
                float thisHalfHeight = Height / 2;
                float otherHalfWidth = rectCollider.Width / 2;
                float otherHalfHeight = rectCollider.Height / 2;

                float deltaX = Math.Abs(otherPosition.x - thisPosition.x);
                float deltaY = Math.Abs(otherPosition.y - thisPosition.y);

                float intersectX = thisHalfWidth + otherHalfWidth - deltaX;
                float intersectY = thisHalfHeight + otherHalfHeight - deltaY;

                return intersectX > 0 && intersectY > 0;
            }
            else
            {
                throw new ArgumentException("Tipo de colisionador no compatible.");
            }
        }
    }
}


