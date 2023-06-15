using System.Collections.Generic;

namespace HitsLandscape
{
    public class Cell
    {
        private int x;
        private int y;
        private float elevation;
        private bool isPassable;
        private IProperty property;
        private bool changed;
        
        public Cell()
        {
            x = 0;
            y = 0;
            elevation = 0.0f;
            isPassable = true;
            property = null;
        }

        public Cell(int x, int y, float elevation, bool isPassable, IProperty property)
        {
            this.x = x;
            this.y = y;
            this.elevation = elevation;
            this.isPassable = isPassable;
            this.property = property;
        }

        public void ChangePassable(bool value)
        {
            this.isPassable = value;
        }
        
        public float GetElevation()
        {
            return elevation;
        }

        public bool GetPassable()
        {
            return isPassable;
        }
        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
        
        public IProperty GetProperty()
        {
            return property;
        }

        public void SetProperty(IProperty property)
        {
            this.property = property;
        }

        public void SetElevation(float newElevation)
        {
            this.elevation = newElevation;
        }
        public bool HasChanged()
        {
            return changed;
        }

        public void SetChanged()
        {
            changed = true;
        }

        public void ResetChanged()
        {
            changed = false;
        }
    }
}