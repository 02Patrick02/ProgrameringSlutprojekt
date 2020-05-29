using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Template
{
    public class Game1 : Game
    {
        //refererar till min spelareklass
        Spelare1 S;

        //graphics ändrar hur stor skärmen ska vara
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        //Random
        private Random rnd = new Random();

        // Int är olika värden på heltal
        private int EnemySpawnPos = 0, EnemyTimer = 0, SpawnRate = 60, PowerTimer = 0, Spawnratepower = 1000, PowerPos = 0;

        //sprites som ska ritas ut
        private Texture2D background, EnemySpawn, Power;

        //positionen för spelaren
        private Vector2 SpelarePos;

        // listor
        private List<Vector2> RandomEnemySpawn = new List<Vector2>();
        private List<Vector2> RandomPowerSpawn = new List<Vector2>();
        private List<Vector2> SpelareSkottPos = new List<Vector2>();

        //konstruktor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            S = new Spelare1(SpelareSkottPos);
        }

        protected override void Initialize()
        {
            S.Initialize();
            
            //Ändrar storleken på skärmen
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();

            base.Initialize();
        }

        //här skriver man allt som ska laddas in på skärmen"alla sprites"
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            S.LoadContent(Content);

            EnemySpawn = Content.Load<Texture2D>("EnemySpawn");
            background = Content.Load<Texture2D>("background");
            Power = Content.Load<Texture2D>("Power");
             
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState a = Keyboard.GetState(); //istället för att skriva keyboardstate så kan man skriva a med samma funktion 

            // om man trycker på escape så avslutas monogame
            if (a.IsKeyDown(Keys.Escape))
                Exit();
        
            //S. refererar till min klass som heter Spelare1
            S.Update(gameTime);
      


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

            //vart enemy ska spawna random mellan 0, 1800
            EnemySpawnPos = rnd.Next(0, 1800);
            if (rnd.Next(0, SpawnRate) == 0)
            {
                RandomEnemySpawn.Add(new Vector2(EnemySpawnPos, 0));
            }

           
            //collision mellan enemy och spelaren då spelet stängs av om spelaren kolliderar
            for (int i = 0; i < RandomEnemySpawn.Count; i++)
            {
                RandomEnemySpawn[i] = RandomEnemySpawn[i] - new Vector2(0, -2);
                Rectangle rec = new Rectangle((int)RandomEnemySpawn[i].X, (int)RandomEnemySpawn[i].Y, 30, 50);
                Rectangle StorlekSpelare = new Rectangle((int)SpelarePos.X, (int)SpelarePos.Y, 130, 130);
                if (S.Storlekspelare.Intersects(rec))
                {
                    RandomEnemySpawn.Remove(RandomEnemySpawn[i]);
                }
            }
            // Spawn raten på powerupsen som dyker upp random på skärmen och de sapwnar fler ju längre man spelar
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

            //kollision mellan skotten och enemy då båda försvinner från skärmen vid kollision
            for (int i = 0; i < RandomEnemySpawn.Count; i++)
            {
                RandomEnemySpawn[i] = RandomEnemySpawn[i] - new Vector2(0, -2);
                Rectangle rec = new Rectangle((int)RandomEnemySpawn[i].X, (int)RandomEnemySpawn[i].Y, 30, 50);
                for(int j = 0; j < SpelareSkottPos.Count; j++)
                {
                    Rectangle Storlekskott = new Rectangle((int)SpelareSkottPos[j].X, (int)SpelareSkottPos[j].Y, 100, 100);

                    if (Storlekskott.Intersects(rec))
                    {
                        SpelareSkottPos.Remove(SpelareSkottPos[j]);
                        RandomEnemySpawn.Remove(RandomEnemySpawn[Math.Max(i,0)]);
                        i--;
                    }
                }
            }

            //kollision mellan spelare och powerupsen, då spelaren kolliderar så får han + 2 speed permanent
            for (int i = 0; i < RandomPowerSpawn.Count; i++)
            {
                Rectangle rec = new Rectangle((int)RandomPowerSpawn[i].X, (int)RandomPowerSpawn[i].Y, 10, 50);
                Rectangle StorlekSpelare = new Rectangle((int)SpelarePos.X, (int)SpelarePos.Y, 130, 130);
                if (S.Storlekspelare.Intersects(rec))
                {
                    RandomPowerSpawn.Remove(RandomPowerSpawn[i]);

                    S.Speed += 2; // för varje gång spelaren tar upp poweruppen så ökar hastigheten permanent för spelaren.
                }
            }

            base.Update(gameTime);
        }

        //ritar ut sprites
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
            spriteBatch.Draw(Power, new Rectangle(GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height, 0, 0), Color.White);
            S.Draw(spriteBatch);
           
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



