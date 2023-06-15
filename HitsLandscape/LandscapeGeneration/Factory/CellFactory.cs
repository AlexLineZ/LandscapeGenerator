using System;
using HitsLandscape.Material;
using HitsLandscape.Material.StaticMaterial;

namespace HitsLandscape
{
    public class CellFactory
    {
        private const int NumberOfLayers = 7;
        public Cell CreateCell(int x, int y, float elevation, float minElevation, float maxElevation, Random random)
        {
            IProperty property = CreateProperty(minElevation, maxElevation, elevation, random);
            Cell newCell = new Cell(x, y, elevation, property.GetPossible(), property);
            return newCell;
        }
        
        private IProperty CreateProperty(float minElevation, float maxElevation, float nowElevation, Random random)
        {
            float elevationRange = maxElevation - minElevation;
            float intervalSize = elevationRange / NumberOfLayers;
            int intervalIndex = (int)((nowElevation - minElevation) / intervalSize);
            
            switch (intervalIndex)
            {
                case 0:
                    return CreateNullLayer(random);
                case 1:
                    return CreateFirstLayer(random);
                case 2:
                    return CreateSecondLayer(random);
                case 3:
                    return CreateThirdLayer(random);
                case 4:
                    return CreateFourthLayer(random);
                case 5:
                    return CreateFifthLayer(random);
                case 6:
                    return CreateSixthLayer(random);
                default:
                    return new Stone();
            }
        }

        private IProperty CreateNullLayer(Random random)
        {
            return new Water();
        }
        
        private IProperty CreateFirstLayer(Random random)
        {
            int blockType = random.Next(1, 50);
            switch (blockType)
            {
                case 1:
                    return new Sand();
                default:
                    return new Water();
            }
        }
        
        private IProperty CreateSecondLayer(Random random)
        {
            return new Sand();
        }
        
        private IProperty CreateThirdLayer(Random random)
        {
            int plantType = random.Next(1, 9);
            switch (plantType)
            {
                case 1:
                    return new Flower(random);
                case 2:
                    return new Grass(random);
                case 3:
                    return new Tree();
                default:
                    return new Dirt();
            }
        }

        private IProperty CreateFourthLayer(Random random)
        {
            int dirtType = random.Next(1, 5);
            switch (dirtType)
            {
                case 1:
                    return new Grass(random);
                case 2:
                    return new Flower(random);
                case 3:
                    return new Tree();
                default:
                    return new Dirt();
            }
        }
        
        private IProperty CreateFifthLayer(Random random)
        {
            int rockType = random.Next(1, 3001);
            switch (rockType)
            {
                case 1:
                    return new Water();
                default:
                    return new Stone();
            }
        }
        
        private IProperty CreateSixthLayer(Random random)
        {
            int topType = random.Next(1, 4);
            switch (topType)
            {
                case 1:
                    return new Stone();
                default:
                    return new Snow();
            }
        }
        
        public Cell CreateCellWithParameter(CellType cellType, Cell oldCell, Random random)
        {
            int x = oldCell.GetX();
            int y = oldCell.GetY();
            float elevation = oldCell.GetElevation();

            IProperty property = GetPropertyWithParameter(cellType, random);
            bool isPassable = property.GetPossible();
            
            Cell newCell = new Cell(x, y, elevation, isPassable, property);

            return newCell;
        }
        
        private IProperty GetPropertyWithParameter(CellType cellType, Random random) => cellType switch
        {
            CellType.Grass => new Grass(random),
            CellType.Flower => new Flower(random),
            CellType.Water => new Water(),
            CellType.Tree => new Tree(),
            CellType.Snow => new Snow(),
            CellType.Sand => new Sand(),
            CellType.Dirt => new Dirt(),
            CellType.Fire => new Fire(),
            _ => new Stone()
        };
    }
}