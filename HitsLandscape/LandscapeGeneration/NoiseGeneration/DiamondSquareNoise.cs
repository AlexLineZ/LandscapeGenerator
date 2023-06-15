using System;

namespace HitsLandscape.NoiseGeneration
{
    public class DiamondSquareNoise : INoiseGenerator
    {
        public float[,] GetNoise(int size)
        {
            int newSize = SizeOfMap(size);
            float[,] noise = CutOff(GenerateHeightMap(newSize, 3f), size);

            return noise;
        }

        private static float[,] CutOff(float[,] matrix, int size)
        {
            float[,] newMatrix = new float[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            return newMatrix;
        }

        private static float[,] GenerateHeightMap(int size, float roughness)
        {
            var random = new Random();
            float[,] heightMap = new float[size, size];

            int squareSize = size - 1;

            while (squareSize > 1)
            {
                DiamondStep(heightMap, squareSize, roughness, random);
                SquareStep(heightMap, squareSize, roughness, random);

                squareSize /= 2;
                roughness /= 2f;
            }

            return heightMap;
        }

        private static void DiamondStep(float[,] heightMap, int squareSize, float roughness, Random random)
        {
            for (int x = 0; x < heightMap.GetLength(0) - 1; x += squareSize)
            {
                for (int y = 0; y < heightMap.GetLength(1) - 1; y += squareSize)
                {
                    int halfSquareSize = squareSize / 2;

                    float cornerAvg = (heightMap[x, y] +
                                       heightMap[x + squareSize, y] +
                                       heightMap[x, y + squareSize] +
                                       heightMap[x + squareSize, y + squareSize]) / 4f;

                    float offset = (float)(random.NextDouble() * 2f - 1f) * roughness * squareSize;

                    heightMap[x + halfSquareSize, y + halfSquareSize] = cornerAvg + offset;
                }
            }
        }

        private static void SquareStep(float[,] heightMap, int squareSize, float roughness, Random random)
        {
            int size = heightMap.GetLength(0);

            for (int x = 0; x < size - 1; x += squareSize / 2)
            {
                for (int y = (x + squareSize / 2) % squareSize; y < size - 1; y += squareSize)
                {
                    int halfSquareSize = squareSize / 2;

                    float sideAvg = (heightMap[(x - halfSquareSize + size) % size, y] +
                                     heightMap[(x + halfSquareSize) % size, y] +
                                     heightMap[x, (y - halfSquareSize + size) % size] +
                                     heightMap[x, (y + halfSquareSize) % size]) / 4f;

                    float offset = (float)(random.NextDouble() * 2f - 1f) * roughness * squareSize;

                    heightMap[x, y] = sideAvg + offset;

                    if (y == size - 2 && squareSize > 1)
                    {
                        heightMap[x, 0] = sideAvg + offset;
                    }

                    if (x == size - 2 && squareSize > 1)
                    {
                        heightMap[0, y] = sideAvg + offset;
                    }
                }
            }
        }

        private static int SizeOfMap(int size)
        {
            int newSize = 1;
            while (newSize < size)
            {
                newSize *= 2;
            }

            return newSize + 1;
        }
    }

}