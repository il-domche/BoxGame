using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indiv0.BoxGame.Classes.Base.GUI.Input
{
    class Button : Sprite
    {
        private bool _isClicked = false;
        private bool _isDisplayed = false;

        public bool IsDisplayed
        {
            get { return _isDisplayed; }
            set { _isDisplayed = value; }
        }
        public bool IsClicked
        {
            get { return _isClicked; }
            set { _isClicked = value; }
        }

        public Button(string texString, int x, int y, int texWidth, int texHeight)
            : base(texString, x, y, texWidth, texHeight)
        {
        }
    }
}
