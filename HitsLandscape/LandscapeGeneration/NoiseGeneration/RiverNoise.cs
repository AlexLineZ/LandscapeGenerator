using System;

namespace HitsLandscape.NoiseGeneration
{
    public class RiverNoise : INoiseGenerator
    {
        public float[,] GetNoise(int mapSize)
        {
            float[,] noiseMap = new float[mapSize, mapSize];

            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    noiseMap[x, y] = GenerateRiverNoise(x, y, mapSize);
                }
            }

            return noiseMap;
        }

        private float GenerateRiverNoise(float x, float y, int mapSize)
        {
            float scale = 10f;
            float amplitude = 1f;
            float frequency = 1f;
            float noiseHeight = 0f;

            for (int i = 0; i < 4; i++)
            {
                float sampleX = x / mapSize * scale * frequency;
                float sampleY = y / mapSize * scale * frequency;

                float perlinValue = (float)Math.Sin(sampleX + sampleY);
                noiseHeight += perlinValue * amplitude;

                amplitude *= 0.5f;
                frequency *= 2f;
            }

            return noiseHeight;
        }
    }
}