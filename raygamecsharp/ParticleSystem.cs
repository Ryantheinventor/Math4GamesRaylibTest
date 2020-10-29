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
    class ParticleSystem : Sprite
    {
        private float curEmitTime = 0;
        public float emitTime = 0.1f;
        public int particlePerEmit = 1;
        public float lifeTime = 10f;
        public int maxParticleCount = 100;
        public Vector2 minVelocity = new Vector2(-30, -30);
        public Vector2 maxVelocity = new Vector2(30, -15);
        public Vector2 scale = new Vector2(10, 10);
        
        List<ParticleData> particles = new List<ParticleData>();
        List<ParticleData> deadParticles = new List<ParticleData>();
        Random random = new Random();

        public ParticleSystem(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public ParticleSystem(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }


        public override void Draw()
        {
            //remove old particles
            foreach (ParticleData p in particles)
            {
                if (p.lifeTime > lifeTime) 
                {
                    deadParticles.Add(p);
                }
            }
            foreach (ParticleData p in deadParticles)
            { 
                particles.Remove(p);
            }
            deadParticles = new List<ParticleData>();

            //add new particles
            curEmitTime += GetFrameTime();
            int emited = 0;
            while (curEmitTime >= emitTime && particles.Count < maxParticleCount && emited < particlePerEmit) 
            {
                emited++;
                particles.Add(new ParticleData(new Vector2(randomValue(minVelocity.X,maxVelocity.X), randomValue(minVelocity.Y, maxVelocity.Y)), new Vector2(WorldTransform.m7, WorldTransform.m8), scale));
                if (emited >= particlePerEmit) 
                {
                    curEmitTime = 0;
                }
            }

            //move particles
            foreach (ParticleData p in particles)
            {
                p.lifeTime += GetFrameTime();
                p.screenPos += p.velocity * GetFrameTime();
                Rectangle sourceRec = new Rectangle(0, 0, texture.width, texture.height);
                Rectangle destRec = new Rectangle(p.screenPos.X, p.screenPos.Y, p.particleScale.X, p.particleScale.Y);
                DrawTexturePro(texture, sourceRec, destRec, new Vector2(0, 0), 0, color);
            }

        }

        /// <summary>
        /// Get a random float in a range of two numbers
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private float randomValue(float start, float end) 
        {
            if (start < end)
            {
                float rangeSize = end - start;

                float rPick = (float)random.NextDouble();

                rangeSize *= rPick;
                rangeSize += start;

                return rangeSize;
            }
            return 0;
        }


    }

    public class ParticleData 
    {
        public Vector2 velocity;
        public Vector2 screenPos;
        public Vector2 particleScale;
        public float lifeTime;

        public ParticleData(Vector2 velocity, Vector2 screenPos, Vector2 particleScale) 
        {
            this.velocity = velocity;
            this.screenPos = screenPos;
            this.particleScale = particleScale;
        }


    }

}
