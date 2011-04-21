using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Indiv0.BoxGame.Classes.Base;
using BoxGame.Classes.Base;

namespace Indiv0.BoxGame.Classes.Base
{
    class Terrain
    {
        #region Constants
        //Used to be 0
        private const int WATER_LEVEL = -1;
        private const int GRASS_LEVEL = 0;
        //Used to be 40
        private const int MOUNTAIN_LEVEL = 20;
        #endregion

        #region Class Variables
        private Texture2D _grassTexture;
        private Texture2D _waterTexture;
        private Texture2D _mountainTexture;
        private Sprite[,] _textureMap;
        private float[,] _heightMap;
        private int _iterations = 5;
        private float _seed = 0;
        private int _variation = 10;
        //A roughness of 2-3 seems to be ideal.
        private int _roughness = 2;
        private int _arrayWidth;
        private int _arrayHeight;
        private int _blocksWide;
        private int _blocksHigh;
        private int _blockWidth;
        private int _blockHeight;
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
        public int Iterations
        {
            get { return _iterations; }
            set { _iterations = value; }
        }
        public float Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }
        public int Variation
        {
            get { return _variation; }
            set { _variation = value; }
        }
        public int Roughness
        {
            get { return _roughness; }
            set { _roughness = value; }
        }
        #endregion
        
        public Terrain(string texString, int w, int h, int screenWidth, int screenHeight,
            Texture2D grassTexture, Texture2D waterTexture, Texture2D mountainTexture)
        {
            _blockWidth = w;
            _blockHeight = h;
            _grassTexture = grassTexture;
            _waterTexture = waterTexture;
            _mountainTexture = mountainTexture;
            //_blocksWide = screenWidth / _blockWidth;
            //_blocksHigh = screenHeight / _blockHeight;
            //_arrayWidth = _blocksWide-1;
            //_arrayHeight = _blocksHigh-1;

            GenerateNewMap();
        }

        public void GenerateNewMap()
        {
            _heightMap = DSAlgorithm.GenerateHeightMap(_iterations, _seed, _variation, _roughness);

            _arrayWidth = _heightMap.GetUpperBound(0);
            _arrayHeight = _heightMap.GetUpperBound(1);
            _blocksWide = _arrayWidth++;
            _blocksHigh = _arrayHeight++;

            _textureMap = new Sprite[_arrayWidth, _arrayHeight];

            GenerateTextureMap();
        }
        
        public void DrawTerrain(SpriteBatch spriteBatch)
        {
            for (int j = 0; j < _arrayHeight; j++)
            {
                for (int i = 0; i < _arrayWidth; i++)
                {
                    spriteBatch.Draw(_textureMap[i, j].Texture,
                        _textureMap[i, j].Position, Color.White);
                }
            }
        }

        private void GenerateTextureMap()
        {
            for (int j = 0; j < _arrayHeight; j++)
            {
                for (int i = 0; i < _arrayWidth; i++)
                {
                    if (_heightMap[i, j] <= WATER_LEVEL)
                    {
                        _textureMap[i, j] = new Sprite("res/art/terrain/water_block", (i * _blockWidth), (j * _blockHeight),
                            _blockWidth, _blockHeight);
                        _textureMap[i, j].TerrainType = Sprite.TerrainTypes.Water;
                        _textureMap[i, j].Texture = _waterTexture;
                    }
                    if (_heightMap[i, j] > WATER_LEVEL && _heightMap[i, j] < MOUNTAIN_LEVEL)
                    {
                        _textureMap[i, j] = new Sprite("res/art/terrain/grass_block", (i * _blockWidth), (j * _blockHeight),
                            _blockWidth, _blockHeight);
                        _textureMap[i, j].TerrainType = Sprite.TerrainTypes.Grass;
                        _textureMap[i, j].Texture = _grassTexture;
                    }
                    if (_heightMap[i, j] >= MOUNTAIN_LEVEL)
                    {
                        _textureMap[i, j] = new Sprite("res/art/terrain/mountain_block", (i * _blockWidth), (j * _blockHeight),
                            _blockWidth, _blockHeight);
                        _textureMap[i, j].TerrainType = Sprite.TerrainTypes.Mountain;
                        _textureMap[i, j].Texture = _mountainTexture;
                    }
                }
            }
        }
    }
}
