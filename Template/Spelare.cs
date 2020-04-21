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
    class Spelare
    {
        private Texture2D Spelare1, skott1;
        
        private Vector2 spelare1pos;
        
        private Rectangle storlekSpelare1;

        public Rectangle StorlekSpelare1
        {
            get { return storlekSpelare1; }
            set { storlekSpelare1 = value; }
        }
        

        private static List<Vector2> Spelare1skottpos = new List<Vector2>();
   
        public List<Vector2> Spelare1skottPos
        {
            get { return Spelare1skottpos; }
            set { Spelare1skottpos = value; }
        }

        private KeyboardState kNewstate, kOldstate;

        public Spelare()//Konstruktor
        {

        }

        public void Initialize()
        {
            spelare1pos = new Vector2(1200, 500);
        }

        public void LoadContent(ContentManager Content)//load content
        {
            Spelare1 = Content.Load<Texture2D>("Spelare1");
            skott1 = Content.Load<Texture2D>("skott1");
        }
        
        public void Update(GameTime gameTime)//Update
        {
            kNewstate = Keyboard.GetState();

            KeyboardState a = Keyboard.GetState();

            //storleken på spritebatchen Spelare1
            storlekSpelare1 = new Rectangle((int)spelare1pos.X, (int)spelare1pos.Y, 130, 130);

            //Rör på Spelare1 sprite höger, vänster, upp, ned
            if (a.IsKeyDown(Keys.Right))
                spelare1pos.X += 10;

            if (a.IsKeyDown(Keys.Left))
                spelare1pos.X -= 10;

            if (a.IsKeyDown(Keys.Up))
                spelare1pos.Y -= 10;

            if (a.IsKeyDown(Keys.Down))
                spelare1pos.Y += 10;


            //gör så att spelaren inte kan åka utanför skärmen
            if (spelare1pos.X <= 0)
                spelare1pos.X = 0;

            if (spelare1pos.X >= 1800)
                spelare1pos.X = 1800;

            if (spelare1pos.Y <= 0)
                spelare1pos.Y = 0;
           
            if (spelare1pos.Y >= 930)
                spelare1pos.Y = 930;


            //skott för Spelare1 spriten
            if (a.IsKeyDown(Keys.Space) && kOldstate.IsKeyDown(Keys.Space))
            {
                Spelare1skottpos.Add(spelare1pos + new Vector2(10, 0));
            }
            for (int i = 0; i < Spelare1skottpos.Count; i++)
            {
                Spelare1skottpos[i] = Spelare1skottpos[i] - new Vector2(0, 5);
            }
        }

        public void Draw(SpriteBatch spriteBatch)//Draw
        {
            spriteBatch.Draw(Spelare1, storlekSpelare1, Color.White);

            //skott för spelare1
            foreach (Vector2 Spelare1skottpos in Spelare1skottpos) 
            {
                Rectangle rec = new Rectangle();
                rec.Location = Spelare1skottpos.ToPoint();
                rec.Size = new Point(30, 30);
                spriteBatch.Draw(skott1, rec, Color.White);
            }
        }
    }
}