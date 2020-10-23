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
    class AsteroidSpawner : Sprite {

        public float asteroidSizeModifier = 0.1f;
        public int maxAsteroids = 35;//35
        public int asteroidVariants = 1;//the largest number on asteroid textures (must be one for all sizes)
        public int minAsteroidSpeed = 100;
        public int maxAsteroidSpeed = 200;
        Random random = new Random();
        float spawnWaitTime = 3f;//3
        float curWaitTime = 0f;
        Player player;

        public AsteroidSpawner(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public AsteroidSpawner(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }
        public AsteroidSpawner(Texture2D texture, Vector3 posAndRot, string name = "New Sprite") : base(texture, posAndRot, name) { }
        public AsteroidSpawner(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite") : base(texture, posAndRot, children, name) { }

        public override void Start()
        {
            foreach (Sprite s in objects)
            {
                if (s.objectName == "Player") 
                {
                    player = (Player)s;
                }
            }
        }

        public override void Update()
        {
            curWaitTime += GetFrameTime();
            int asteroidCount = 0;
            foreach (Sprite s in objects) 
            {
                if (s.objectName == "Asteroid") 
                {
                    asteroidCount++;
                }
            }
            if (curWaitTime > spawnWaitTime && asteroidCount < maxAsteroids)
            {

                //random spawn
                Vector2 spawnPos = new Vector2(random.Next(0, 1600), random.Next(0, 900));
                while (spawnPos.X < player.transform.m7 + 150 && spawnPos.X > player.transform.m7 - 150) //keep new asteroids away from the player
                {
                    spawnPos.X = random.Next(0, 1600);
                }

                //random rotation
                int randomRot = random.Next(0, 360);

                //random asteroid size
                int size = random.Next(1, 4);

                //random asteroid texture name
                string asteroidTextureName = "Asteroid" + random.Next(1, 5);

                //create asteroid
                Asteroid a = new Asteroid(textures[asteroidTextureName], new Vector3(spawnPos.X, spawnPos.Y, randomRot), "Asteroid") { Scale = size * asteroidSizeModifier};
                a.size = size;
                float speed = random.Next(minAsteroidSpeed, maxAsteroidSpeed);
                float angleRad = (float)random.NextDouble() * 2;
                a.collider.velocity.x = MathF.Cos(angleRad) * speed;
                a.collider.velocity.y = MathF.Sin(angleRad) * speed;
                a.spawner = this;
                NewObject(a);
                curWaitTime = 0;
            }
        }
    }

}
