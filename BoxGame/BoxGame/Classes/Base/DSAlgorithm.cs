using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoxGame.Classes.Base
{
    class DSAlgorithm
    {
        private static Random randomGenerator = new Random();

        public static float[,] GenerateHeightMap(int iterations, float seed, int variation, int roughness)
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

                DiamondStep(map, minCoordinate, variation, size, roughness);
                SquareStepEven(map, minCoordinate, variation, size, maxIndex, roughness);
                SquareStepOdd(map, minCoordinate, size, maxIndex, variation, roughness);

                variation = variation >> 1;
            }

            return map;
        }

        private static void DiamondStep(float[,] map, int minCoordinate, int variation, int arrayLength, int roughness)
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
                            map, x, y, roughness);
                }
            }
        }

        private static void SquareStepEven(float[,] map, int minCoordinate, int variation, int size, int maxIndex, int roughness)
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
                            map, x, y, roughness);
                }
            }
        }

        private static void SquareStepOdd(float[,] map, int minCoordinate, int size, int maxIndex, int variation, int roughness)
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
                            map, x, y, roughness);
                }
            }
        }

        private static void CalculateAndInsertAverage(float val1, float val2, float val3,
                float val4, int variation, float[,] map, int x, int y, int roughness)
        {
            float avg = (int)(val1 + val2 + val3 + val4) >> 2;// average
            int f = randomGenerator.Next(roughness);
            int var = (int)((f * ((variation << 1) + 1)) - variation);
            map[x, y] = avg + var;
        }
    }
}
