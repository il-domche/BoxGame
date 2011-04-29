using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indiv0.BoxGame.Classes.Base
{
    class Unit : Sprite
    {
        public enum Countries
        {
            Russia,
            Japan,
            None
        }
        public enum BlockTypes
        {
            Attack,
            Defense,
            None
        }

        Countries _country;
        BlockTypes _blockType;

        public Unit(string texString, int x, int y, int texWidth, int texHeight, Countries country, BlockTypes blockType)
            : base(texString, x, y, texWidth, texHeight)
        {
            _country = country;
            _blockType = blockType;
        }
    }
}
