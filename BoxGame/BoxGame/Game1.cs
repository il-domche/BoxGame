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
using Indiv0.BoxGame.Classes.Base.GUI.Input;
using Indiv0.BoxGame.Classes.Base.GUI.StateManagement;

namespace Indiv0.BoxGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Fields
        GraphicsDeviceManager graphics;

        SpriteBatch _spriteBatch;
        SpriteBatch _utilsSpriteBatch;

        SpriteFont kootenayFont;
        
        private int _screenHeight;
        private int _screenWidth;

        public const int _DEFAULT_SCREEN_HEIGHT = 576;
        public const int _DEFAULT_SCREEN_WIDTH = 1024;
        
        const long _ticksInASecond = 10000000;
        int _framesSoFar = 0;
        int _framesPerSecond;
        long _elapsedTicks = 0;
        string FramesPerSecond;
        Vector2 FramesPosition;

        bool _displayingUtils = false;

        StateManager _stateManager;
        
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        MouseState currentMouseState;
        MouseState previousMouseState;
        #endregion

        #region Methods

        #region My Methods

        private void ToggleFullscreen()
        {
            graphics.ToggleFullScreen();
        }

        private void ToggleUtilsDisplay()
        {
            _displayingUtils = !_displayingUtils;
        }

        private void ChangeResolution(int width, int height)
        {
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.ApplyChanges();
        }
        #endregion

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

            _stateManager = new StateManager(this.Content);

            IsMouseVisible = true;
            
            FramesPosition = new Vector2();
            FramesPosition.X = 20;
            FramesPosition.Y = 20;
            kootenayFont = Content.Load<SpriteFont>("res/fonts/kootenay");
            
            ChangeResolution(_DEFAULT_SCREEN_WIDTH, _DEFAULT_SCREEN_HEIGHT);
            ToggleFullscreen();
        }
        #endregion

        #region Content Methods
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _utilsSpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }
        #endregion

        #region Update Method
        protected override void Update(GameTime gameTime)
        {
            #region Frames Per Second
            _elapsedTicks += gameTime.ElapsedGameTime.Ticks;
            if (_elapsedTicks < _ticksInASecond)
            {
                _framesSoFar++;
            }
            else
            {
                _elapsedTicks = 0;
                _framesPerSecond = _framesSoFar;
                _framesSoFar = 0;
                FramesPerSecond = Convert.ToString(_framesPerSecond);
            }
            #endregion

            #region Keyboard
            currentKeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed | currentKeyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (currentKeyboardState != previousKeyboardState)
            {
                if (currentKeyboardState.IsKeyDown(Keys.F11))
                {
                    ToggleFullscreen();
                }

                if (currentKeyboardState.IsKeyDown(Keys.F3))
                {
                    ToggleUtilsDisplay();
                }
            }
            #endregion

            _stateManager.CurrentStateIndex = (int)StateManager.StateOptions.GamePlay;
            
            _stateManager.Update(gameTime);

            base.Update(gameTime);

            #region Resets
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
            #endregion
        }
        #endregion

        #region Draw Method
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _stateManager.Draw(gameTime, GraphicsDevice, _spriteBatch);

            if (_displayingUtils == true)
            {
                _utilsSpriteBatch.Begin();
                _utilsSpriteBatch.DrawString(kootenayFont, FramesPerSecond, FramesPosition, Color.White);
                _utilsSpriteBatch.End();
            }

            base.Draw(gameTime);
        }
        #endregion

        #endregion
    }
}
