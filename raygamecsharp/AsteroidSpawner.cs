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

        public int asteroidSizeModifier = 1;
        public int maxAsteroids = 35;
        public int asteroidVariants = 1;//the largest number on asteroid textures (must be one for all sizes)
        public int minAsteroidSpeed = 100;
        public int maxAsteroidSpeed = 200;
        Random random = new Random();
        float spawnWaitTime = 5f;//5
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
                //-----

                //random asteroid size
                int size = random.Next(1, 4) * asteroidSizeModifier;

                //random asteroid texture name
                string asteroidTextureName = "Asteroid" + random.Next(1, 5);

                //create asteroid
                //Asteroid a = new Asteroid(textures[asteroidTextureName],)

            }
        }
    }

}
