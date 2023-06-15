using System.Drawing;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;

namespace HitsLandscape.Material.StaticMaterial
{
    public class Sand : StaticMaterial
    {
        public Sand()
        {
            state = new Solid();
            color = Color.Khaki;
            type = CellType.Sand;
        }
    }
}