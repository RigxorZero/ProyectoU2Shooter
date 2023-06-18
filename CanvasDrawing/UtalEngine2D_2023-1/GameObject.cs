
using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using System;
using System.Drawing;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class GameObject
    {
        public class Renderer
        {
            public Image sprite;
            public Vector2 position;
            public Vector2 size;
            public float rotation;
        }

        public Rigidbody rigidbody;

        public Transform transform = new Transform();
        public Renderer renderer = new Renderer();
        public GameObject(Image newSprite, Vector2 newSize, float xPos = 0, float yPos = 0)
        {
            Init(newSprite, newSize, true, xPos, yPos);
        }
        public GameObject(Image newSprite, Vector2 newSize, bool hasCollider = true, float xPos = 0, float yPos = 0)
        {
            Init(newSprite, newSize, hasCollider, xPos, yPos);
        }
        public void Init(Image newSprite, Vector2 newSize, bool hasCollider = true, float xPos = 0, float yPos = 0)
        {
            transform.position = new Vector2(xPos, yPos);

            
            if (hasCollider)
            {
                
                rigidbody = new Rigidbody();
                rigidbody.transform = transform;
                rigidbody.CreateCircleCollider(newSize.x / 2);
                rigidbody.OnCollision = OnCollisionEnter;
                rigidbody.GetOnCollisionObject = GetOnCollision;
            }
            GameObjectManager.AllNewGameObjects.Add(this);
            renderer.sprite = newSprite;
            renderer.size = newSize;
        }

        public void OnCollisionEnter(Object otherO)
        {
            GameObject otherGO = otherO as GameObject;
            OnCollisionEnter(otherGO);
        }
        public Object GetOnCollision()
        {
            return this;
        }
        public virtual void OnCollisionEnter(GameObject other)
        {

        }

        public virtual void Start()
        {

        }
        public virtual void Update()
        {

        }

        public virtual void OnDestroy()
        {
            PhysicsEngine.Destroy(rigidbody);
        }
        public void Draw(Graphics graphics, Camera camera)
        {
            int xOffset = 0;
            int yOffset = 0;
            if(renderer == null)
            {
                return;
            }
            graphics.DrawImage(renderer.sprite,
                (transform.position.x - camera.Position.x - renderer.size.x / 2) * camera.scale + xOffset,
                (transform.position.y - camera.Position.y - renderer.size.y / 2) * camera.scale + yOffset,
                renderer.size.x / camera.scale, 
                renderer.size.y / camera.scale);
        }
    }
}
