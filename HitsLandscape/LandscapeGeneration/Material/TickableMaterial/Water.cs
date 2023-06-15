using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;


namespace HitsLandscape.Material
{
    public class Water : TickableMaterial
    {
        private float volume;

        public Water()
        {
            volume = 10.0f;
            state = new Fluid();
            color = Color.DodgerBlue;
            type = CellType.Water;
        }
        
        public Water(float volume)
        {
            this.volume = volume;
            state = new Fluid();
            color = Color.DodgerBlue;
            type = CellType.Water;
        }

        public bool IsWaterBlock()
        {
            return volume > 3f;
        }
        protected override void Update(Cell cell, Map map, Random random)
        {
            if (volume < 0.5f)
            {
                return;
            }
            
            List<Cell> neighbours = map.GetNeighborsOfCell(cell);
            float waterVolumeToSpread = volume * 0.9f;
            
            foreach (Cell currentCell in neighbours.ToList())
            {
                if (currentCell.GetProperty().GetPossible() && currentCell.GetElevation() < cell.GetElevation())
                {
                    currentCell.SetProperty(new Water(waterVolumeToSpread));
                    currentCell.SetChanged();
                }
            }
        }
    }
}