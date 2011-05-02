using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Indiv0.BoxGame.Classes.Base.GUI.Input
{
    class Button : Sprite
    {
        #region Fields
        Rectangle _buttonRectangle;
        Texture2D _hoverTexture;
        Texture2D _clickedTexture;
        Texture2D _defaultTexture;
        ButtonStates _buttonState;
        
        bool _isDisplayed = true;

        public enum ButtonStates
        {
            None,
            Hover,
            Pressed,
            Released
        }

        public bool IsDisplayed
        {
            get { return _isDisplayed; }
            set { _isDisplayed = value; }
        }

        public Rectangle ButtonRectangle
        {
            get { return _buttonRectangle; }
            set { _buttonRectangle = value; }
        }

        public ButtonStates ButtonState
        {
            get { return _buttonState; }
            set { _buttonState = value; }
        }
        #endregion

        public Button(string texString, int x, int y, int texWidth, int texHeight, 
            Texture2D defaultTexture, Texture2D hoverTexture, Texture2D clickedTexture)
            : base(texString, x, y, texWidth, texHeight)
        {
            _buttonRectangle = new Rectangle(x, y, texWidth, texHeight);

            _defaultTexture = defaultTexture;
            _hoverTexture = hoverTexture;
            _clickedTexture = clickedTexture;
            this.Texture = _defaultTexture;
        }

        public void Update(GameTime gameTime)
        {
            switch (_buttonState)
            {
                case ButtonStates.None:
                    this.Texture = _defaultTexture;
                    break;

                case ButtonStates.Hover:
                    this.Texture = _hoverTexture;
                    break;

                case ButtonStates.Pressed:
                    this.Texture = _clickedTexture;
                    break;

                case ButtonStates.Released:
                    this.Texture = _defaultTexture;
                    break;
            } 
        }
    }
}
