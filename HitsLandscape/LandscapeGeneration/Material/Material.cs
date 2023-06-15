using System;
using System.Drawing;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;

namespace HitsLandscape.Material
{
    public abstract class Material : IProperty
    {
        protected Color color;
        protected IStageOfAggregation state;
        protected CellType type;
        
        public void Tick(Cell cell, Map map, Random random)
        {
            Update(cell, map, random);
        }

        protected abstract void Update(Cell cell, Map map, Random random);

        public bool GetPossible()
        {
            return GetStageOfAggregation().IsPassable();
        }
        public IStageOfAggregation GetStageOfAggregation()
        {
            return state;
        }
        public Color Display()
        {
            return GetColor();
        }

        private Color GetColor()
        {
            return color;
        }

        public CellType GetCellType()
        {
            return type;
        }
    }
}