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
using Indiv0.BoxGame.Classes.Base;

namespace Indiv0.BoxGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch terrainSpriteBatch;

        #region Keyboard
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        #endregion

        #region Screen
        private int _screenHeight;
        private int _screenWidth;

        private const int _DEFAULT_SCREEN_HEIGHT = 576;
        private const int _DEFAULT_SCREEN_WIDTH = 1024;
        #endregion
        
        #region Terrain
        Terrain _terrain;
        #endregion

        #region Textures
        Texture2D GrassTexture;
        Texture2D WaterTexture;
        Texture2D MountainTexture;
        #endregion

        #endregion

        #region Methods

        private void ChangeResolution(int height, int width)
        {
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.ApplyChanges();
        }

        private void ToggleFullscreen()
        {
            if (!graphics.IsFullScreen)
                graphics.ToggleFullScreen();
        }

        #region Game Method
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            ChangeResolution(_DEFAULT_SCREEN_HEIGHT, _DEFAULT_SCREEN_WIDTH);
        }
        #endregion

        #region Initialize Method
        protected override void Initialize()
        {
            base.Initialize();

            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;

            GrassTexture = Content.Load<Texture2D>("res/art/terrain/grass_block");
            WaterTexture = Content.Load<Texture2D>("res/art/terrain/grass_block");
            MountainTexture = Content.Load<Texture2D>("res/art/terrain/grass_block");

            _terrain = new Terrain("res/art/terrain/grass_block", 32, 32,
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            _terrain.GenerateTextureMap(GrassTexture, WaterTexture, MountainTexture);
        }
        #endregion

        #region Content Methods
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            terrainSpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }
        #endregion

        #region Update Method
        protected override void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (currentKeyboardState != previousKeyboardState)
            {
                if (currentKeyboardState.IsKeyDown(Keys.F11))
                {
                    ToggleFullscreen();
                }
            }

            base.Update(gameTime);

            previousKeyboardState = currentKeyboardState;
        }
        #endregion

        #region Draw Method
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            terrainSpriteBatch.Begin();
            _terrain.DrawTerrain(terrainSpriteBatch);
            terrainSpriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #endregion
    }
}
