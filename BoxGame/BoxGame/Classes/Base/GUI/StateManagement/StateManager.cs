using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Indiv0.BoxGame.Classes.Base.GUI.States;

namespace Indiv0.BoxGame.Classes.Base.GUI.StateManagement
{    
    class StateManager   
    {       
        private int _currentState;
  
        List<State> StateList;   
    
        public enum StateOptions
        {         
            LoadScreen,   
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

        public void PushState(int stateOptions)   
        {        
            _currentState = stateOptions;     
        }   

        public StateManager(ContentManager contentManager)   
        {          
            // Create the stack to begin with      
            StateList = new List<State>();     
            InitializeStates(contentManager);    
        }       
        // Creates the states and stores them apppropriately  
        private void InitializeStates(ContentManager contentManager)    
        {         
            LoadScreen loadScreen = new LoadScreen(contentManager);
            MainMenu mainMenu = new MainMenu(contentManager);
            GamePlay gamePlay = new GamePlay(contentManager);
            StateList.Add(loadScreen);            
            StateList.Add(mainMenu);          
            StateList.Add(gamePlay);        
            _currentState = 0;      
        }   
    }
}
