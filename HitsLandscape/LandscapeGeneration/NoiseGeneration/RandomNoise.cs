using System;
using System.Collections.Generic;
using System.Drawing;

namespace HitsLandscape.NoiseGeneration
{
    public class RandomNoise : INoiseGenerator
    {
        Random random = new Random();
        public float[,] GetNoise(int size)
        {
            float[,] noise = new float[size, size];
            
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    noise[x, y] = (float)random.NextDouble();
                }
            }

            return noise;
        }
    }
}