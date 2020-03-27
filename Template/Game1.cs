using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Template
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D Spelare1, skott1, skott2, Spelare2, background, EnemySpawn;
        private Vector2 spelare1pos, Spelare2pos;
        private Rectangle storlekSpelare1, storlekspelare2;

        private Random rnd = new Random();





        private List<Vector2> Spelare1skottpos = new List<Vector2>();
        private List<Vector2> Spelare2skottpos = new List<Vector2>();
        private List<Vector2> RandomEnemySpawn = new List<Vector2>();


        int screenwidth, screenheight, EnemySpawnPos = 0, EnemyTimer = 0, SpawnRate = 60;
        private KeyboardState kNewstate, kOldstate;






        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spelare1pos = new Vector2(1200, 500);
            Spelare2pos = new Vector2(550, 480);



            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();







            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Spelare1 = Content.Load<Texture2D>("Spelare1");
            skott1 = Content.Load<Texture2D>("skott1");
            skott2 = Content.Load<Texture2D>("skott2");
            Spelare2 = Content.Load<Texture2D>("Spelare2");
            background = Content.Load<Texture2D>("background");
            EnemySpawn = Content.Load<Texture2D>("EnemySpawn");


            screenwidth = GraphicsDevice.Viewport.Width;
            screenheight = GraphicsDevice.Viewport.Height;




            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (kNewstate.IsKeyDown(Keys.Escape))
                Exit();//om man trycker på escape så avslutas gamet



            storlekspelare2 = new Rectangle((int)Spelare2pos.X, (int)Spelare2pos.Y, 150, 150);//storlek på fienden
            storlekSpelare1 = new Rectangle((int)spelare1pos.X, (int)spelare1pos.Y, 130, 130);//storleken på spritebatchen Spelare1 


            kNewstate = Keyboard.GetState();
            KeyboardState a = Keyboard.GetState();

            if (a.IsKeyDown(Keys.Right))//Rör på Spelare1 sprite höger, vänster, upp, ned
                spelare1pos.X += 10;
            if (a.IsKeyDown(Keys.Left))
                spelare1pos.X -= 10;
            if (a.IsKeyDown(Keys.Up))
                spelare1pos.Y -= 10;
            if (a.IsKeyDown(Keys.Down))
                spelare1pos.Y += 10;



            if (a.IsKeyDown(Keys.Space) && kOldstate.IsKeyUp(Keys.Space))//skott för Spelare1 spriten
            {
                Spelare1skottpos.Add(spelare1pos + new Vector2(10, 0));
            }
            for (int i = 0; i < Spelare1skottpos.Count; i++)
            {
                Spelare1skottpos[i] = Spelare1skottpos[i] - new Vector2(0, 5);
            }



            if (a.IsKeyDown(Keys.D))// rörelse för fienden
                Spelare2pos.X += 10;
            if (a.IsKeyDown(Keys.A))
                Spelare2pos.X -= 10;
            if (a.IsKeyDown(Keys.W))
                Spelare2pos.Y -= 10;
            if (a.IsKeyDown(Keys.S))
                Spelare2pos.Y += 10;




            if (a.IsKeyDown(Keys.Z) && kOldstate.IsKeyUp(Keys.Z))//skott för fienden
            {
                Spelare2skottpos.Add(Spelare2pos + new Vector2(89, 0));
            }
            for (int i = 0; i < Spelare2skottpos.Count; i++)
            {
                Spelare2skottpos[i] = Spelare2skottpos[i] - new Vector2(0, 5);
            }





            if (spelare1pos.X <= 0)//gör så att Spelare1 spriten inte kan komma utanför skärmen i X-led
                spelare1pos.X = 0;
            if (spelare1pos.X + storlekSpelare1.Width >= screenwidth)
                spelare1pos.X = screenwidth - storlekSpelare1.Width;
            if (spelare1pos.Y <= 0) //i Y-led så att Spelare1 spriten inte kommer utanför skärmen
                spelare1pos.Y = 0;
            if (spelare1pos.Y + storlekSpelare1.Height >= screenheight)
                spelare1pos.Y = screenheight - storlekSpelare1.Height;
            if (Spelare2pos.X <= 0)//göt så att Spelare2 spriten inte kan komma utanför skärmen i X-led
                Spelare2pos.X = 0;
            if (Spelare2pos.X + storlekspelare2.Width >= screenwidth)
                Spelare2pos.X = screenwidth - storlekspelare2.Width;
            if (Spelare2pos.Y <= 0) //i Y-led så att Spelare2 spriten inte kommer utanför skärmen
                Spelare2pos.Y = 0;
            if (Spelare2pos.Y + storlekspelare2.Height >= screenheight)
                Spelare2pos.Y = screenheight - storlekspelare2.Height;



            if (SpawnRate > 15)//Om Spawnrate är över 15 så kommer den minska med 5 var 10 sekund, 10s = enemytimer 600
            {
                EnemyTimer++;
                if (EnemyTimer == 120)
                {
                    SpawnRate -= 5;
                    EnemyTimer = 0;
                }
            }
            if (SpawnRate <= 15 && SpawnRate > 5)//Om Spawnrate är 5<x<15 så kommer den minska med 1 var 10 sekund, 10s = enemytimer 600
            {
                EnemyTimer++;
                if (EnemyTimer == 120)
                {
                    SpawnRate -= 1;
                    EnemyTimer = 0;
                }
            }
            EnemySpawnPos = rnd.Next(0, screenwidth);
            if (rnd.Next(0, SpawnRate) == 0)
            {
                RandomEnemySpawn.Add(new Vector2(EnemySpawnPos, 0));
            }
            for (int i = 0; i < RandomEnemySpawn.Count; i++)
            {
                RandomEnemySpawn[i] = RandomEnemySpawn[i] - new Vector2(0, -2);
            }

            RemoveObjects();

            kOldstate = kNewstate;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
            spriteBatch.Draw(Spelare1, storlekSpelare1, Color.White);
            spriteBatch.Draw(Spelare2, storlekspelare2, Color.White);
            spriteBatch.Draw(EnemySpawn, new Rectangle(0, 0, 0, 0), Color.White);




            foreach (Vector2 Spelare1skottpos in Spelare1skottpos) //skott för spelare1
            {
                Rectangle rec = new Rectangle();
                rec.Location = Spelare1skottpos.ToPoint();
                rec.Size = new Point(30, 30);
                spriteBatch.Draw(skott1, rec, Color.White);
            }

            foreach (Vector2 Spelare2skottpos in Spelare2skottpos)//skott för spelare2
            {
                Rectangle rec = new Rectangle();
                rec.Location = Spelare2skottpos.ToPoint();
                rec.Size = new Point(40, 40);
                spriteBatch.Draw(skott2, rec, Color.White);
            }


            foreach (Vector2 RandomEnemySpawn in RandomEnemySpawn)
            {
                Rectangle rec = new Rectangle();
                rec.Location = RandomEnemySpawn.ToPoint();
                rec.Size = new Point(80, 80);
                spriteBatch.Draw(EnemySpawn, rec, Color.White);
            }




            spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        void RemoveObjects()
        {
            List<Vector2> temp = new List<Vector2>();
            foreach (var item in Spelare1skottpos)
            {
                if (item.Y >= 0)
                {
                    temp.Add(item);
                }
            }

            Spelare1skottpos = temp;

            List<Vector2> temp2 = new List<Vector2>();
            foreach (var item in Spelare2skottpos)
            {
                if (item.Y >= 0)
                {
                    temp2.Add(item);
                }
            }

            Spelare2skottpos = temp2;

            List<Vector2> temp3 = new List<Vector2>();
            foreach (var item in RandomEnemySpawn)
            {
                if (item.Y <= screenheight)
                {
                    temp3.Add(item);
                }
            }

            RandomEnemySpawn = temp3;
        }
    }
}



