using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using static Raylib_cs.Raylib;  // core methods (InitWindow, BeginDrawing())
using static Raylib_cs.Color;   // color (RAYWHITE, MAROON, etc.)
using static Raylib_cs.Raymath; // mathematics utilities and operations (Vector2Add, etc.)
using MathClasses;
using System.Numerics;

namespace M4GVisualTest
{
    public class Sprite
    {
        public Texture2D texture;
        public Matrix3 transform = new Matrix3();
        public List<Sprite> children = new List<Sprite>();
        public Sprite parent = null;

        public Sprite(Texture2D texture) 
        {
            this.texture = texture;
        }

        public Sprite(Texture2D texture, List<Sprite> children)
        {
            this.texture = texture;
            this.children = children;
            foreach (Sprite c in this.children) 
            {
                c.parent = this;
            }
        }

        public void Update() 
        {

            foreach (Sprite child in children)
            {
                child.Update();
            }
        }

        public void Draw()
        {
            Rectangle sourceRec = new Rectangle(0, 0, texture.width, texture.height);
            NPatchInfo patch = new NPatchInfo();
            patch.sourceRec = sourceRec;

            Matrix3 worldMatrix = GetWorldMatrix();

            float width = (float)Math.Sqrt(worldMatrix.m1 * worldMatrix.m1 + worldMatrix.m2 * worldMatrix.m2) * texture.width;
            float height = (float)Math.Sqrt(worldMatrix.m4 * worldMatrix.m4 + worldMatrix.m5 * worldMatrix.m5) * texture.height;
            float posX = worldMatrix.m7;// - width / 2;
            float posY = worldMatrix.m8;// - height / 2;

            


            float rotation = ((float)Math.Atan2(worldMatrix.m1, worldMatrix.m4))*180/(float)Math.PI;
            //(float)Math.Acos(0.5 * (worldMatrix.m1 + worldMatrix.m5 + worldMatrix.m9 - 1));
            Console.WriteLine("----");
            worldMatrix.PrintMatrixToConsole();
            Console.WriteLine(rotation);
            Console.WriteLine("----");


            Rectangle destRec = new Rectangle(posX, posY, width, height);
            //DrawTexture(texture, (int)posX, (int)posY, WHITE);
            //DrawTexturePro(texture, sourceRec, destRec, new Vector2(width / 2, height / 2), 45, WHITE);
            DrawTextureNPatch(texture, patch, destRec, new Vector2(width/2, height/2), rotation, WHITE);
            foreach (Sprite child in children)
            {
                child.Draw();
            }
        }



        public Matrix3 GetWorldMatrix() 
        {
            if (parent == null)
            {
                return transform;
            }
            else 
            {
                return parent.GetWorldMatrix() * transform;
            }
        }

    }
}
