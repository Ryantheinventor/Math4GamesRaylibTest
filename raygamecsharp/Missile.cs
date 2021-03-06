﻿using System;
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
    class Missile : Sprite
    {
        

        public Missile(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public Missile(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }
        public Missile(Texture2D texture, Vector3 posAndRot, string name = "New Sprite") : base(texture, posAndRot, name) { }
        public Missile(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite") : base(texture, posAndRot, children, name) { }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            //destroy self at edge of screen
            if (transform.m7 > 1650)
            {
                Destroy(this);
            }
            if (transform.m7 < -50)
            {
                Destroy(this);
            }
            if (transform.m8 > 950)
            {
                Destroy(this);
            }
            if (transform.m8 < -50)
            {
                Destroy(this);
            }
        }
    }
}
