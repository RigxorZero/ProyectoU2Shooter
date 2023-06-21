using System;
using System.Collections.Generic;

namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public class Rigidbody
    {
        public Transform transform;
        public Vector2 lastPos;
        public List<Collider> colliders = new List<Collider>();
        public float mass;
        public Vector2 Velocity;
        public bool isStatic = false;

        public delegate void CollisionDelegate(Object o);
        public CollisionDelegate OnCollision;
        public delegate Object GetOnCollisionObjectDelegate();
        public GetOnCollisionObjectDelegate GetOnCollisionObject;

        public Rigidbody()
        {
            PhysicsEngine.allNewRigidbodies.Add(this);
        }
        public void SetTransform(Transform transform)
        {
            this.transform = transform;
            lastPos = transform.position;
        }
        public void CreateRectCollider(float width, float height)
        {
            colliders.Add(new RectCollider(this, width, height));
        }
        public void CreateCircleCollider(float radius)
        {
            colliders.Add(new CircleCollider(this, radius));
        }
        public bool CheckCollision(Rigidbody otherRigid)
        {
            foreach(Collider myC in colliders)
            {
                foreach(Collider otherC in otherRigid.colliders)
                {
                    if (myC.CheckCollision(otherC))
                    {
                        if(myC.isSolid && otherC.isSolid)
                        {
                            bool checkSecondColl = false;
                            Vector2 toOtherDirection = otherC.rigidbody.transform.position - transform.position;
                            if (!otherC.rigidbody.isStatic)
                            {
                                checkSecondColl = true;
                                otherC.rigidbody.transform.position = otherC.rigidbody.lastPos;                                                                                             
                            }
                            if (!isStatic)
                            {
                                checkSecondColl = true;
                                transform.position = lastPos;                                                               
                            }
                            if(checkSecondColl && myC.CheckCollision(otherC))
                            {
                                if (!otherC.rigidbody.isStatic)
                                {
                                    otherC.rigidbody.transform.position += toOtherDirection * Time.deltaTime;
                                }
                                if (!isStatic)
                                {
                                    transform.position -= toOtherDirection * Time.deltaTime;
                                }
                            }

                        }
                        if (OnCollision != null && otherRigid.GetOnCollisionObject != null)
                        {
                            OnCollision(otherRigid.GetOnCollisionObject());                            
                        }
                        if(GetOnCollisionObject != null && otherRigid.OnCollision != null)
                        {
                            otherRigid.OnCollision(GetOnCollisionObject());
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
