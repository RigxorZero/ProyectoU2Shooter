using System;
using System.Drawing;

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

        public override void DrawCollider(Graphics graphics, Camera camera)
        {
            // Dibujar el collider del rectángulo
            int x = (int)(rigidbody.transform.position.x - (Width / 2));
            int y = (int)(rigidbody.transform.position.y - (Height / 2));
            int w = (int)Width;
            int h = (int)Height;
            graphics.DrawRectangle(Pens.Red, x, y, w, h);
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




