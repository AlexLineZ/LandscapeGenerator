using System;

namespace HitsLandscape.NoiseGeneration
{
    public class CraterNoise : INoiseGenerator
    {
        public float[,] GetNoise(int size)
        {
            float[,] noise = new float[size, size];
            float maxDistance = size / 2f;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    float distanceX = Math.Abs(x - size / 2f);
                    float distanceY = Math.Abs(y - size / 2f);
                    
                    float distance = (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

                    float normalizedDistance = distance / maxDistance;
                    float value = Clamp(normalizedDistance, 0f, 1f);

                    noise[x, y] = value;
                }
            }

            return noise;
        }


        private float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
    }
}