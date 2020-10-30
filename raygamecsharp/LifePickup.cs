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
    class LifePickup : Sprite
    {

        public LifePickup(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public LifePickup(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }
        public LifePickup(Texture2D texture, Vector3 posAndRot, string name = "New Sprite") : base(texture, posAndRot, name) { }
        public LifePickup(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite") : base(texture, posAndRot, children, name) { }

        public override void Update()
        {
            //screen wrap
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

        public override void OnCollision(Sprite other)
        {
            if (other.objectName == "Player") 
            {
                //give health to player
                ((Player)other).health++;
                //create pickup pop effect
                Sprite pPop = new TimedObject(textures["Square"], new Vector2(transform.m7, transform.m8), new List<Sprite> {
                        new ParticleSystem(textures["Square"], new Vector2(0, 0), "AsteroidPopParticles")
                        { color = Fade(RED, 0.7f), lifeTime = 1, scale = new Vector2(5, 5), minVelocity = new Vector2(-100, -100), maxVelocity = new Vector2(100, 100), particlePerEmit = 50, emitTime = 0, maxParticleCount = 50}
                    }, "LifePickup")
                { waitTime = 1 };
                NewObject(pPop);
                Destroy(this);
            }
        }

    }
}
