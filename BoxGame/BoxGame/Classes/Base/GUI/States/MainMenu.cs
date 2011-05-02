using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Indiv0.BoxGame.Classes.Base.GUI.StateManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Indiv0.BoxGame.Classes.Base.GUI.States
{
    class MainMenu : State
    {
        #region Fields
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        MouseState currentMouseState;
        MouseState previousMouseState;
        #endregion

        #region Public Methods
        public MainMenu(ContentManager contentManager)
        {
            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = currentKeyboardState;

            currentMouseState = Mouse.GetState();
            previousMouseState = Mouse.GetState();

            //WaterTexture = contentManager.Load<Texture2D>("res/art/terrain/water_block");
            //GrassTexture = contentManager.Load<Texture2D>("res/art/terrain/grass_block");
            //MountainTexture = contentManager.Load<Texture2D>("res/art/terrain/mountain_block");

            LoadContent(contentManager);
        }
    
        public override void LoadContent(ContentManager contentManager)
        {
            //SelectionBoxTexture = contentManager.Load<Texture2D>("res/art/gui/selection_box");
            //SelectionMenuTexture = contentManager.Load<Texture2D>("res/art/gui/menus/selection_menu_right");

            //_selectionButtonPosition.X = (Game1._DEFAULT_SCREEN_WIDTH - _selectionButtonWidth);
            //_selectionButtonPosition.Y = (Game1._DEFAULT_SCREEN_HEIGHT / 2);
            //SelectionButton = new Button(_selectionButtonTextureString,
            //    (int)_selectionButtonPosition.X, (int)_selectionButtonPosition.Y,
            //    _selectionButtonWidth, _selectionButtonHeight);
            //SelectionButton.IsDisplayed = true;
            //SelectionButton.Texture = contentManager.Load<Texture2D>("res/art/gui/buttons/button_right");

            //_selectionMenuPosition.X = (SelectionButton.Position.X - (GUI_SPACING + _selectionMenuWidth));
            //_selectionMenuPosition.Y = ((SelectionButton.Position.Y + (SelectionButton.Height / 2)) - (_selectionMenuHeight / 2));
        }

        public override void Update(GameTime gameTime)
        {
            //if (currentKeyboardState != previousKeyboardState)
            //{
            //    if (currentKeyboardState.IsKeyDown(Keys.Enter))
            //    {
            //        _terrain.GenerateNewMap();
            //    }
            //    if (currentKeyboardState.IsKeyDown(Keys.Up))
            //    {
            //        _terrain.Roughness++;
            //    }
            //    if (currentKeyboardState.IsKeyDown(Keys.Down))
            //    {
            //        if (_terrain.Roughness > 0)
            //        {
            //            _terrain.Roughness--;
            //        }
            //    }
            //}

            //if (currentMouseState.X >= SelectionButton.Position.X && currentMouseState.Y >= SelectionButton.Position.Y &&
            //    currentMouseState.X <= (SelectionButton.Position.X + SelectionButton.Width) &&
            //    currentMouseState.Y <= (SelectionButton.Position.Y + SelectionButton.Height))
            //{
            //    if (currentMouseState.LeftButton == ButtonState.Pressed)
            //    {
            //        if (previousMouseState.LeftButton == ButtonState.Released)
            //        {
            //            _displayingSelectionMenu = !_displayingSelectionMenu;
            //        }
            //    }
            //}
        }
  
        public override void Draw(GameTime gameTime, 
            GraphicsDevice graphicsDevice, 
            SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();
            spriteBatch.End();
        }
        #endregion
    }
}
