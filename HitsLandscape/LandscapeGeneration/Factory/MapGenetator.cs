using System;
using System.Collections.Generic;
using System.Linq;
using HitsLandscape.NoiseGeneration;

namespace HitsLandscape.Factory
{
    static class MapGenerator
    {
        public static Map FillMap(NoiseType noiseType, Map map)
        {
            int sizeMap = map.Size;

            NoiseFactory noiseFactory = new NoiseFactory();
            CellFactory cellFactory = new CellFactory();
            Random random = new Random();
            
            INoiseGenerator noiseGenerator = noiseFactory.CreateNoise(noiseType);
            float[,] noise = noiseGenerator.GetNoise(map.Size);
            
            float minNoise = GetMinNoise(map, noise);
            float maxNoise = GetMaxNoise(map, noise);
            
            IEnumerable<Cell> cells = Enumerable.Range(0, sizeMap)
                .SelectMany(y => Enumerable.Range(0, sizeMap)
                    .Select(x =>
                    {
                        float elevation = noise[x, y];
                        return cellFactory.CreateCell(x, y, elevation, minNoise, maxNoise, random);
                    }));

            foreach (Cell cell in cells)
            {
                map.SetCell(cell);
            }

            return map;
        }
        
        private static float GetMaxNoise(Map map, float[,] noise)
        {
            int mapSize = map.GetMapSize();
            float maxNoise = Enumerable.Range(0, mapSize)
                .SelectMany(y => Enumerable.Range(0, mapSize).Select(x => noise[x, y]))
                .Max();
    
            return maxNoise;
        }

        private static float GetMinNoise(Map map, float[,] noise)
        {
            int mapSize = map.GetMapSize();
            float minNoise = Enumerable.Range(0, mapSize)
                .SelectMany(y => Enumerable.Range(0, mapSize).Select(x => noise[x, y]))
                .Min();
    
            return minNoise;
        }
    }
}