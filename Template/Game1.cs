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
        Fiende F = new Fiende();
       
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private static bool quit;
        private Texture2D background;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            s1.Initialize();
            s2.Initialize();
            F.Initialize();
            
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
            F.LoadContent(Content);
            background = Content.Load<Texture2D>("background");
   
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (quit == true)
            {
                Exit();
            }
            
            KeyboardState a = Keyboard.GetState();

            // om man trycker på escape så avslutas gamet
            if (a.IsKeyDown(Keys.Escape))
                Exit();
        
            s1.Update(gameTime);
            s2.Update(gameTime);
            F.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
            s1.Draw(spriteBatch);
            s2.Draw(spriteBatch);
            F.Draw(spriteBatch);
           
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void Quit()
        {
            quit = true;
        }
    }
}



