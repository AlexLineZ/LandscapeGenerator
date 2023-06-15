using System;

namespace HitsLandscape;

static class RandomFun
{ 
    public static bool RandomChance(Random random, float chance)
    {
        return random.NextDouble() < chance;
    }
}