﻿using CanvasDrawing.UtalEngine2D_2023_1.Physics;
using CanvasDrawing.UtalEngine2D_2023_1;
using System.Drawing;
using System;

public class GameObject
{
    public Rigidbody rigidbody;
    public Transform transform = new Transform();
    public SpriteRenderer spriteRenderer = new SpriteRenderer();

    public GameObject(Image newSprite, Vector2 newSize, float xPos, float yPos)
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
            rigidbody.SetTransform(transform);
            rigidbody.CreateCircleCollider(newSize.x / 2);
            rigidbody.OnCollision = OnCollisionEnter;
            rigidbody.GetOnCollisionObject = GetOnCollision;
        }

        GameObjectManager.AllNewGameObjects.Add(this);
        spriteRenderer.Sprite = newSprite;
        spriteRenderer.Size = newSize;
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

    public void Dead()
    {

    }

    public void Draw(Graphics graphics, Camera camera)
    {
        int xOffset = (int)transform.position.x;
        int yOffset = (int)transform.position.y;
        spriteRenderer.Draw(graphics, camera, xOffset, yOffset);
    }

}

