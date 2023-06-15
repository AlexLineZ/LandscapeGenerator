using System;
using HitsLandscape.Disaster;
using HitsLandscape.NoiseGeneration;

namespace HitsLandscape.LandscapeGeneration.Factory;

public class DisasterFactory
{
    public static IDisaster CreateDisaster(DisasterType type)
    {
        switch (type)
        {
            case DisasterType.Drought:
                return new Drought();
            case DisasterType.Flood:
                return new Flood();
            case DisasterType.Wildfire:
                return new Wildfire();
            default:
                throw new ArgumentException("Unsupported disaster type");
        }
    }
}