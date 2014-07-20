using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace miniLD_53
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameState
        {
            mainMenu,
            options,
            Playing,
        }
        GameState currentGameState = GameState.mainMenu;

        int screenWidth = 800, screenHeight = 600;

        Button button;
        Map map;
        Player player1;

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
            map = new Map();
            player1 = new Player( new Vector2(128, 32));

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

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            button = new Button(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            button.setPosition(new Vector2(350, 300));

            Tiles.Content = Content;

            map.Generate(new int[,]{
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,3,3,3,3,3,3},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,2,2,2,2},
                {0,0,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,2,2,2,2},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,2,1,2,2,2,2},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,2,1,2,2,2,2},
                {0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,1,2,2,2,2},
                {3,3,3,3,2,0,0,3,3,3,3,0,0,0,0,0,0,0,0,0,2,1,2,2,2,2},
                {2,2,1,2,2,0,0,2,2,2,1,0,0,0,0,0,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,0,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,0,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,0,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,2,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,2,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,2,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,2,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,0,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,0,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,0,0,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
                {2,2,1,2,2,1,2,2,2,2,1,2,2,2,2,1,2,2,2,2,2,1,2,2,2,2},
            }, 32);
            player1.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            MouseState mouse = Mouse.GetState();

            switch (currentGameState)
            {
                case GameState.mainMenu:
                    if (button.isClicked == true) currentGameState = GameState.Playing;
                    button.Update(gameTime);
                    break;

                case GameState.Playing:
                    player1.Update(gameTime, 1);
                    break;
            }

            foreach (CollisionTiles tile in map.CollisionTiles)
            {
                player1.Collision(tile.Rectangle, map.Width, map.Height);
            }
          
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
            switch(currentGameState)
            {
                case GameState.mainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    button.Draw(spriteBatch);
                    break;

                case GameState.Playing:
            map.Draw(spriteBatch);
            player1.draw(spriteBatch);
            break;
        }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
