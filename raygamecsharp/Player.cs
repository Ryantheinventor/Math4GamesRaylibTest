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
    class Player : Sprite
    {
        public float maxSpeed = 350f;
        public float acceleration = 250f;
        public float rotationSpeed = 180f;
        public float shotTimer = 0.3f;
        private float curTime = 0;
        public int health = 3;
        public int score = 0;
        public bool gameOver = false;

        public Player(Texture2D texture, Vector2 pos, string name = "New Sprite") : base(texture, pos, name) { }
        public Player(Texture2D texture, Vector2 pos, List<Sprite> children, string name = "New Sprite") : base(texture, pos, children, name) { }
        public Player(Texture2D texture, Vector3 posAndRot, string name = "New Sprite") : base(texture, posAndRot, name) { }
        public Player(Texture2D texture, Vector3 posAndRot, List<Sprite> children, string name = "New Sprite") : base(texture, posAndRot, children, name) { }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            if (!gameOver)
            {
                base.Update();
                if (IsKeyDown(KeyboardKey.KEY_W))
                {
                    //apply forward thrust
                    Vector3 direction = new Vector3(transform.m2, -transform.m5, 0);
                    direction.Normalize();
                    collider.velocity += direction * (acceleration * GetFrameTime());

                    //keep velocity at a max
                    float speed = MathF.Abs(MathF.Sqrt(MathF.Pow(collider.velocity.x, 2) + MathF.Pow(collider.velocity.y, 2)));
                    if (speed > maxSpeed)
                    {
                        float maxSpeedPercent = maxSpeed / speed;
                        collider.velocity.x *= maxSpeedPercent;
                        collider.velocity.y *= maxSpeedPercent;
                    }

                    //show flame comming out main thruster
                    foreach (Sprite s in children)
                    {
                        if (s.objectName == "Thrust")
                        {
                            s.visable = true;
                        }
                    }
                }
                else
                {
                    //hide flame comming out main thruster
                    foreach (Sprite s in children)
                    {
                        if (s.objectName == "Thrust")
                        {
                            s.visable = false;
                        }
                    }
                }

                //rotate ship
                if (IsKeyDown(KeyboardKey.KEY_A))
                {
                    Rotation -= rotationSpeed * GetFrameTime();
                    //show flame comming out side thruster
                    foreach (Sprite s in children)
                    {
                        if (s.objectName == "ThrustA")
                        {
                            s.visable = true;
                        }
                    }
                }
                else
                {
                    //hide flame comming out side thruster
                    foreach (Sprite s in children)
                    {
                        if (s.objectName == "ThrustA")
                        {
                            s.visable = false;
                        }
                    }
                }
                if (IsKeyDown(KeyboardKey.KEY_D))
                {
                    Rotation += rotationSpeed * GetFrameTime();
                    //show flame comming out side thruster
                    foreach (Sprite s in children)
                    {
                        if (s.objectName == "ThrustD")
                        {
                            s.visable = true;
                        }
                    }
                }
                else
                {
                    //hide flame comming out side thruster
                    foreach (Sprite s in children)
                    {
                        if (s.objectName == "ThrustD")
                        {
                            s.visable = false;
                        }
                    }
                }

                //count down for fire rate
                if (curTime > 0)
                {
                    curTime -= GetFrameTime();
                }

                //fire new missile
                if (curTime <= 0 && IsKeyDown(KeyboardKey.KEY_SPACE))
                {
                    Missile m = new Missile(textures["Missile"], new Vector3(transform.m7, transform.m8, Rotation), new List<Sprite> {
                        new ParticleSystem(textures["Square"], new Vector2(0, 0), "MissileParticles")
                        { color = Fade(YELLOW, 0.2f), lifeTime = 2, scale = new Vector2(5, 5), minVelocity = new Vector2(-10, -10), maxVelocity = new Vector2(10, 10) },
                        new ParticleSystem(textures["Square"], new Vector2(0, 150), "MissileParticles")
                        { color = Fade(YELLOW, 0.2f), lifeTime = 2, scale = new Vector2(5, 5), minVelocity = new Vector2(-10, -10), maxVelocity = new Vector2(10, 10) },
                        new ParticleSystem(textures["Square"], new Vector2(0, 50), "MissileParticles")
                        { color = Fade(YELLOW, 0.2f), lifeTime = 2, scale = new Vector2(5, 5), minVelocity = new Vector2(-10, -10), maxVelocity = new Vector2(10, 10) },
                        new ParticleSystem(textures["Square"], new Vector2(0, 100), "MissileParticles")
                        { color = Fade(YELLOW, 0.2f), lifeTime = 2, scale = new Vector2(5, 5), minVelocity = new Vector2(-10, -10), maxVelocity = new Vector2(10, 10) }
                    }, "Missile") { Scale = 0.3f };
                    NewObject(m);
                    Vector3 direction = new Vector3(transform.m2, -transform.m5, 0);
                    direction.Normalize();
                    m.Translate(direction * 50);
                    m.collider.velocity = direction * 500;
                    curTime = shotTimer;
                }
            }
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

            //end the game
            if (health <= 0) 
            {
                gameOver = true;
            }
            
            //reset game
            if (gameOver && IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON)) 
            {
                foreach (Sprite s in objects) 
                {
                    if (s.objectName == "Asteroid" || s.objectName == "Missile" || s.objectName == "LifePickup") 
                    {
                        Destroy(s);
                    }
                }
                health = 3;
                gameOver = false;
                score = 0;
                Rotation = 180;
                collider.velocity = new Vector3(0, 0, 0);
                transform.m7 = 800;
                transform.m8 = 450;
            }

        }

        public override void UIDraw()
        {
            //draw health
            Rectangle sourceRec = new Rectangle(0,0,textures["Heart"].width, textures["Heart"].height);
            for (int i = 0; i < health; i++) 
            {
                Rectangle destRec = new Rectangle(10 + (30 * (i + 1)), 10, 20, 20);
                DrawTexturePro(textures["Heart"],sourceRec,destRec,new Vector2(0,0),0,Fade(WHITE,0.7f));
            }

            //draw score
            int x = 800 - MeasureText("Score:" + score, 20) / 2;
            DrawText("Score:" + score, x, 10, 20, WHITE);

            //game over screen
            if (gameOver) 
            {
                DrawText("Game Over!", 800 - MeasureText("Game Over!", 100) / 2, 400, 100, WHITE);
                DrawText("Left click to restart...", 800 - MeasureText("Left click to restart...", 30) / 2, 500, 30, WHITE);
            }
        }

    }
}
