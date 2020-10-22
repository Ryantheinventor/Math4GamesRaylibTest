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

        public Player(Texture2D texture, Vector2 pos) : base(texture, pos) { }
        public Player(Texture2D texture, Vector2 pos, List<Sprite> children) : base(texture, pos, children) { }
        public Player(Texture2D texture, Vector3 posAndRot) : base(texture, posAndRot) { }
        public Player(Texture2D texture, Vector3 posAndRot, List<Sprite> children) : base(texture, posAndRot, children) { }

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

            }
            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                Rotation -= rotationSpeed * GetFrameTime();
            }
            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                Rotation += rotationSpeed * GetFrameTime();
            }
            if (curTime > 0) 
            {
                curTime -= GetFrameTime();
            }

            if (curTime <= 0 && IsKeyDown(KeyboardKey.KEY_SPACE)) 
            {
                Missile m = new Missile(textures["Missile"], new Vector3(transform.m7, transform.m8, Rotation), "Misslie") { Scale = 0.3f };
                NewObject(m);
                m.collider.velocity = new Vector3(transform.m2, -transform.m5, 0) * 1000;
                curTime = shotTimer;
            }
        }
    }
}
