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

namespace Indiv0.BoxGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables
        GraphicsDeviceManager graphics;

        #region SpriteBatches
        SpriteBatch tempSpriteBatch;
        SpriteBatch terrainSpriteBatch;
        SpriteBatch guiSpriteBatch;
        SpriteBatch utilsSpriteBatch;
        #endregion

        #region Fonts
        SpriteFont kootenayFont;
        #endregion

        #region Keyboard
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        #endregion

        #region Mouse
        MouseState currentMouseState;
        MouseState previousMouseState;
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

        #region Game Values
        const int POINTS_PER_TURN = 10;
        int _boxWidth = 32;
        int _boxHeight = 32;
        Vector2 _hoveringOver;
        private Unit[,] _unitMap;
        #endregion

        #region Utils
        const long _ticksInASecond = 10000000;
        int _framesSoFar = 0;
        int _framesPerSecond;
        long _elapsedTicks = 0;
        string FramesPerSecond;
        Vector2 FramesPosition;

        bool _displayingUtils = false;
        #endregion

        #region GUI
        bool _drawBox = false;
        Vector2 SelectionBoxPosition;

        Button SelectionButton;
        string _selectionButtonTextureString = "res/art/gui/buttons/button_right";
        Vector2 _selectionButtonPosition;
        int _selectionButtonWidth = 18;
        int _selectionButtonHeight = 58;

        bool _displayingSelectionMenu = false;
        Vector2 _selectionMenuPosition;
        int _selectionMenuWidth = 39;
        int _selectionMenuHeight = 98;



        const int GUI_SPACING = 2;
        #endregion

        #region Textures
        Texture2D GrassTexture;
        Texture2D WaterTexture;
        Texture2D MountainTexture;
        Texture2D SelectionBoxTexture;
        Texture2D SelectionMenuTexture;
        #endregion

        #endregion

        #region Methods

        #region My Methods

        private void ChangeResolution(int height, int width)
        {
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.ApplyChanges();
        }

        private void ToggleFullscreen()
        {
            graphics.ToggleFullScreen();
        }

        private void ToggleUtilsDisplay()
        {
            _displayingUtils = !_displayingUtils;
        }

        private void GenerateNewUnitMap()
        {
            for (int i = 0; i < _terrain.ArrayHeight; i++)
            {
                for (int j = 0; j < _terrain.ArrayWidth; j++)
                {
                    _unitMap[j, i] = new Unit(_terrain.TextureMap[j, i].TextureString, 
                        j * _boxWidth, i * _boxHeight, _boxWidth, _boxHeight, 
                        Unit.Countries.None, Unit.BlockTypes.None);
                }
            }
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
            IsMouseVisible = true;

            base.Initialize();

            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;

            currentMouseState = Mouse.GetState();
            previousMouseState = Mouse.GetState();

            FramesPosition = new Vector2();
            FramesPosition.X = 20;
            FramesPosition.Y = 20;

            WaterTexture = Content.Load<Texture2D>("res/art/terrain/water_block");
            GrassTexture = Content.Load<Texture2D>("res/art/terrain/grass_block");
            MountainTexture = Content.Load<Texture2D>("res/art/terrain/mountain_block");
            kootenayFont = Content.Load<SpriteFont>("res/fonts/kootenay");
            
            SelectionBoxTexture = Content.Load<Texture2D>("res/art/gui/selection_box");
            SelectionMenuTexture = Content.Load<Texture2D>("res/art/gui/menus/selection_menu_right");

            _terrain = new Terrain("res/art/terrain/grass_block", _boxWidth, _boxHeight, 
                GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, GrassTexture, WaterTexture, MountainTexture);
            
            _terrain.GenerateNewMap();

            ChangeResolution(_terrain.ArrayHeight * _boxHeight, _terrain.ArrayWidth * _boxWidth);
            ToggleFullscreen();

            _unitMap = new Unit[_terrain.ArrayWidth,_terrain.ArrayHeight];

            GenerateNewUnitMap();

            //Add new stuff here
            _selectionButtonPosition.X = GraphicsDevice.Viewport.Width;
            _selectionButtonPosition.Y = (GraphicsDevice.Viewport.Height / 2);
            SelectionButton = new Button(_selectionButtonTextureString, 
                (int)_selectionButtonPosition.X, (int)_selectionButtonPosition.Y,
                _selectionButtonWidth, _selectionButtonHeight);
            SelectionButton.IsDisplayed = true;
            SelectionButton.Texture = Content.Load<Texture2D>("res/art/gui/buttons/button_right");

            _selectionMenuPosition.X = (SelectionButton.Position.X - (GUI_SPACING + _selectionMenuWidth));
            _selectionMenuPosition.Y = ((SelectionButton.Position.Y + SelectionButton.Height) - (_selectionMenuHeight / 2));
            //But before here
        }
        #endregion

        #region Content Methods
        protected override void LoadContent()
        {
            tempSpriteBatch = new SpriteBatch(GraphicsDevice);
            terrainSpriteBatch = new SpriteBatch(GraphicsDevice);
            guiSpriteBatch = new SpriteBatch(GraphicsDevice);
            utilsSpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }
        #endregion

        #region Update Method
        protected override void Update(GameTime gameTime)
        {
            _drawBox = false;

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

                if (currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    _terrain.GenerateNewMap();
                }
                if (currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    _terrain.Roughness++;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    if (_terrain.Roughness > 0)
                    {
                        _terrain.Roughness--;
                    }
                }
            }
            #endregion

            #region Mouse
            currentMouseState = Mouse.GetState();

            //Check if mouse is colliding with any squares (Which it should be)
            for (int i = 0; i < _terrain.ArrayHeight; i++)
            {
                for (int j = 0; j < _terrain.ArrayWidth; j++)
                {
                    if (currentMouseState.X >= _terrain.TextureMap[j, i].Position.X &&
                        currentMouseState.X <= _terrain.TextureMap[j, i].Position.X + _terrain.TextureMap[j, i].Width)
                    {
                        if (currentMouseState.Y >= _terrain.TextureMap[j, i].Position.Y &&
                        currentMouseState.Y <= _terrain.TextureMap[j, i].Position.Y + _terrain.TextureMap[j, i].Height)
                        {
                            //System.Console.WriteLine("Intersecting: [" + j + ", " + i + "]"); 
                            _drawBox = true;
                            SelectionBoxPosition.X = _terrain.TextureMap[j, i].Position.X;
                            SelectionBoxPosition.Y = _terrain.TextureMap[j, i].Position.Y;
                            _hoveringOver.X = j;
                            _hoveringOver.Y = i;
                        }
                    }
                }
            }

            if (currentMouseState != previousMouseState)
            {
                if (currentMouseState.X >= SelectionButton.Position.X && currentMouseState.Y >= SelectionButton.Position.Y &&
                    currentMouseState.X <= (SelectionButton.Position.X + SelectionButton.Width) &&
                    currentMouseState.Y <= (SelectionButton.Position.Y + SelectionButton.Height))
                {
                    if (currentMouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (_displayingSelectionMenu == false)
                        {
                            _displayingSelectionMenu = true;
                        }
                        else { _displayingSelectionMenu = false; }
                    }
                }
            }
            #endregion

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

            terrainSpriteBatch.Begin();
            _terrain.DrawTerrain(terrainSpriteBatch);
            terrainSpriteBatch.End();

            guiSpriteBatch.Begin();
            if (_drawBox == true)
            {
                guiSpriteBatch.Draw(SelectionBoxTexture, SelectionBoxPosition, Color.White);
            }
            if (SelectionButton.IsDisplayed == true)
            {
                guiSpriteBatch.Draw(SelectionButton.Texture, SelectionButton.Position, Color.White);
            }
            if (_displayingSelectionMenu == true)
            {
                guiSpriteBatch.Draw(SelectionMenuTexture, _selectionMenuPosition, Color.White);
            }
            guiSpriteBatch.End();

            if (_displayingUtils == true)
            {
                utilsSpriteBatch.Begin();
                utilsSpriteBatch.DrawString(kootenayFont, FramesPerSecond, FramesPosition, Color.White);
                utilsSpriteBatch.End();
            }

            base.Draw(gameTime);
        }
        #endregion

        #endregion
    }
}
