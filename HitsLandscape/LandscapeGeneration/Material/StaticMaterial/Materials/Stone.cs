using System.Drawing;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;

namespace HitsLandscape.Material.StaticMaterial
{
    public class Stone : StaticMaterial
    {
        public Stone()
        {
            state = new Solid();
            color = Color.Gray;
            type = CellType.Stone;
        }
    }
}