using System;
using System.Drawing;

namespace HitsLandscape
{
    public interface IProperty
    {
        void Tick(Cell cell, Map map, Random random);
        bool GetPossible();
        Color Display();
        CellType GetCellType();
    }
}