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
    class Player : Sprite
    {
        public float speed = 100f;
        public float rotationSpeed = 90f;


        public Player(Texture2D texture, Vector2 pos) : base(texture, pos) { }
        public Player(Texture2D texture, Vector2 pos, List<Sprite> children) : base(texture, pos, children) { }
        public Player(Texture2D texture, Vector3 posAndRot) : base(texture, posAndRot) { }
        public Player(Texture2D texture, Vector3 posAndRot, List<Sprite> children) : base(texture, posAndRot, children) { }

        public override void Start()
        {

        }

        public override void Update()
        {
            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                Rotation -= rotationSpeed * GetFrameTime();
            }
            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                Rotation += rotationSpeed * GetFrameTime();
            }
        }
    }
}
