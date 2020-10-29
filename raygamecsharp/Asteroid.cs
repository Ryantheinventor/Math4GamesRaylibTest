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
    class Asteroid : Sprite
    {

        public int size = 1;
        public AsteroidSpawner spawner;
        Random random = new Random();
        public Player player;
        public Asteroid(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public Asteroid(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }
        public Asteroid(Texture2D texture, Vector3 posAndRot, string name = "New Sprite") : base(texture, posAndRot, name) { }
        public Asteroid(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite") : base(texture, posAndRot, children, name) { }

        public override void Start()
        {

        }

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
            if (other.objectName == "Missile") 
            {
                player.score += 10 + 5 * (size - 1);
                Destroy(other);
                if (size != 1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        //random rotation
                        int randomRot = random.Next(0, 360);

                        //random asteroid texture name
                        string asteroidTextureName = "Asteroid" + random.Next(1, 5);

                        //create asteroid
                        Asteroid a = new Asteroid(textures[asteroidTextureName], new Vector3(transform.m7, transform.m8, randomRot), "Asteroid") { Scale = (size - 1) * spawner.asteroidSizeModifier };
                        a.size = size - 1;
                        float speed = random.Next(spawner.minAsteroidSpeed, spawner.maxAsteroidSpeed);
                        float angleRad = (float)random.NextDouble() * 2;
                        a.collider.velocity.x = MathF.Cos(angleRad) * speed;
                        a.collider.velocity.y = MathF.Sin(angleRad) * speed;
                        a.spawner = spawner;
                        a.player = player;
                        NewObject(a);
                    }
                }
                if (random.Next(0, 15) == 0) 
                {
                    //create life pickup
                    Sprite lifePickup = new LifePickup(textures["Heart"], new Vector2(transform.m7, transform.m8), new List<Sprite> {
                        new ParticleSystem(textures["Square"], new Vector2(0, 0), "LifeParticles")
                        { color = Fade(RED, 0.7f), lifeTime = 2, scale = new Vector2(5, 5), minVelocity = new Vector2(-10, -10), maxVelocity = new Vector2(10, -5) }
                    }, "LifePickup")
                    { Rotation = 0 };
                    lifePickup.collider.velocity = collider.velocity*0.1f;
                    NewObject(lifePickup);
                }
                //create asteroid pop effect
                Sprite aPop = new TimedObject(textures["Square"], new Vector2(transform.m7, transform.m8), new List<Sprite> {
                        new ParticleSystem(textures["Square"], new Vector2(0, 0), "AsteroidPopParticles")
                        { color = Fade(BROWN, 0.7f), lifeTime = 1, scale = new Vector2(5, 5), minVelocity = new Vector2(-100, -100), maxVelocity = new Vector2(100, 100), particlePerEmit = 50, emitTime = 0}
                    }, "LifePickup")
                { waitTime = 1 };
                NewObject(aPop);

                Destroy(this);
            }
            if (other.objectName == "Player") 
            {
                ((Player)other).health--;
                
                Destroy(this);
            }
        }

    }
}