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
    class Spelare1
    {
        private Texture2D Spelare2, skott2;
        private static Vector2 Spelare2pos;
        private Rectangle storlekSpelare2;

        public Rectangle StorlekSpelare2
        {
            get { return storlekSpelare2; }
            set { storlekSpelare2 = value; }
        }

        private List<Vector2> Spelare2skottpos = new List<Vector2>();

        private KeyboardState kNewstate, kOldstate;

        public Spelare1()//Konstruktor
        {

        }
        public void Initialize()
        {
            Spelare2pos = new Vector2(550, 480);
        }

        public void LoadContent(ContentManager Content)//load content
        {
            Spelare2 = Content.Load<Texture2D>("Spelare2");
            skott2 = Content.Load<Texture2D>("skott2");
        }

        public void Update(GameTime gameTime)//Update
        {
            kNewstate = Keyboard.GetState();

            KeyboardState a = Keyboard.GetState();

            //storlek på fienden
            storlekSpelare2 = new Rectangle((int)Spelare2pos.X, (int)Spelare2pos.Y, 150, 150);

            // rörelse för fienden
            if (a.IsKeyDown(Keys.D))
                Spelare2pos.X += 10;
            if (a.IsKeyDown(Keys.A))
                Spelare2pos.X -= 10;
            if (a.IsKeyDown(Keys.W))
                Spelare2pos.Y -= 10;
            if (a.IsKeyDown(Keys.S))
                Spelare2pos.Y += 10;

            //skott för fienden
            if (a.IsKeyDown(Keys.Z) && kOldstate.IsKeyUp(Keys.Z))
            {
                Spelare2skottpos.Add(Spelare2pos + new Vector2(89, 0));
            }
            for (int i = 0; i < Spelare2skottpos.Count; i++)
            {
                Spelare2skottpos[i] = Spelare2skottpos[i] - new Vector2(0, 5);
            }


            //Gör så att spelaren inte kan komma utanför skärmen
            if (Spelare2pos.X <= 0)
                Spelare2pos.X = 0;
            if (Spelare2pos.X >= 1800)
                Spelare2pos.X = 1800;
            if (Spelare2pos.Y <= 0)
                Spelare2pos.Y = 0;
            if (Spelare2pos.Y >= 930)
                Spelare2pos.Y = 930;
        }
        public void Draw(SpriteBatch spriteBatch)//Draw
        {
            spriteBatch.Draw(Spelare2, storlekSpelare2, Color.White);

            //skott för spelare2
            foreach (Vector2 Spelareskottpos in Spelare2skottpos)
            {
                Rectangle rec = new Rectangle();
                rec.Location = Spelareskottpos.ToPoint();
                rec.Size = new Point(40, 40);
                spriteBatch.Draw(skott2, rec, Color.White);
            }
        }
    }
}