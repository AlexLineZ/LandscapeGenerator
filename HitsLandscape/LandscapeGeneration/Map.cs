using System;
using System.Collections.Generic;
using HitsLandscape.Factory;
using HitsLandscape.Material;
using HitsLandscape.NoiseGeneration;

namespace HitsLandscape
{
    public class Map
    {
        private Cell[,] cells; 
        public int Size { get; private set; }
        private Random random;

        public Map(int size)
        {
            Size = size;
            cells = new Cell[Size, Size];
            random = new Random();
        }

        public int GetMapSize()
        {
            return Size;
        }

        public void UpdateMap()
        {
            foreach (Cell cell in cells)
            {
                if (cell != null)
                {
                    cell.GetProperty().Tick(cell, this, random);
                }
            }
        }

        public List<Cell> GetAllChangedCells()
        {
            List<Cell> changedCells = new List<Cell>();
            
            foreach (Cell cell in cells)
            {
                if (cell.HasChanged())
                {
                    changedCells.Add(cell);
                    cell.ResetChanged();
                }
            }

            return changedCells;
        }

        public void SetCell(Cell cell)
        {
            int x = cell.GetX();
            int y = cell.GetY();
            
            this.cells[x, y] = cell;
            this.cells[x, y].SetChanged();
        }

        public Cell[,] GetAllCells()
        {
            return cells;
        }

        public List<Cell> GetNeighborsOfCell(Cell cell)
        {
            List<Cell> neighbors = new List<Cell>();

            int x = cell.GetX();
            int y = cell.GetY();

            if (y > 0)
            {
                neighbors.Add(this.GetCell(x, y - 1));
                if (x > 0)
                    neighbors.Add(this.GetCell(x - 1, y - 1));
                if (x < this.Size - 1)
                    neighbors.Add(this.GetCell(x + 1, y - 1));
            }
            
            if (y < this.Size - 1)
            {
                neighbors.Add(this.GetCell(x, y + 1));
                if (x > 0)
                    neighbors.Add(this.GetCell(x - 1, y + 1));
                if (x < this.Size - 1)
                    neighbors.Add(this.GetCell(x + 1, y + 1));
            }
            
            if (x > 0)
                neighbors.Add(this.GetCell(x - 1, y));
            if (x < this.Size - 1)
                neighbors.Add(this.GetCell(x + 1, y));

            return neighbors;
        }
        
        public Cell GetCell(int x, int y)
        {
            if (x >= 0 && x < Size && y >= 0 && y < Size)
            {
                return cells[x, y];
            }
            return null;
        }
    }
}
