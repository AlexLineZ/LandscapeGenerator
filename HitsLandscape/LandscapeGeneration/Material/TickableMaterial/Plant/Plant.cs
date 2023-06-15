using System;

namespace HitsLandscape.Material
{
    public abstract class Plant : TickableMaterial
    {
        protected int lifespan;
        protected int tick = 0;
        protected float GrowPossibility;
    }
}