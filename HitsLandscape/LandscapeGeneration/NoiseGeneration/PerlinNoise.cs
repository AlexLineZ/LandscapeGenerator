using System;

namespace HitsLandscape.NoiseGeneration
{
    public class PerlinNoise : INoiseGenerator
    {
        private Random random;
        private int seed;
        public PerlinNoise()
        {
            random = new Random();
            seed = random.Next();
        }
        public float[,] GetNoise(int size)
        {
            float[,] noiseMap = new float[size, size];
            float frequency = 0.1f;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    noiseMap[x, y] = GeneratePerlinNoise(x, y, frequency);
                }
            }

            return noiseMap;
        }

        private float GeneratePerlinNoise(float x, float y, float frequency)
        {
            float total = 0f;
            float amplitude = 1f;
            float persistence = 0.5f;
            int octaves = 4;

            for (int i = 0; i < octaves; i++)
            {
                total += InterpolatedNoise(x * frequency, y * frequency) * amplitude;
                frequency *= 2f;
                amplitude *= persistence;
            }

            return total;
        }

        private float InterpolatedNoise(float x, float y)
        {
            int integerX = (int)x;
            int integerY = (int)y;
            float fractionalX = x - integerX;
            float fractionalY = y - integerY;

            float v1 = SmoothedNoise(integerX, integerY);
            float v2 = SmoothedNoise(integerX + 1, integerY);
            float v3 = SmoothedNoise(integerX, integerY + 1);
            float v4 = SmoothedNoise(integerX + 1, integerY + 1);

            float i1 = Interpolate(v1, v2, fractionalX);
            float i2 = Interpolate(v3, v4, fractionalX);

            return Interpolate(i1, i2, fractionalY);
        }

        private float SmoothedNoise(int x, int y)
        {
            float corners = (Noise(x - 1, y - 1) + Noise(x + 1, y - 1) + Noise(x - 1, y + 1) + Noise(x + 1, y + 1)) / 16f;
            float sides = (Noise(x - 1, y) + Noise(x + 1, y) + Noise(x, y - 1) + Noise(x, y + 1)) / 8f;
            float center = Noise(x, y) / 4f;

            return corners + sides + center;
        }

        private float Noise(int x, int y)
        {
            int n = x + y * 57 + seed;
            n = (n << 13) ^ n;
            return (1.0f - ((n * (n * n * 15731 + 789221) + 1376312589) & 0x7FFFFFFF) / 1073741824.0f);
        }

        private float Interpolate(float a, float b, float blend)
        {
            float theta = blend * (float)Math.PI;
            float f = (1f - (float)Math.Cos(theta)) * 0.5f;
            return a * (1f - f) + b * f;
        }
    }
}