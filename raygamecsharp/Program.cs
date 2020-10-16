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
//using System.Numerics;          // mathematics types (Vector2, Vector3, etc.)
using System.Collections.Generic;
using Raylib_cs;
using System;

namespace M4GVisualTest
{
    public class core_window
    {

        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static List<Sprite> objects = new List<Sprite>();

        public static int Main()
        { 
            const int screenWidth = 1600;
            const int screenHeight = 900;
            InitWindow(screenWidth, screenHeight, "Math4Games");
            SetTargetFPS(60);
            LoadTextures();

            objects.Add(new Sprite(textures["MainShipPart"], new List<Sprite> { 
                new Sprite(textures["MainShipPart"],
                new List<Sprite> {
                    new Sprite(textures["MainShipPart"]) 
                }) 
            }));

            objects[0].transform.m7 = 100;
            objects[0].transform.m8 = 100;
            objects[0].children[0].transform.m7 = 100;
            objects[0].children[0].children[0].transform.m8 = 100;

            // Main game loop
            while (!WindowShouldClose())    // Detect window close button or ESC key
            {
                
                Update();
                objects[0].transform.SetRotateZ(0.1f);
                
                Draw();
                
            }

            CloseWindow();        // Close window and OpenGL context
            return 0;
        }

        public static void Update() 
        {
            foreach (Sprite sprite in objects)
            {
                sprite.Update();
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

        public static void LoadTextures() 
        {
            textures.Add("MainShipPart",LoadTexture("Textures/Parts/spaceParts_037.png"));
        }



    }
}