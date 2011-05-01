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
        public virtual void Draw(ref GameTime gameTime, GraphicsDevice graphicsDevice, ref SpriteBatch spriteBatch) 
        { } 
        public virtual void LoadContent(ContentManager contentManager) 
        { } 
    } 
}
