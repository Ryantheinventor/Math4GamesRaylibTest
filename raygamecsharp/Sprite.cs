using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;  // core methods (InitWindow, BeginDrawing())
using static Raylib_cs.Color;   // color (RAYWHITE, MAROON, etc.)
using MathClasses;
using System.Numerics;
using Vector3 = MathClasses.Vector3;

namespace M4GVisualTest
{
    public class Sprite
    {
        public Texture2D texture;
        public Matrix3 transform = new Matrix3();
        public List<Sprite> children = new List<Sprite>();
        public Sprite parent = null;
        public Collider collider;
        public bool flipTexture;
        public bool physicsEnabled = true;
        public string objectName = "";
        public Color color = WHITE;
        public bool visable = true;

        /// <summary>
        /// Transform relative to world
        /// </summary>
        public Matrix3 WorldTransform 
        {
            get 
            {
                if (parent == null)
                {
                    return transform;
                }
                else
                {
                    return parent.WorldTransform * transform;
                }
            }
        }

        /// <summary>
        /// World rotation (no local rotation sorry)
        /// </summary>
        public float Rotation
        {

            get => (((float)Math.Atan2(WorldTransform.m1, WorldTransform.m4)) * 180 / (float)Math.PI) + 90;//the line ive been trying to fix for the past hour
            set
            {
                transform.SetRotateZ((Rotation - value) * (float)Math.PI / 180);//the fix
                //transform.SetRotateZ((value - Rotation) * (float)Math.PI / 180);//old version
            }

        }

        /// <summary>
        /// Local scale 
        /// </summary>
        public float Scale
        {
            get => (float)Math.Sqrt(transform.m1 * transform.m1 + transform.m2 * transform.m2);
            set
            {
                float scaleVal = value / Scale;
                Matrix3 scaleBy = new Matrix3(scaleVal, 0, 0, 0, scaleVal, 0, 0, 0, 1);
                transform *= scaleBy;
            }
        }

        public Sprite(Texture2D texture, Vector2 pos, string name = "New Sprite")
        {
            this.texture = texture;
            transform.m7 = pos.X;
            transform.m8 = pos.Y;
            this.collider = new Collider(this);
            objectName = name;
        }

        public Sprite(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite")
        {
            this.texture = texture;
            this.children = children;
            transform.m7 = pos.X;
            transform.m8 = pos.Y;
            foreach (Sprite c in this.children)
            {
                c.parent = this;
            }
            this.collider = new Collider(this);
            objectName = name;
        }

        public Sprite(Texture2D texture, Vector3 posAndRot, string name = "New Sprite")
        {
            this.texture = texture;
            transform.m7 = posAndRot.x;
            transform.m8 = posAndRot.y;
            Rotation = posAndRot.z;
            this.collider = new Collider(this);
            objectName = name;
        }

        public Sprite(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite")
        {
            this.texture = texture;
            this.children = children;
            transform.m7 = posAndRot.x;
            transform.m8 = posAndRot.y;
            Rotation = posAndRot.z;
            foreach (Sprite c in this.children)
            {
                c.parent = this;
            }
            this.collider = new Collider(this);
            objectName = name;
        }

        /// <summary>
        /// Start is run after object creation
        /// </summary>
        public virtual void Start()
        {

            foreach (Sprite child in children)
            {
                child.Start();
            }
        }


        /// <summary>
        /// Update is run once per frame before physics are calculated
        /// </summary>
        public virtual void Update() 
        {

            foreach (Sprite child in children)
            {
                child.Update();
            }
        }

        /// <summary>
        /// Draw sprite onto screen
        /// </summary>
        public virtual void Draw()
        {
            if (visable)
            {
                Rectangle sourceRec = new Rectangle();
                if (flipTexture)
                {
                    sourceRec = new Rectangle(0, 0, -texture.width, texture.height);
                }
                else
                {
                    sourceRec = new Rectangle(0, 0, texture.width, texture.height);
                }
                NPatchInfo patch = new NPatchInfo();
                patch.sourceRec = sourceRec;

                float width = (float)Math.Sqrt(WorldTransform.m1 * WorldTransform.m1 + WorldTransform.m2 * WorldTransform.m2) * texture.width;
                float height = (float)Math.Sqrt(WorldTransform.m4 * WorldTransform.m4 + WorldTransform.m5 * WorldTransform.m5) * texture.height;
                float posX = WorldTransform.m7;
                float posY = WorldTransform.m8;
                Rectangle destRec = new Rectangle(posX, posY, width, height);

                DrawTextureNPatch(texture, patch, destRec, new Vector2(width / 2, height / 2), Rotation, color);
            }
            foreach (Sprite child in children)
            {
                child.Draw();
            }
            if (physicsEnabled)
            {
                collider.DrawOBBCollider();
                //collider.DrawAABBCollider();
            }
        }

        public virtual void UIDraw() 
        {
            foreach (Sprite child in children)
            {
                child.Update();
            }
        }


        public void Translate(Vector3 translation) 
        {
            transform.m7 += translation.x;
            transform.m8 += translation.y;
        }

        public virtual void OnCollision(Sprite other) 
        { 
            
        }



    }


    /*
    class BaseLayout : Sprite
    {

        public BaseLayout(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public BaseLayout(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }
        public BaseLayout(Texture2D texture, Vector3 posAndRot, string name = "New Sprite") : base(texture, posAndRot, name) { }
        public BaseLayout(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite") : base(texture, posAndRot, children, name) { }

        public override void Start()
        {

        }

        public override void Update()
        {

        }
    }
    */
}
