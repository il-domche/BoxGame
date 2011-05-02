using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Indiv0.BoxGame.Classes.Base.GUI.StateManagement
{ 
    class State
    {
        #region Fields
        bool _isCurrentState = false;
        bool _isPopup = false;

        public bool IsPopup
        {
            get { return _isPopup; }
            set { _isPopup = value; }
        }

        public bool IsCurrentState
        {
            get { return _isCurrentState; }
            set { _isCurrentState = value; }
        }
        #endregion

        public virtual void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch) 
        { } 
        public virtual void LoadContent(ContentManager contentManager) 
        { }
        public virtual void Update(GameTime gameTime)
        { }
    } 
}
