using CanvasDrawing.UtalEngine2D_2023_1;
using System;
using System.Drawing;

namespace CanvasDrawing.Game
{
    public class Frame : GameObject
    {
        public static int LASTID = 0;
        public float Speed = 1;
        public float timerMove = 0;
        public int myId = 0;
        static Random rand;
        public Camera myCamera;
        public UtalText text;
        public Vector2 lastPos;
        public Frame(float Speed, Image newsprite, Vector2 newSize, float x = 0, float y = 0) : base(newsprite, newSize, x, y)
        {
            FrameManager.AllFrames.Add(this);
            myId = LASTID++;
            myCamera = new Camera();
            this.Speed = Speed;
            spriteRenderer.Size = newSize;
            if (rand == null)
            {
                rand = new Random();
            }
            Vector2 textPosition = GameEngine.WorldToCameraPos(transform.position);
            lastPos = transform.position;

        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        public override void OnCollisionEnter(GameObject other)
        {
            //renderer.sprite.Dispose();
            //GameEngine.Destroy(this); //Destruye al NPC
            FrameManager.AllFrames.Remove(this);
            Bullet b = other as Bullet;
            if (b != null)
            {
                GameEngine.Destroy(b);
            }
        }

        public void GoBack()
        {
            transform.position = lastPos;
        }  
    }
}
