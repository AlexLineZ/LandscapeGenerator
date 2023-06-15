using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;
using HitsLandscape.Material.StaticMaterial;

namespace HitsLandscape.Material
{
    public class Grass : Plant
    {
        public Grass(Random random)
        {
            state = new Solid();
            color = Color.LimeGreen;
            GrowPossibility = 0.0008f;
            lifespan = random.Next(20, 501);
            type = CellType.Grass;
        }

        protected override void Update(Cell cell, Map map, Random random)
        {
            List<Cell> neighbours = map.GetNeighborsOfCell(cell);

            var eligibleNeighbours = neighbours.Where(neighborCell =>
                neighborCell.GetPassable() && CheckPassableForGrass(cell));

            foreach (Cell neighborCell in eligibleNeighbours)
            {
                if (RandomFun.RandomChance(random, GrowPossibility))
                {
                    neighborCell.SetProperty(new Grass(random));
                    neighborCell.SetChanged();
                }
            }

            tick++;

            if (tick >= lifespan)
            {
                cell.SetProperty(new Dirt());
                cell.SetChanged();
            }
        }
        
        private bool CheckPassableForGrass(Cell cell)
        {
            return cell.GetProperty().GetCellType() == CellType.Dirt;
        }
        
    }
}