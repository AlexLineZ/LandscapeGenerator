using System;
using HitsLandscape.Material;
using HitsLandscape.Material.StaticMaterial;

namespace HitsLandscape.Disaster
{
    public class Drought : IDisaster
    {
        public void StartDisaster(Map map, Random random)
        {
            DestroyPassable(map);
            DestroyWater(map, random);
        }

        private void DestroyPassable(Map map)
        {
            foreach (Cell cell in map.GetAllCells())
            {
                cell.ChangePassable(false);
            }
        }

        private void DestroyWater(Map map, Random random)
        {
            var cells = map.GetAllCells();
            foreach (Cell cell in cells)
            {
                IProperty property = cell.GetProperty();
                if (property is Water water)
                {
                    if (random.NextDouble() > 0.3 && water.IsWaterBlock())
                    {
                        cell.SetProperty(new Sand());
                        cell.SetChanged();
                    }
                    
                    else
                    {
                        cell.SetProperty(new Water(0.4f));
                        cell.SetChanged();
                    }
                }
            }
        }
    }
}