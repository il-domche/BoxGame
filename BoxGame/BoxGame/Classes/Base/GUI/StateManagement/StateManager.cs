using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Indiv0.BoxGame.Classes.Base.GUI.States;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Indiv0.BoxGame.Classes.Base.GUI.StateManagement
{    
    class StateManager
    {
        #region Fields
        private int _currentState;
  
        List<State> StateList;   
    
        public enum StateOptions
        { 
            MainMenu,      
            GamePlay,
        }      

        public State PresentState
        {        
            get     
            {            
                return (StateList.ElementAt<State>(_currentState));     
            }   
        }     

        public int CurrentStateIndex
        {         
            get        
            {            
                return (_currentState);     
            }       
            set          
            {        
                this._currentState = value;         
            }       
        }      
        #endregion

        public StateManager(ContentManager contentManager)   
        {          
            // Create the stack to begin with      
            StateList = new List<State>();     
            InitializeStates(contentManager);    
        }       

        // Creates the states and stores them apppropriately  
        private void InitializeStates(ContentManager contentManager)    
        {         
            MainMenu mainMenu = new MainMenu(contentManager);
            GamePlay gamePlay = new GamePlay(contentManager); 
            StateList.Add(mainMenu);          
            StateList.Add(gamePlay);        
            _currentState = 0;      
        }

        public void Update(GameTime gameTime)
        {
            StateList[_currentState].Update(gameTime);
        }

        public void Draw(GameTime gameTime, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            StateList[_currentState].Draw(gameTime, graphicsDevice, spriteBatch);
        }

        public void PushState(int stateOptions)
        {
            _currentState = stateOptions;
        }   
    }
}
