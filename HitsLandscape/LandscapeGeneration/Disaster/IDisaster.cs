using System;

namespace HitsLandscape.Disaster
{
    public interface IDisaster
    {
        void StartDisaster(Map map, Random random);
    }
}