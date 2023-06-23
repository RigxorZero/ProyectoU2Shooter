using System;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public class CircleCollider : Collider
    {
        public float radius;
        public CircleCollider(Rigidbody rigidbody, float radius) : base(rigidbody)
        {
            this.radius = radius;
        }

        /*public override bool CheckCollision(Collider other)
        {
            CircleCollider otherC = other as CircleCollider;
            if(otherC != null)
            {
                Vector2 distVector = otherC.rigidbody.transform.position - rigidbody.transform.position;
                float squareDist = distVector.x * distVector.x + distVector.y * distVector.y;
                float dist = (float)Math.Sqrt(squareDist);

                if (squareDist < (radius + otherC.radius) * (otherC.radius + otherC.radius))
                {
                    Console.WriteLine("Fue en CircleCollider");
                    return true;
                }

            }
            return false;
        }*/
    }
}
