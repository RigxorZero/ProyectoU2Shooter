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

        /*public override bool CheckCollision(Collider other)
        {
            if (other is RectCollider otherRect)
            {
                Vector2 distVector = otherRect.rigidbody.transform.position - rigidbody.transform.position;
                float deltaX = Math.Abs(distVector.x);
                float deltaY = Math.Abs(distVector.y);
                float intersectX = (Width + otherRect.Width) / 2 - deltaX;
                float intersectY = (Height + otherRect.Height) / 2 - deltaY;

                if (intersectX > 0 && intersectY > 0)
                {
                    Console.WriteLine("Fue en RectCollider");
                    return true;
                }
            }
            return false;
        }*/
    }
}




