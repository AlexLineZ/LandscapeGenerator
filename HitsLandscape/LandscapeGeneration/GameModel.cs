using System;
using System.Collections.Generic;
using HitsLandscape.Disaster;
using HitsLandscape.Factory;
using HitsLandscape.LandscapeGeneration.Factory;
using HitsLandscape.NoiseGeneration;
using Timer = System.Threading.Timer;

namespace HitsLandscape
{
    public class GameModel
    {
        private Map map;
        private Timer updateTimer;
        private Timer disasterTimer;
        private const int updateInterval = 800;
        private const int disasterInterval = 30000;
        private Random random;

        public event EventHandler MapUpdated;

        public void CreateMap(int size, NoiseType noiseType)
        {
            map = new Map(size);
            map = MapGenerator.FillMap(noiseType, map);
            random = new Random();

            updateTimer = new Timer(UpdateMap, null, 0, updateInterval);
            disasterTimer = new Timer(TriggerRandomDisaster, null, disasterInterval, disasterInterval);
        }

        public Map GetMap()
        {
            return map;
        }

        public void GetDisaster(DisasterType type)
        {
            IDisaster disaster = DisasterFactory.CreateDisaster(type);
            disaster.StartDisaster(map, random);
            OnMapUpdated();
        }

        private void UpdateMap(object state)
        {
            map.UpdateMap();
            OnMapUpdated();
        }

        private void TriggerRandomDisaster(object state)
        {
            int numDisasters = Enum.GetValues(typeof(DisasterType)).Length;
            DisasterType randomType = (DisasterType)random.Next(numDisasters);
            GetDisaster(randomType);
        }

        public void UpdateCell(int x, int y, CellType cellType)
        {
            Cell oldCell = map.GetCell(x, y);
            CellFactory cellFactory = new CellFactory();
            Cell newCell = cellFactory.CreateCellWithParameter(cellType, oldCell, random);
        
            map.SetCell(newCell);
        }

        protected virtual void OnMapUpdated()
        {
            MapUpdated?.Invoke(this, EventArgs.Empty);
        }

        public List<Cell> GetChangedCells()
        {
            return map.GetAllChangedCells();
        }
    }
}