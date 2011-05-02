using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Indiv0.BoxGame.Classes.Base.GUI.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Indiv0.BoxGame.Classes.Base.GUI.Input;

namespace Indiv0.BoxGame.Classes.Base.GUI.States
{    
    class GamePlay : State
    {
        #region Fields
        Texture2D GrassTexture;
        Texture2D WaterTexture;
        Texture2D MountainTexture;
        Texture2D SelectionBoxTexture;
        Texture2D SelectionMenuTexture;

        const int POINTS_PER_TURN = 10;
        int _boxWidth = 32;
        int _boxHeight = 32;
        Vector2 _hoveringOver;
        private Unit[,] _unitMap;

        Terrain _terrain;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        
        MouseState currentMouseState;
        MouseState previousMouseState;

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

        #region Public Methods
        public GamePlay(ContentManager contentManager)
        {
            //cManager.RootDirectory = "Content";            
            //Console.WriteLine("Content Manager root directory: "+cManager.RootDirectory.ToString());                        
            //paddle = cManager.Load<Texture2D>(cManager.RootDirectory+"/GameAssets/breakout_paddle");
            //entityList = new List<Entities.BaseEntity>();

            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;

            currentMouseState = Mouse.GetState();
            previousMouseState = Mouse.GetState();

            WaterTexture = contentManager.Load<Texture2D>("res/art/terrain/water_block");
            GrassTexture = contentManager.Load<Texture2D>("res/art/terrain/grass_block");
            MountainTexture = contentManager.Load<Texture2D>("res/art/terrain/mountain_block");

            _terrain = new Terrain("res/art/terrain/grass_block", _boxWidth, _boxHeight,
                Game1._DEFAULT_SCREEN_WIDTH, Game1._DEFAULT_SCREEN_HEIGHT, GrassTexture, WaterTexture, MountainTexture);
            _terrain.GenerateNewMap();

            _unitMap = new Unit[_terrain.ArrayWidth, _terrain.ArrayHeight];
            GenerateNewUnitMap();

            LoadContent(contentManager);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            SelectionBoxTexture = contentManager.Load<Texture2D>("res/art/gui/selection_box");
            SelectionMenuTexture = contentManager.Load<Texture2D>("res/art/gui/menus/selection_menu_right");

            _selectionButtonPosition.X = (Game1._DEFAULT_SCREEN_WIDTH - _selectionButtonWidth);
            _selectionButtonPosition.Y = (Game1._DEFAULT_SCREEN_HEIGHT / 2);
            SelectionButton = new Button(_selectionButtonTextureString,
                (int)_selectionButtonPosition.X, (int)_selectionButtonPosition.Y,
                _selectionButtonWidth, _selectionButtonHeight,
                contentManager.Load<Texture2D>("res/art/gui/buttons/button_right"),
                contentManager.Load<Texture2D>("res/art/gui/buttons/button_right_hover"),
                contentManager.Load<Texture2D>("res/art/gui/buttons/button_right_clicked"));
            SelectionButton.IsDisplayed = true;

            _selectionMenuPosition.X = (SelectionButton.Position.X - (GUI_SPACING + _selectionMenuWidth));
            _selectionMenuPosition.Y = ((SelectionButton.Position.Y + (SelectionButton.Height / 2)) - (_selectionMenuHeight / 2));
        }

        public override void Update(GameTime gameTime)
        {
            _drawBox = false;

            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(currentMouseState.X, currentMouseState.Y, 1, 1); 

            if (currentKeyboardState != previousKeyboardState)
            {
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

            //Check if mouse is colliding with any squares (Which it should be)
            for (int i = 0; i < _terrain.ArrayHeight; i++)
            {
                for (int j = 0; j < _terrain.ArrayWidth; j++)
                {
                    Rectangle terrainRectangle = new Rectangle((int)_terrain.TextureMap[j, i].Position.X,
                        (int)_terrain.TextureMap[j, i].Position.Y,
                        _terrain.TextureMap[j, i].Width,
                        _terrain.TextureMap[j, i].Height);

                    if (mouseRectangle.Intersects(terrainRectangle))
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
            if (mouseRectangle.Intersects(SelectionButton.ButtonRectangle))
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    SelectionButton.ButtonState = Button.ButtonStates.Pressed;
                }
                else
                {
                    //Sets the button state to Released if the current state is Pressed,
                    //and sets it to Hover if the current state is not pressed.
                    SelectionButton.ButtonState = SelectionButton.ButtonState ==
                        Button.ButtonStates.Pressed ? Button.ButtonStates.Released : Button.ButtonStates.Hover;

                    if (SelectionButton.ButtonState == Button.ButtonStates.Released)
                    {
                        _displayingSelectionMenu = !_displayingSelectionMenu;
                    }
                }

                //if (currentMouseState.LeftButton == ButtonState.Pressed)
                //{
                //    if (previousMouseState.LeftButton == ButtonState.Released)
                //    {
                //        _displayingSelectionMenu = !_displayingSelectionMenu;
                //    }
                //}
            }

            SelectionButton.Update(gameTime);
            base.Update(gameTime);

            previousMouseState = currentMouseState;
            previousKeyboardState = currentKeyboardState;
        }

        public override void Draw(GameTime gameTime,
            GraphicsDevice graphicsDevice,
            SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            _terrain.DrawTerrain(spriteBatch);

            spriteBatch.Draw(SelectionButton.Texture, SelectionButton.Position, Color.White);

            if (_drawBox == true)
            {
                spriteBatch.Draw(SelectionBoxTexture, SelectionBoxPosition, Color.White);
            }
            if (_displayingSelectionMenu == true)
            {
                spriteBatch.Draw(SelectionMenuTexture, _selectionMenuPosition, Color.White);
            }
            spriteBatch.End();
        }
        #endregion

        #region Private Methods
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
    }
}