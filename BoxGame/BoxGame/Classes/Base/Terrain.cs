using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Indiv0.BoxGame.Classes.Base
{
    class Terrain : Sprite
    {
        #region Class Variables
        private Sprite[,] _textureMap;
        private int _arrayWidth;
        private int _arrayHeight;
        private int _blocksWide;
        private int _blocksHigh;
        #endregion

        #region Public Variables
        public Sprite[,] TextureMap
        {
            get { return _textureMap; }
            set { _textureMap = value; }
        }
        public int ArrayWidth
        {
            get { return _arrayWidth; }
            set { _arrayWidth = value; }
        }
        public int ArrayHeight
        {
            get { return _arrayHeight; }
            set { _arrayHeight = value; }
        }
        #endregion

        public Terrain(/*GraphicsDevice graphicsDevice, SpriteBatch spriteBatch,*/
            string texString, 
            int x, int y, 
            int w, int h, 
            int screenWidth, int screenHeight)
            : base(/*graphicsDevice, spriteBatch, */texString, x, y, w, h)
        {
            _blocksWide = screenWidth / Width;
            _blocksHigh = screenHeight / Height;
            _arrayWidth = _blocksWide-1;
            _arrayHeight = _blocksHigh-1;
            _textureMap = new Sprite [_blocksWide,_blocksHigh];
            InitializeTerrain();
        }

        private void InitializeTerrain(){
            int ii;
            int jj;
            for (int j = 0; j <= _arrayHeight; j++){
                for (int i = 0; i <= _arrayWidth; i++){
                    ii = i * Width;
                    jj = j * Height;
                    _textureMap[i, j] = new Sprite(/*graphicsDevice, spriteBatch, */
                        "res/art/terrain/grass_", 
                        ii, jj, Width, Height);
                }
            }
        }

        public void DrawTerrain(SpriteBatch spriteBatch)
        {
            for (int j = 0; j <= _arrayHeight; j++)
            {
                for (int i = 0; i <= _arrayWidth; i++)
                {
                    spriteBatch.Draw(_textureMap[i, j].Texture,
                        _textureMap[i, j].Position, Color.White);
                }
            }
        }
    }
}
