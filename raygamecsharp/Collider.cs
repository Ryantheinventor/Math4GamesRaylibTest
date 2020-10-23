#define ShowCollisionDebug
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
    public class Collider
    {
        public Sprite parentSprite;
        public Vector3 velocity = new Vector3(0,0,0);
        public Vector2 pointA = new Vector2();
        public Vector2 pointB = new Vector2();
        public Vector2 pointC = new Vector2();
        public Vector2 pointD = new Vector2();
        public Vector2 center = new Vector2();
        public Matrix3 boxTransform = new Matrix3();
        private Color color = GREEN;

        public Collider(Sprite parentSprite) 
        {
            this.parentSprite = parentSprite;
        }

        public void UpdatePoints() 
        {
            boxTransform.Set(parentSprite.transform * new Matrix3(parentSprite.texture.width / 2, 0, 0, 0, parentSprite.texture.height / 2, 0, 0, 0, 1));
            pointA = new Vector2(-boxTransform.m1 + -boxTransform.m4 + boxTransform.m7, -boxTransform.m2 + -boxTransform.m5 + boxTransform.m8);
            pointB = new Vector2(boxTransform.m1 + -boxTransform.m4 + boxTransform.m7, boxTransform.m2 + -boxTransform.m5 + boxTransform.m8);
            pointC = new Vector2(boxTransform.m1 + boxTransform.m4 + boxTransform.m7, boxTransform.m2 + boxTransform.m5 + boxTransform.m8);
            pointD = new Vector2(-boxTransform.m1 + boxTransform.m4 + boxTransform.m7, -boxTransform.m2 + boxTransform.m5 + boxTransform.m8);
            center = new Vector2(boxTransform.m7, boxTransform.m8);
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

        public void DrawOBBCollider()
        {
#if ShowCollisionDebug
            DrawLine((int)pointA.X, (int)pointA.Y, (int)pointB.X, (int)pointB.Y, color);
            DrawLine((int)pointB.X, (int)pointB.Y, (int)pointC.X, (int)pointC.Y, color);
            DrawLine((int)pointC.X, (int)pointC.Y, (int)pointD.X, (int)pointD.Y, color);
            DrawLine((int)pointD.X, (int)pointD.Y, (int)pointA.X, (int)pointA.Y, color);
#endif
        }

        public bool CheckCollision(Collider other) 
        {
            CollisionChecksPerFrame++;
            Vector3 center3 = new Vector3(center.X, center.Y, 0);
            Vector3 otherCenter3 = new Vector3(other.center.X, other.center.Y, 0);
            float dist = (float)Math.Sqrt((otherCenter3.x - center3.x) * (otherCenter3.x - center3.x) + (otherCenter3.y - center3.y) * (otherCenter3.y - center3.y));
            float range = new Vector3(boxTransform.m1, boxTransform.m2, 0).Magnitude() + new Vector3(boxTransform.m4, boxTransform.m5, 0).Magnitude()
                + new Vector3(other.boxTransform.m1, other.boxTransform.m2, 0).Magnitude() + new Vector3(other.boxTransform.m4, other.boxTransform.m5, 0).Magnitude();
            if (dist>range) 
            {
                return false;
            }


            {//other in this
                Vector3 xExtent = new Vector3(boxTransform.m1, boxTransform.m2, 0);
                Vector3 yExtent = new Vector3(boxTransform.m4, boxTransform.m5, 0);

                {//other point A in this
                    Vector2 preV = other.pointA - center;
                    Vector3 v = new Vector3(preV.X, preV.Y, 0);
#if ShowCollisionDebug
                    DrawLine((int)center.X, (int)center.Y, (int)other.pointA.X, (int)other.pointA.Y, RED);
#endif
                    CollisionChecksPerFrame++;
                    if (!(Math.Sqrt(Math.Abs(v.Dot(xExtent))) > xExtent.Magnitude() || Math.Sqrt(Math.Abs(v.Dot(yExtent))) > yExtent.Magnitude()))
                    {
#if ShowCollisionDebug
                        DrawLine((int)center.X, (int)center.Y, (int)other.pointA.X, (int)other.pointA.Y, GREEN);
#endif
                        return true;
                    }
                }

                {//other point B in this
                    Vector2 preV = other.pointB - center;
                    Vector3 v = new Vector3(preV.X, preV.Y, 0);
#if ShowCollisionDebug
                    DrawLine((int)center.X, (int)center.Y, (int)other.pointB.X, (int)other.pointB.Y, RED);
#endif
                    CollisionChecksPerFrame++;
                    if (!(Math.Sqrt(Math.Abs(v.Dot(xExtent))) > xExtent.Magnitude() || Math.Sqrt(Math.Abs(v.Dot(yExtent))) > yExtent.Magnitude()))
                    {
#if ShowCollisionDebug
                        DrawLine((int)center.X, (int)center.Y, (int)other.pointB.X, (int)other.pointB.Y, GREEN);
#endif
                        return true;
                    }
                }

                {//other point C in this
                    Vector2 preV = other.pointC - center;
                    Vector3 v = new Vector3(preV.X, preV.Y, 0);
#if ShowCollisionDebug
                    DrawLine((int)center.X, (int)center.Y, (int)other.pointC.X, (int)other.pointC.Y, RED);
#endif
                    CollisionChecksPerFrame++;
                    if (!(Math.Sqrt(Math.Abs(v.Dot(xExtent))) > xExtent.Magnitude() || Math.Sqrt(Math.Abs(v.Dot(yExtent))) > yExtent.Magnitude()))
                    {
#if ShowCollisionDebug
                        DrawLine((int)center.X, (int)center.Y, (int)other.pointC.X, (int)other.pointC.Y, GREEN);
#endif
                        return true;
                    }
                }

                {//other point D in this
                    Vector2 preV = other.pointD - center;
                    Vector3 v = new Vector3(preV.X, preV.Y, 0);
#if ShowCollisionDebug
                    DrawLine((int)center.X, (int)center.Y, (int)other.pointD.X, (int)other.pointD.Y, RED);
#endif
                    CollisionChecksPerFrame++;
                    if (!(Math.Sqrt(Math.Abs(v.Dot(xExtent))) > xExtent.Magnitude() || Math.Sqrt(Math.Abs(v.Dot(yExtent))) > yExtent.Magnitude()))
                    {
#if ShowCollisionDebug
                        DrawLine((int)center.X, (int)center.Y, (int)other.pointD.X, (int)other.pointD.Y, GREEN);
#endif
                        return true;
                    }
                }
            }

            {//this in other
                Vector3 xExtent = new Vector3(other.boxTransform.m1, other.boxTransform.m2, 0);
                Vector3 yExtent = new Vector3(other.boxTransform.m4, other.boxTransform.m5, 0);
#if ShowCollisionDebug
                DrawLine((int)other.center.X, (int)other.center.Y, (int)other.center.X + (int)xExtent.x, (int)other.center.Y + (int)xExtent.y, DARKPURPLE);
                DrawLine((int)other.center.X, (int)other.center.Y, (int)other.center.X + (int)yExtent.x, (int)other.center.Y + (int)yExtent.y, DARKPURPLE);
#endif
                {//this point A in other
                    Vector2 preV = pointA - other.center;
                    Vector3 v = new Vector3(preV.X, preV.Y, 0);
#if ShowCollisionDebug
                    DrawLine((int)other.center.X, (int)other.center.Y, (int)pointA.X, (int)pointA.Y, RED);
#endif
                    CollisionChecksPerFrame++;
                    if (!(Math.Sqrt(Math.Abs(v.Dot(xExtent))) > xExtent.Magnitude() || Math.Sqrt(Math.Abs(v.Dot(yExtent))) > yExtent.Magnitude()))
                    {
#if ShowCollisionDebug
                        DrawLine((int)other.center.X, (int)other.center.Y, (int)pointA.X, (int)pointA.Y, GREEN);
#endif
                        return true;
                    }
                }

                {//this point B in other
                    Vector2 preV = pointB - other.center;
                    Vector3 v = new Vector3(preV.X, preV.Y, 0);
#if ShowCollisionDebug
                    DrawLine((int)other.center.X, (int)other.center.Y, (int)pointB.X, (int)pointB.Y, RED);
#endif
                    CollisionChecksPerFrame++;
                    if (!(Math.Sqrt(Math.Abs(v.Dot(xExtent))) > xExtent.Magnitude() || Math.Sqrt(Math.Abs(v.Dot(yExtent))) > yExtent.Magnitude()))
                    {
#if ShowCollisionDebug
                        DrawLine((int)other.center.X, (int)other.center.Y, (int)pointB.X, (int)pointB.Y, GREEN);
#endif
                        return true;
                    }
                }

                {//this point C in other
                    Vector2 preV = pointC - other.center;
                    Vector3 v = new Vector3(preV.X, preV.Y, 0);
#if ShowCollisionDebug
                    DrawLine((int)other.center.X, (int)other.center.Y, (int)pointC.X, (int)pointC.Y, RED);
#endif
                    CollisionChecksPerFrame++;
                    if (!(Math.Sqrt(Math.Abs(v.Dot(xExtent))) > xExtent.Magnitude() || Math.Sqrt(Math.Abs(v.Dot(yExtent))) > yExtent.Magnitude()))
                    {
#if ShowCollisionDebug
                        DrawLine((int)other.center.X, (int)other.center.Y, (int)pointC.X, (int)pointC.Y, GREEN);
#endif
                        return true;
                    }
                }

                {//this point D in other
                    Vector2 preV = pointD - other.center;
                    Vector3 v = new Vector3(preV.X, preV.Y, 0);
#if ShowCollisionDebug
                    DrawLine((int)other.center.X, (int)other.center.Y, (int)pointD.X, (int)pointD.Y, RED);
#endif
                    CollisionChecksPerFrame++;
                    if (!(Math.Sqrt(Math.Abs(v.Dot(xExtent))) > xExtent.Magnitude() || Math.Sqrt(Math.Abs(v.Dot(yExtent))) > yExtent.Magnitude()))
                    {
#if ShowCollisionDebug
                        DrawLine((int)other.center.X, (int)other.center.Y, (int)pointD.X, (int)pointD.Y, GREEN);
#endif
                        return true;
                    }
                }
            }

            return false;
        }


    }
}
