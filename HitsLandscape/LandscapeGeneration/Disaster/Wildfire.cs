using System;
using System.Linq;
using HitsLandscape.Material;

namespace HitsLandscape.Disaster
{
    public class Wildfire : IDisaster
    {
        private const float IgnitionProbability = 0.1f;

        public void StartDisaster(Map map, Random random)
        {
            SetFire(map, random);
        }

        private void SetFire(Map map, Random random)
        {
            foreach (Cell cell in map.GetAllCells().Cast<Cell>())
            {
                if (CheckStartFirePoints(cell) && RandomFun.RandomChance(random, IgnitionProbability))
                {
                    cell.SetProperty(new Fire());
                    cell.SetChanged();
                }
            }
        }

        private bool CheckStartFirePoints(Cell cell)
        {
            return cell.GetProperty().GetCellType() == CellType.Tree;
        }
    }
}