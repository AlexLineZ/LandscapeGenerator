using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HitsLandscape.LandscapeGeneration.Material.StageOfAggregation;
using HitsLandscape.Material.StaticMaterial;

namespace HitsLandscape.Material;

public class Fire : TickableMaterial
{
    private const float SpreadProbability = 0.1f;
    private const int lifespan = 20;
    private int tick = 0;

    public Fire()
    {
        color = Color.OrangeRed;
        state = new Gas();
        type = CellType.Fire;
    }

    protected override void Update(Cell cell, Map map, Random random)
    {
        List<Cell> neighbours = map.GetNeighborsOfCell(cell);

        neighbours.Where(neighborCell => CheckSpreadForFire(neighborCell))
            .Where(neighborCell => RandomFun.RandomChance(random, SpreadProbability))
            .ToList()
            .ForEach(neighborCell =>
            {
                neighborCell.SetProperty(new Fire());
                neighborCell.SetChanged();
            });

        tick++;

        if (tick >= lifespan)
        {
            cell.SetProperty(new Dirt());
            cell.SetChanged();
        }
    }

    private bool CheckSpreadForFire(Cell cell)
    {
        IProperty property = cell.GetProperty();
        if (property.GetCellType() == CellType.Flower ||
            property.GetCellType() == CellType.Grass ||
            property.GetCellType() == CellType.Tree)
        {
            return true;
        }

        return false;
    }
}
