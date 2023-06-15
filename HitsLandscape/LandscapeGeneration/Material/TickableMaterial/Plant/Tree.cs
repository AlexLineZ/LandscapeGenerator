using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;
using HitsLandscape.Material.StaticMaterial;

namespace HitsLandscape.Material
{
    public class Tree : Plant
    {
        public Tree()
        {
            state = new Solid();
            GrowPossibility = 0.0001f;
            color = Color.Green;
            type = CellType.Tree;
        }
        
        protected override void Update(Cell cell, Map map, Random random)
        {
            List<Cell> neighbours = map.GetNeighborsOfCell(cell);

            var eligibleNeighbours = neighbours.Where(neighborCell =>
                neighborCell.GetPassable() && CheckPassableForTree(cell));

            foreach (Cell neighborCell in eligibleNeighbours)
            {
                if (RandomFun.RandomChance(random, GrowPossibility))
                {
                    neighborCell.SetProperty(new Grass(random));
                    neighborCell.SetChanged();
                }
            }
        }
        
        private bool CheckPassableForTree(Cell cell)
        {
            if (cell.GetProperty().GetCellType() == CellType.Dirt ||
                cell.GetProperty().GetCellType() == CellType.Grass ||
                cell.GetProperty().GetCellType() == CellType.Flower)
            {
                return true;
            }

            return false;
        }
    }
}