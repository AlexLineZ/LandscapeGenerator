using System.Collections.Generic;

namespace HitsLandscape.NoiseGeneration
{
    public interface INoiseGenerator
    {
        float[,] GetNoise(int size);
    }
}