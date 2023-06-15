using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;
using HitsLandscape.Material.StaticMaterial;

namespace HitsLandscape.Material
{
    public class Flower : Plant
    {
        public Flower(Random random)
        {
            state = new Solid();
            color = Color.HotPink;
            lifespan = random.Next(10, 201);
            GrowPossibility = 0.0005f;
            type = CellType.Flower;
        }
        
        protected override void Update(Cell cell, Map map, Random random)
        {
            List<Cell> neighbours = map.GetNeighborsOfCell(cell);

            var eligibleNeighbours = neighbours.Where(neighborCell =>
                neighborCell.GetPassable() && CheckPassableForFlower(neighborCell));

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
                cell.SetProperty(new Grass(random));
                cell.SetChanged();
            }
        }

        private bool CheckPassableForFlower(Cell cell)
        {
            if (cell.GetProperty().GetCellType() == CellType.Dirt ||
                cell.GetProperty().GetCellType() == CellType.Grass)
            {
                return true;
            }

            return false;
        }
    }
}