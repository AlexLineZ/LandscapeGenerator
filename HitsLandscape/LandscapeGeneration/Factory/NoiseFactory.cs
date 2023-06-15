using System;
using HitsLandscape.NoiseGeneration;

namespace HitsLandscape.Factory
{
    public class NoiseFactory
    {
        public INoiseGenerator CreateNoise(NoiseType type)
        {
            switch (type)
            {
                case NoiseType.Perlin:
                    return new PerlinNoise();
                case NoiseType.River:
                    return new RiverNoise();
                case NoiseType.Crater:
                    return new CraterNoise();
                case NoiseType.Random:
                    return new RandomNoise();
                case NoiseType.DiamondSquare:
                    return new DiamondSquareNoise();
                default:
                    throw new ArgumentException("Unsupported noise type");
            }
        }
    }
}