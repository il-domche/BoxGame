using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Indiv0.BoxGame.Classes.Base;

namespace Indiv0.BoxGame.Classes.Base
{
    class Terrain
    {
        #region Constants
        private const int WATER_LEVEL = 0;
        private const int GRASS_LEVEL = 0;
        private const int MOUNTAIN_LEVEL = 40;
        #endregion

        #region Class Variables
        private static Random randomGenerator = new Random();
        private Sprite[,] _textureMap;
        private float[,] _heightMap;
        //private int[,] _heightMap;
        private int _arrayWidth;
        private int _arrayHeight;
        private int _blocksWide;
        private int _blocksHigh;
        private int _blockWidth;
        private int _blockHeight;
        private int _seedOne = 33; 
        private int _seedTwo = 44;
        private int _seedThree = 26;
        private int _seedFour = 11;
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

        public Terrain(string texString, int w, int h, int screenWidth, int screenHeight)
        {
            _blockWidth = w;
            _blockHeight = h;
            //_blocksWide = screenWidth / _blockWidth;
            //_blocksHigh = screenHeight / _blockHeight;
            //_arrayWidth = _blocksWide-1;
            //_arrayHeight = _blocksHigh-1;

            _heightMap = GenerateHeightMap(5, 0, 20f);

            _arrayWidth = _heightMap.GetUpperBound(0);
            _arrayHeight = _heightMap.GetUpperBound(1);
            _blocksWide = _arrayWidth++;
            _blocksHigh = _arrayHeight++;

            _textureMap = new Sprite[_arrayWidth, _arrayHeight];
        }

        private float[,] GenerateHeightMap(int iterations, float seed, float variation)
        {
            //Calculate the size of the array by using <<. E.g. (1 << 5) + 1 = 33
            int size = (1 << iterations) + 1;
            int maxIndex = size - 1;

            float[,] map = new float[size, size];

            map[0, 0] = seed;
            map[0, maxIndex] = seed;
            map[maxIndex, 0] = seed;
            map[maxIndex, maxIndex] = seed;

            for (int i = 1; i <= iterations; i++)
            {
                //Set the minCoordinate of the current square to maxIndex >> i. E.g. (32 >> 5) = 1 OR (16 >> 2) = 4
                int minCoordinate = maxIndex >> i;
                size = minCoordinate << 1;

                DiamondStep(map, minCoordinate, variation, size);
                SquareStepEven(map, minCoordinate, variation, size, maxIndex);
                SquareStepOdd(map, size, minCoordinate, maxIndex, variation);

                variation = variation / 2;
            }

            return map;
        }

        private void DiamondStep(float[,] map, int minCoordinate, float variation, int arrayLength)
        {
            for (int x = minCoordinate; x < (arrayLength - minCoordinate); x += arrayLength)
            {
                for (int y = minCoordinate; y < (map.GetLength(0) - minCoordinate); y += arrayLength)
                {
                    int left = x - minCoordinate;
                    int right = x + minCoordinate;
                    int up = y - minCoordinate;
                    int down = y + minCoordinate;

                    // the four corner values
                    float val1 = map[left, up];   // upper left
                    float val2 = map[left, down]; // lower left
                    float val3 = map[right, up];  // upper right
                    float val4 = map[right, down];// lower right

                    CalculateAndInsertAverage(val1, val2, val3, val4, variation,
                            map, x, y);
                }
            }
        }

        private void SquareStepEven(float[,] map, int minCoordinate, float variation, int size, int maxIndex)
        {
            for (int x = minCoordinate; x < map.GetLength(0); x += size)
            {
                for (int y = 0; y < map.GetLength(0); y += size)
                {
                    if (y == maxIndex)
                    {
                        map[x, y] = map[x, 0];
                        continue;
                    }

                    int left = x - minCoordinate;
                    int right = x + minCoordinate;
                    int down = y + minCoordinate;
                    int up = 0;

                    if (y == 0)
                    {
                        up = maxIndex - minCoordinate;
                    }
                    else
                    {
                        up = y - minCoordinate;
                    }

                    // the four corner values
                    float val1 = map[left, y]; // left
                    float val2 = map[x, up];   // up
                    float val3 = map[right, y];// right
                    float val4 = map[x, down]; // down

                    CalculateAndInsertAverage(val1, val2, val3, val4, variation,
                            map, x, y);
                }
            }
        }

        private void SquareStepOdd(float[,] map, int size, int minCoordinate,
                int maxIndex, float variation)
        {
            for (int x = 0; x < map.GetLength(0); x += size)
            {
                for (int y = minCoordinate; y < map.GetLength(0); y += size)
                {
                    if (x == maxIndex)
                    {
                        map[x, y] = map[0, y];
                        continue;
                    }

                    int left = 0;
                    int right = x + minCoordinate;
                    int down = y + minCoordinate;
                    int up = y - minCoordinate;

                    if (x == 0)
                    {
                        left = maxIndex - minCoordinate;
                    }
                    else
                    {
                        left = x - minCoordinate;
                    }

                    // the four corner values
                    float val1 = map[left, y]; // left
                    float val2 = map[x, up];   // up
                    float val3 = map[right, y];// right
                    float val4 = map[x, down]; // down

                    CalculateAndInsertAverage(val1, val2, val3, val4, variation,
                            map, x, y);
                }
            }
        }

        private static void CalculateAndInsertAverage(float val1, float val2, float val3,
                float val4, float variation, float[,] map, int x, int y)
        {
            float avg = (val1 + val2 + val3 + val4) / 2;// average
            int f = randomGenerator.Next();
            int var = (int)((randomGenerator.Next() * ((variation * 2) + 1)) - variation);
            map[x, y] = avg + var;
        }

        public void GenerateTextureMap(Texture2D grassTexture, Texture2D waterTexture, Texture2D mountainTexture)
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
                        _textureMap[i, j].Texture = waterTexture;
                    }
                    if (_heightMap[i, j] > WATER_LEVEL && _heightMap[i, j] < MOUNTAIN_LEVEL)
                    {
                        _textureMap[i, j] = new Sprite("res/art/terrain/grass_block", (i * _blockWidth), (j * _blockHeight),
                            _blockWidth, _blockHeight);
                        _textureMap[i, j].TerrainType = Sprite.TerrainTypes.Grass;
                        _textureMap[i, j].Texture = grassTexture;
                    }
                    if (_heightMap[i, j] >= MOUNTAIN_LEVEL)
                    {
                        _textureMap[i, j] = new Sprite("res/art/terrain/mountain_block", (i * _blockWidth), (j * _blockHeight),
                            _blockWidth, _blockHeight);
                        _textureMap[i, j].TerrainType = Sprite.TerrainTypes.Mountain;
                        _textureMap[i, j].Texture = mountainTexture;
                    }
                }
            }
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
    }
}
