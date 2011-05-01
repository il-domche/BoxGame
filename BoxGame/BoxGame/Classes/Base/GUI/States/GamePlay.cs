using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Indiv0.BoxGame.Classes.Base.GUI.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Indiv0.BoxGame.Classes.Base.GUI.States
{    
    class GamePlay : State   
    {                    
        public GamePlay(ContentManager cManager)        
        {            
            //cManager.RootDirectory = "Content";            
            //Console.WriteLine("Content Manager root directory: "+cManager.RootDirectory.ToString());                        
            //paddle = cManager.Load<Texture2D>(cManager.RootDirectory+"/GameAssets/breakout_paddle");            
            //entityList = new List<Entities.BaseEntity>();            
            //loadContent(cManager);        
        }        
        public override void LoadContent(ContentManager cManager)        
        {            
            //paddle = new Entities.Paddle();            
            //paddle.entity_texture = cManager.Load<Texture2D>(cManager.RootDirectory + "/GameAssets/breakout_paddle");            
            //entityList.Add(paddle);        
        }        
        public override void Draw(ref Microsoft.Xna.Framework.GameTime gameTime, 
            GraphicsDevice graphicsDevice, 
            ref SpriteBatch spriteBatch)        
        {            
            graphicsDevice.Clear(Color.Black);            
            //paddle.positionX += ((GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * gameTime.ElapsedGameTime.Milliseconds) * 1.25f;            
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))            
            {                
                //paddle.positionY -= 1.0f;            
            }            
            else if (GamePad.GetState(PlayerIndex.One).IsButtonUp(Buttons.A))            
            {                
                //if (paddle.positionY <= graphicsDevice.Viewport.Height-10)                
                //{                    
                //    paddle.positionY += 1.0f;                
                //}            
            }                                   
            //foreach (Entities.BaseEntity entity in entityList)            
            //{                
            //    spriteBatch.Draw(entity.entity_texture, new Vector2(entity.positionX, entity.positionY), Color.White);            
            //}            
            //spriteBatch.Draw(paddle, new Vector2(gDev.Viewport.Width/2,gDev.Viewport.Height/2), Color.White);        
        }    
    }
}