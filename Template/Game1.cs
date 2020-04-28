using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Template
{
    public class Game1 : Game
    {
   
        Spelare s1 = new Spelare();
        Spelare1 s2 = new Spelare1();
   
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Random rnd = new Random();

        private int EnemySpawnPos = 0, EnemyTimer = 0, SpawnRate = 60, PowerTimer = 0, Spawnratepower = 1000, PowerPos = 0;


        private Texture2D background, EnemySpawn, Power;

        private Vector2 spelare1pos, Spelare2pos;

        private List<Vector2> RandomEnemySpawn = new List<Vector2>();
        private List<Vector2> RandomPowerSpawn = new List<Vector2>();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            s1.Initialize();
            s2.Initialize();
        
            
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            s1.LoadContent(Content);
            s2.LoadContent(Content);
            EnemySpawn = Content.Load<Texture2D>("EnemySpawn");
            background = Content.Load<Texture2D>("background");
            Power = Content.Load<Texture2D>("Power");
            

   
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState a = Keyboard.GetState();

            // om man trycker på escape så avslutas monogame
            if (a.IsKeyDown(Keys.Escape))
                Exit();
        
            s1.Update(gameTime);
            s2.Update(gameTime);
      


            //Om Spawnrate är över 15 så kommer den minska med 5 var 10 sekund, 10s = enemytimer 600
            if (SpawnRate > 15)
            {
                EnemyTimer++;
                if (EnemyTimer == 120)
                {
                    SpawnRate -= 5;
                    EnemyTimer = 0;
                }
            }
            //Om Spawnrate är 5<x<15 så kommer den minska med 1 var 10 sekund, 10s = enemytimer 600
            if (SpawnRate <= 15 && SpawnRate > 5)
            {
                EnemyTimer++;
                if (EnemyTimer == 120)
                {
                    SpawnRate -= 1;
                    EnemyTimer = 0;
                }
            }
            EnemySpawnPos = rnd.Next(0, 1800);
            if (rnd.Next(0, SpawnRate) == 0)
            {
                RandomEnemySpawn.Add(new Vector2(EnemySpawnPos, 0));
            }

            for (int i = 0; i < RandomEnemySpawn.Count; i++)
            {
                RandomEnemySpawn[i] = RandomEnemySpawn[i] - new Vector2(0, -2);
                Rectangle rec = new Rectangle((int)RandomEnemySpawn[i].X, (int)RandomEnemySpawn[i].Y, 30, 50);
                Rectangle storlekSpelare1 = new Rectangle((int)spelare1pos.X, (int)spelare1pos.Y, 130, 130);
                if (s1.StorlekSpelare1.Intersects(rec))
                {
                    Exit();
                }
            }


            for (int i = 0; i < RandomEnemySpawn.Count; i++)
            {
                RandomEnemySpawn[i] = RandomEnemySpawn[i] - new Vector2(0, -2);
                Rectangle rec = new Rectangle((int)RandomEnemySpawn[i].X, (int)RandomEnemySpawn[i].Y, 30, 50);
                Rectangle storlekSpelare2 = new Rectangle((int)Spelare2pos.X, (int)Spelare2pos.Y, 130, 130);
                if (s2.StorlekSpelare2.Intersects(rec))
                {
                    Exit();
                }
            }

            if(Spawnratepower > 15)
            {
                PowerTimer++;
                if(PowerTimer == 10)
                {
                    Spawnratepower -= 1;
                    PowerTimer = 0;
                }
            }

            if(Spawnratepower <= 1)
            {
                PowerTimer++;
                if(PowerTimer == 10)
                {
                    Spawnratepower -= 1;
                    PowerTimer = 0;
                }
            }
            PowerPos = rnd.Next(0, 1900);
            if (rnd.Next(0, Spawnratepower) == 0)
            {
                RandomPowerSpawn.Add(new Vector2 (rnd.Next(0, 1900), rnd.Next(0, 950)));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
            spriteBatch.Draw(Power, new Rectangle(GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height, 0, 0), Color.White);
            s1.Draw(spriteBatch);
            s2.Draw(spriteBatch);
           
            foreach (Vector2 RandomEnemySpawn in RandomEnemySpawn)
            {
                Rectangle rec = new Rectangle();
                rec.Location = RandomEnemySpawn.ToPoint();
                rec.Size = new Point(80, 80);
                spriteBatch.Draw(EnemySpawn, rec, Color.White);
            }
            foreach (Vector2 RandomPowerSpawn in RandomPowerSpawn)
            {
                Rectangle rec = new Rectangle();
                rec.Location = RandomPowerSpawn.ToPoint();
                rec.Size = new Point(80, 80);
                spriteBatch.Draw(Power, rec, Color.White);
            }


            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}



