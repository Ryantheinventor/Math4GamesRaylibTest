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
    public class Collider
    {
        public Sprite parentSprite;
        public Vector3 velocity = new Vector3(0,0,0);


        public Collider(Sprite parentSprite) 
        {
            this.parentSprite = parentSprite;
        }



        public void DrawAABBCollider() 
        {


            float width = (float)Math.Sqrt(parentSprite.WorldTransform.m1 * parentSprite.WorldTransform.m1 + parentSprite.WorldTransform.m2 * parentSprite.WorldTransform.m2) * parentSprite.texture.width;
            float height = (float)Math.Sqrt(parentSprite.WorldTransform.m4 * parentSprite.WorldTransform.m4 + parentSprite.WorldTransform.m5 * parentSprite.WorldTransform.m5) * parentSprite.texture.height;
            float posX = parentSprite.WorldTransform.m7;
            float posY = parentSprite.WorldTransform.m8;


            DrawLine((int)(posX - width / 2), (int)(posY - height / 2), (int)(posX - width / 2), (int)(posY + height / 2), GREEN);
            DrawLine((int)(posX - width / 2), (int)(posY - height / 2), (int)(posX + width / 2), (int)(posY - height / 2), GREEN);
            DrawLine((int)(posX + width / 2), (int)(posY - height / 2), (int)(posX + width / 2), (int)(posY + height / 2), GREEN);
            DrawLine((int)(posX + width / 2), (int)(posY + height / 2), (int)(posX - width / 2), (int)(posY + height / 2), GREEN);
        }




    }
}
