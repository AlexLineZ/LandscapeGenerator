using System.Drawing;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;

namespace HitsLandscape.Material.StaticMaterial
{
    public class Dirt : StaticMaterial
    {
        public Dirt()
        {
            state = new Solid();
            color = Color.Sienna;
            type = CellType.Dirt;
        }
    }
}