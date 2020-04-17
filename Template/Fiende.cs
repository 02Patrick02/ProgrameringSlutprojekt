using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Template
{
    class Fiende
    {
        Spelare s1 = new Spelare();
        Spelare1 s2 = new Spelare1();

        private Texture2D EnemySpawn;
        private Vector2 spelare1pos, Spelare2pos;

        private Random rnd = new Random();
        private List<Vector2> RandomEnemySpawn = new List<Vector2>();

        private int EnemySpawnPos = 0, EnemyTimer = 0, SpawnRate = 60;

        public Fiende()//Konstruktor
        {

        }

        public void Initialize()
        {
        }

        public void LoadContent(ContentManager Content)//load content
        {
            EnemySpawn = Content.Load<Texture2D>("EnemySpawn");
        }

        public void Update(GameTime gameTime)//Update
        {
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
                    Game1.Quit();
                }
            }

            for (int i = 0; i < RandomEnemySpawn.Count; i++)
            {
                RandomEnemySpawn[i] = RandomEnemySpawn[i] - new Vector2(0, -2);
                Rectangle rec = new Rectangle((int)RandomEnemySpawn[i].X, (int)RandomEnemySpawn[i].Y, 30, 50);
                Rectangle storlekSpelare2 = new Rectangle((int)Spelare2pos.X, (int)Spelare2pos.Y, 130, 130);
                if (s2.StorlekSpelare2.Intersects(rec))
                {
                    Game1.Quit();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)//Draw
        {
            foreach (Vector2 RandomEnemySpawn in RandomEnemySpawn)
            {
                Rectangle rec = new Rectangle();
                rec.Location = RandomEnemySpawn.ToPoint();
                rec.Size = new Point(80, 80);
                spriteBatch.Draw(EnemySpawn, rec, Color.White);

            }

        }
    }
}