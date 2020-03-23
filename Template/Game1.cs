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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D Spelare1, skott;
        Vector2 Spelare1pos, skottpos;
        Rectangle Spelare1storlek;

        List<Vector2> Spelare1SkottPos = new List<Vector2>();

        KeyboardState kNewstate, kOldstate;




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
            Spelare1pos = new Vector2(300, 200);
            skottpos = new Vector2(37, 33);
            Spelare1SkottPos = new List<Vector2>();
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
            skott = Content.Load<Texture2D>("skott");




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

                Exit();

            Spelare1storlek = new Rectangle((int)Spelare1pos.X, (int)Spelare1pos.Y, 100, 85);
            kNewstate = Keyboard.GetState();
            KeyboardState a = Keyboard.GetState();

            if (kNewstate.IsKeyDown(Keys.Right))
                Spelare1pos.X += 10;
            if (a.IsKeyDown(Keys.Left))
                Spelare1pos.X -= 10;
            if (a.IsKeyDown(Keys.Up))
                Spelare1pos.Y -= 10;
            if (a.IsKeyDown(Keys.Down))
                Spelare1pos.Y += 10;


            if (a.IsKeyDown(Keys.Space) && kOldstate.IsKeyUp(Keys.Space))
            {
                Spelare1SkottPos.Add(Spelare1pos + skottpos);
            }
            for (int i = 0; i < Spelare1SkottPos.Count; i++)
            {
                Spelare1SkottPos[i] = Spelare1SkottPos[i] - new Vector2(0, 1);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(Spelare1, Spelare1storlek, Color.White);

            foreach (Vector2 bulletPos in Spelare1SkottPos)
            {
                Rectangle rec = new Rectangle();
                rec.Location = bulletPos.ToPoint();
                rec.Size = new Point(20, 20);
                spriteBatch.Draw(skott, rec, Color.White);
            }
            spriteBatch.End();



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        void RemoveObjects()
        {
            List<Vector2> temp = new List<Vector2>();
            foreach (var item in Spelare1SkottPos)
            {
                if (item.Y >= 0)
                {
                    temp.Add(item);
                }
            }
            Spelare1SkottPos = temp;
        }
    }
}
