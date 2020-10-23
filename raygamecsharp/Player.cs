using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;  // core methods (InitWindow, BeginDrawing())
using static Raylib_cs.Color;   // color (RAYWHITE, MAROON, etc.)
using MathClasses;
using System.Numerics;
using Vector3 = MathClasses.Vector3;
using static M4GVisualTest.Game;


namespace M4GVisualTest
{
    class Player : Sprite
    {
        public float maxSpeed = 350f;
        public float acceleration = 250f;
        public float rotationSpeed = 180f;
        public float shotTimer = 0.3f;
        private float curTime = 0;

        public Player(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public Player(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }
        public Player(Texture2D texture, Vector3 posAndRot, string name = "New Sprite") : base(texture, posAndRot, name) { }
        public Player(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite") : base(texture, posAndRot, children, name) { }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                collider.velocity += new Vector3(transform.m2, -transform.m5, 0) * (acceleration * GetFrameTime());
                float speed = MathF.Abs(MathF.Sqrt(MathF.Pow(collider.velocity.x, 2) + MathF.Pow(collider.velocity.y, 2)));
                if (speed > maxSpeed)
                {
                    float maxSpeedPercent = maxSpeed / speed;
                    collider.velocity.x *= maxSpeedPercent;
                    collider.velocity.y *= maxSpeedPercent;
                }
                foreach (Sprite s in children)
                {
                    if (s.objectName == "Thrust")
                    {
                        s.visable = true;
                    }
                }
            }
            else 
            {
                foreach (Sprite s in children)
                {
                    if (s.objectName == "Thrust")
                    {
                        s.visable = false;
                    }
                }
            }
            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                Rotation -= rotationSpeed * GetFrameTime();
                foreach (Sprite s in children)
                {
                    if (s.objectName == "ThrustA")
                    {
                        s.visable = true;
                    }
                }
            }
            else 
            {
                foreach (Sprite s in children)
                {
                    if (s.objectName == "ThrustA")
                    {
                        s.visable = false;
                    }
                }
            }

            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                Rotation += rotationSpeed * GetFrameTime();
                foreach (Sprite s in children)
                {
                    if (s.objectName == "ThrustD")
                    {
                        s.visable = true;
                    }
                }
            }
            else 
            {
                foreach (Sprite s in children)
                {
                    if (s.objectName == "ThrustD")
                    {
                        s.visable = false;
                    }
                }
            }
            if (curTime > 0) 
            {
                curTime -= GetFrameTime();
            }

            if (curTime <= 0 && IsKeyDown(KeyboardKey.KEY_SPACE)) 
            {
                Missile m = new Missile(textures["Missile"], new Vector3(transform.m7, transform.m8, Rotation), "Missile") { Scale = 0.3f };
                NewObject(m);
                m.Translate(new Vector3(transform.m2, -transform.m5, 0) * 50);
                m.collider.velocity = new Vector3(transform.m2, -transform.m5, 0) * 1000;
                curTime = shotTimer;
            }

            if (transform.m7 > 1650)
            {
                transform.m7 = -30;
            }
            if (transform.m7 < -50)
            {
                transform.m7 = 1630;
            }
            if (transform.m8 > 950)
            {
                transform.m8 = -30;
            }
            if (transform.m8 < -50)
            {
                transform.m8 = 930;
            }


        }
    }
}
