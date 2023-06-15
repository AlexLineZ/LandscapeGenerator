using System;
using System.Linq;
using HitsLandscape.Material;

namespace HitsLandscape.Disaster
{
    public class Flood : IDisaster
    {
        private const float SkipPossibility = 0.3f;
        private const int VolumeLevel = 10;
        public void StartDisaster(Map map, Random random)
        {
            RestorePassable(map);
            UpWaterLevel(map, random);
        }
        
        private void UpWaterLevel(Map map, Random random)
        {
            var cells = map.GetAllCells();
            
            var changedCells = cells.Cast<Cell>()
                .Where(cell => cell.GetProperty() is Water water 
                               && random.NextDouble() > SkipPossibility && water.IsWaterBlock())
                .Select(cell =>
                {
                    cell.SetProperty(new Water(VolumeLevel));
                    cell.SetChanged();
                    return cell;
                })
                .ToList();

            var updatedCells = cells.Cast<Cell>()
                .Where(cell => cell.GetProperty() is Water water 
                               && random.NextDouble() <= SkipPossibility && water.IsWaterBlock())
                .Select(cell =>
                {
                    cell.SetProperty(new Water(VolumeLevel));
                    cell.SetElevation(cell.GetElevation() / VolumeLevel);
                    return cell;
                })
                .ToList();

            changedCells.AddRange(updatedCells);
        }
        
        private void RestorePassable(Map map)
        {
            var cells = map.GetAllCells();
            
            foreach (Cell cell in cells)
            {
                cell.ChangePassable(true);
            }
        }
    }
}