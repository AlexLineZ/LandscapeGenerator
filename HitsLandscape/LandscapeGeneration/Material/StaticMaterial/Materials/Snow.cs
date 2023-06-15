using System.Drawing;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;

namespace HitsLandscape.Material.StaticMaterial
{
    public class Snow : StaticMaterial
    {
        public Snow()
        {
            state = new Solid();
            color = Color.Silver;
            type = CellType.Snow;
        }
    }
}