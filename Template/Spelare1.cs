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
    class Spelare1 // En class som heter Spelare1
    {
        //texture2D är alla sprite bilder som kommer vara synliga på skärmen
        private Texture2D Spelare, Skott;

        //Vector2 är positionen för sprites
        private Vector2 SpelarePos, SkottPos;

        //Rectangle är storleken på sprites
        private Rectangle StorlekSpelare, StorlekSkott;

        // en int variabel som ändrar på hastigheten
        private int speed;

        // En Get och Set metod för storlekspelare
        public Rectangle Storlekspelare 
        {
            get { return StorlekSpelare; }
            set { StorlekSpelare = value; }
        }

        // En Get och Set metod för storlekskott
        public Rectangle Storlekskott
        {
            get { return StorlekSkott; }
            set { StorlekSkott = value; }
        }

        // En Get och Set metod för speed
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        // En Lista flr Skotten till spelaren
        private List<Vector2> SpelareSkottPos = new List<Vector2>();

        // tillåter att kNewstate och k0ldstate får användas
        private KeyboardState kNewstate, kOldstate;

        //Konstruktor
        public Spelare1(List<Vector2>skottLista)
        {
            SpelareSkottPos = skottLista;
        }

        //I initialize så skriver man in det som ska ske när spelet börjar
        public void Initialize()
        {
            SpelarePos = new Vector2(550, 480);
            SkottPos = new Vector2(550, 480);
            Speed = 10;
        }

        //load content
        public void LoadContent(ContentManager Content)
        {
            Spelare = Content.Load<Texture2D>("Spelare");
            Skott = Content.Load<Texture2D>("Skott");
        }

        //Update
        public void Update(GameTime gameTime)
        {
         
            KeyboardState a = Keyboard.GetState();

            //storlek på Spelare
            StorlekSpelare = new Rectangle((int)SpelarePos.X, (int)SpelarePos.Y, 150, 150);
            //Skottens storlek
            StorlekSkott = new Rectangle((int)SkottPos.X, (int)SkottPos.Y, 150, 150);
       

            // rörelse för spelare
            if (a.IsKeyDown(Keys.D))
                SpelarePos.X += speed;
            if (a.IsKeyDown(Keys.A))
                SpelarePos.X -= speed;
            if (a.IsKeyDown(Keys.W))
                SpelarePos.Y -= speed;
            if (a.IsKeyDown(Keys.S))
                SpelarePos.Y += speed;

            //Skott för spelare
            if (a.IsKeyDown(Keys.Space) && kOldstate.IsKeyUp(Keys.Space))
            {
                SpelareSkottPos.Add(SpelarePos + new Vector2(89, 0));
            }
            for (int i = 0; i < SpelareSkottPos.Count; i++)
            {
                SpelareSkottPos[i] = SpelareSkottPos[i] - new Vector2(0, 5);
            }


            //Gör så att spelaren inte kan komma utanför skärmen
            if (SpelarePos.X <= 0)
                SpelarePos.X = 0;
            if (SpelarePos.X >= 1800)
                SpelarePos.X = 1800;
            if (SpelarePos.Y <= 0)
                SpelarePos.Y = 0;
            if (SpelarePos.Y >= 930)
                SpelarePos.Y = 930;
        }
 

        //Draw metoden som ritar ut sprites
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Spelare, StorlekSpelare, Color.White);

            //ändrar på Skotten från spelaren
            foreach (Vector2 Spelareskottpos in SpelareSkottPos)
            {
                Rectangle rec = new Rectangle();
                rec.Location = Spelareskottpos.ToPoint();
                rec.Size = new Point(40, 40);
                spriteBatch.Draw(Skott, rec, Color.White);
            }
        }
    }
}