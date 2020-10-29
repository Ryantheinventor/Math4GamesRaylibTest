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
    class TimedObject : Sprite
    {
        public float waitTime = 5f;
        public TimedObject(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public TimedObject(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }
        public TimedObject(Texture2D texture, Vector3 posAndRot, string name = "New Sprite") : base(texture, posAndRot, name) { }
        public TimedObject(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite") : base(texture, posAndRot, children, name) { }

        public override void Update()
        {
            waitTime -= GetFrameTime();
            if (waitTime <= 0) 
            {
                Destroy(this);
            }
        }

        public override void Draw()
        {
            foreach (Sprite child in children)
            {
                child.Draw();
            }
        }
    }
}