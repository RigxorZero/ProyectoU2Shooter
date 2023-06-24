using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public class CollisionDetector //Detecta todas las colisiones posibles
    {
        public bool DetectCollision(Collider collider1, Collider collider2)
        {
            if (collider1 is RectCollider rectCollider1 && collider2 is RectCollider rectCollider2)
            {
                // Lógica de detección de colisión entre dos RectColliders
                return DetectRectRectCollision(rectCollider1, rectCollider2);
            }

            if (collider1 is CircleCollider circleCollider1 && collider2 is CircleCollider circleCollider2)
            {
                // Lógica de detección de colisión entre dos CircleColliders
                return DetectCircleCircleCollision(circleCollider1, circleCollider2);
            }

            if (collider1 is RectCollider rectCollider && collider2 is CircleCollider)
            {
                // Lógica de detección de colisión entre un RectCollider y un CircleCollider
                return DetectRectCircleCollision(rectCollider, (CircleCollider)collider2);
            }

            if (collider1 is CircleCollider circleCollider && collider2 is RectCollider)
            {
                // Lógica de detección de colisión entre un CircleCollider y un RectCollider
                return DetectRectCircleCollision((RectCollider)collider2, circleCollider);
            }

            return false;
        }

        public bool DetectCollisionWithPoint(Collider collider, Vector2 point)
        {
            if (collider is RectCollider rectCollider)
            {
                return DetectRectPointCollision(rectCollider, point);
            }

            if (collider is CircleCollider circleCollider)
            {
                return DetectCirclePointCollision(circleCollider, point);
            }

            return false;
        }

        private bool DetectRectPointCollision(RectCollider rectCollider, Vector2 point)
        {
            float rectHalfWidth = rectCollider.Width / 2;
            float rectHalfHeight = rectCollider.Height / 2;

            float rectX = rectCollider.rigidbody.transform.position.x;
            float rectY = rectCollider.rigidbody.transform.position.y;

            float closestX = Math.Max(Math.Min(point.x, rectX + rectHalfWidth), rectX - rectHalfWidth);
            float closestY = Math.Max(Math.Min(point.y, rectY + rectHalfHeight), rectY - rectHalfHeight);

            float deltaX = point.x - closestX;
            float deltaY = point.y - closestY;

            float distanceSquared = deltaX * deltaX + deltaY * deltaY;

            return distanceSquared <= 0;
        }

        private bool DetectCirclePointCollision(CircleCollider circleCollider, Vector2 point)
        {
            Vector2 distVector = point - circleCollider.rigidbody.transform.position;
            float squareDist = distVector.x * distVector.x + distVector.y * distVector.y;
            float squareRadius = circleCollider.radius * circleCollider.radius;

            return squareDist <= squareRadius;
        }


        private bool DetectRectRectCollision(RectCollider rectCollider1, RectCollider rectCollider2)
        {
            Vector2 distVector = rectCollider2.rigidbody.transform.position - rectCollider1.rigidbody.transform.position;
            float deltaX = Math.Abs(distVector.x);
            float deltaY = Math.Abs(distVector.y);
            float intersectX = (rectCollider1.Width + rectCollider2.Width) / 2 - deltaX;
            float intersectY = (rectCollider1.Height + rectCollider2.Height) / 2 - deltaY;

            if (intersectX > 0 && intersectY > 0)
            {
                //Console.WriteLine("Fue en Rect y Rect");
                return true;
            }

            return false;
        }

        private bool DetectCircleCircleCollision(CircleCollider circleCollider1, CircleCollider circleCollider2)
        {
            Vector2 distVector = circleCollider2.rigidbody.transform.position - circleCollider1.rigidbody.transform.position;
            float squareDist = distVector.x * distVector.x + distVector.y * distVector.y;
            float sumRadii = circleCollider1.radius + circleCollider2.radius;
            float squareSumRadii = sumRadii * sumRadii;

            if (squareDist < squareSumRadii)
            {
                //Console.WriteLine("Fue en Circle y Circle");
                return true;
            }

            return false;
        }


        private bool DetectRectCircleCollision(RectCollider rectCollider, CircleCollider circleCollider)
        {
            float rectHalfWidth = rectCollider.Width / 2;
            float rectHalfHeight = rectCollider.Height / 2;

            float circleX = circleCollider.rigidbody.transform.position.x;
            float circleY = circleCollider.rigidbody.transform.position.y;

            float rectX = rectCollider.rigidbody.transform.position.x;
            float rectY = rectCollider.rigidbody.transform.position.y;

            float closestX = Math.Max(Math.Min(circleX, rectX + rectHalfWidth), rectX - rectHalfWidth);
            float closestY = Math.Max(Math.Min(circleY, rectY + rectHalfHeight), rectY - rectHalfHeight);

            float deltaX = circleX - closestX;
            float deltaY = circleY - closestY;

            float distanceSquared = deltaX * deltaX + deltaY * deltaY;
            float circleRadiusSquared = circleCollider.radius * circleCollider.radius;

            if (distanceSquared < circleRadiusSquared)
            {
                //Console.WriteLine("Fue en Rect y Circle");
                return true;
            }

            return false;
        }


    }
}


