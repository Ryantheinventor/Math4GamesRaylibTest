/*******************************************************************************************
*
*   raylib [core] example - Basic window
*
*   Welcome to raylib!
*
*   To test examples, just press F6 and execute raylib_compile_execute script
*   Note that compiled executable is placed in the same folder as .c file
*
*   You can find all basic examples on C:\raylib\raylib\examples folder or
*
*   Enjoy using raylib. :)
*
*   This example has been created using raylib-cs 3.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   This example was lightly modified to provide additional 'using' directives to make
*   common math types and utilities readily available, though they are not using in this sample.
*
*   Copyright (c) 2019-2020 Academy of Interactive Entertainment (@aie_usa)
*   Copyright (c) 2013-2016 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using static Raylib_cs.Raylib;  // core methods (InitWindow, BeginDrawing())
using static Raylib_cs.Color;   // color (RAYWHITE, MAROON, etc.)
using static Raylib_cs.Raymath; // mathematics utilities and operations (Vector2Add, etc.)
using MathClasses;
using System.Numerics;          // mathematics types (Vector2, Vector3, etc.)
using System.Collections.Generic;
using Raylib_cs;
using System;
using Vector3 = MathClasses.Vector3;

namespace M4GVisualTest
{
    public class Game
    {

        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static List<Sprite> objects = new List<Sprite>();
        public static List<Sprite> queue = new List<Sprite>();
        public static List<Sprite> marked = new List<Sprite>();

        public static int Main()
        { 
            const int screenWidth = 1600;
            const int screenHeight = 900;
            InitWindow(screenWidth, screenHeight, "Math4Games");
            SetTargetFPS(60);
            LoadTextures();


            #region Objects

            objects.Add(new Player(textures["MainShipPart"], new Vector2(800, 450), new List<Sprite> {
                new Sprite(textures["ShipThruster"],new Vector3(10,40,0)){flipTexture = false, Scale = 0.2f, physicsEnabled = false},
                new Sprite(textures["ShipThruster"],new Vector3(-10,40,0)){flipTexture = false, Scale = 0.2f, physicsEnabled = false},
                new Sprite(textures["ShipThruster"],new Vector3(0,40,0)){flipTexture = false, Scale = 0.4f, physicsEnabled = false},
                new Sprite(textures["ShipWing1"],new Vector3(15,-11,0)){flipTexture = false, Scale = 1f, physicsEnabled = false},
                new Sprite(textures["ShipWing1"],new Vector3(-15,-11,0)){flipTexture = true, Scale = 1f, physicsEnabled = false},
                new Sprite(textures["MainShipPart"],new Vector3(0,0,0)){flipTexture = false, Scale = 1f, physicsEnabled = false},
                new Sprite(textures["ShipWindow"],new Vector3(0,0,0)){flipTexture = false, Scale = 0.5f, physicsEnabled = false, color = BLUE},
                new Sprite(textures["ShipWing3"],new Vector3(30,18,-40)){flipTexture = true, Scale = 0.8f, physicsEnabled = false},
                new Sprite(textures["ShipWing3"],new Vector3(-30,18,40)){flipTexture = false, Scale = 0.8f, physicsEnabled = false},
                new Sprite(textures["ShipWing2"],new Vector3(17,18,22)){flipTexture = false, Scale = 0.5f, physicsEnabled = false},
                new Sprite(textures["ShipWing2"],new Vector3(-17,18,-22)){flipTexture = true, Scale = 0.5f, physicsEnabled = false},
                new Sprite(textures["ShipWing1"],new Vector3(18,-14,-13)){flipTexture = false, Scale = 0.5f, physicsEnabled = false},
                new Sprite(textures["ShipWing1"],new Vector3(-18,-14,13)){flipTexture = true, Scale = 0.5f, physicsEnabled = false},
            })
            { Scale = 0.5f });//0.5f
            //objects.Add(new Player(textures["MainShipPart"], new Vector2(800, 450)));
            #endregion


            Start();
            // Main game loop
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                Update();
                Physics();
                Draw();
                WrapUpFrame();
            }

            CloseWindow();        // Close window and OpenGL context
            return 0;
        }

        public static void Start()
        {
            foreach (Sprite sprite in objects)
            {
                sprite.Start();
            }
        }

        public static void Update() 
        {
            foreach (Sprite sprite in objects)
            {
                sprite.Update();
            }
        }

        public static void Physics() 
        {
            foreach (Sprite g in objects) 
            {
                if (g.physicsEnabled) 
                {
                    g.Translate(g.collider.velocity * GetFrameTime());
                    //collision checks
                }
            }
        }

        public static void Draw() 
        {
            BeginDrawing();
            ClearBackground(BLACK);

            foreach (Sprite sprite in objects) 
            {
                sprite.Draw();
            }


            

            EndDrawing();
        }

        public static void WrapUpFrame() 
        {
            foreach (Sprite s in queue) 
            {
                objects.Add(s);
            }
            queue = new List<Sprite>();

            foreach (Sprite s in marked) 
            {
                objects.Remove(s);
            }
            marked = new List<Sprite>();

        }

        public static void LoadTextures() 
        {
            textures.Add("MainShipPart", LoadTexture("Textures/Parts/spaceParts_037.png"));
            textures.Add("ShipWing1", LoadTexture("Textures/Parts/spaceParts_007.png"));
            textures.Add("ShipWing2", LoadTexture("Textures/Parts/spaceParts_005.png"));
            textures.Add("ShipWing3", LoadTexture("Textures/Parts/spaceParts_004.png"));
            textures.Add("ShipWindow", LoadTexture("Textures/Parts/spaceParts_040.png"));
            textures.Add("ShipThruster", LoadTexture("Textures/Parts/spaceParts_046.png"));
            textures.Add("Missile", LoadTexture("Textures/Missiles/spaceMissiles_003.png"));
        }

        public static void NewObject(Sprite sprite) 
        {
            queue.Add(sprite);
            sprite.Start();
        }

    }
}