using System;
using HitsLandscape.Disaster;
using HitsLandscape.NoiseGeneration;

namespace HitsLandscape;

static class Translator
{
    public static NoiseType GetNoiseTypeFromString(string noise) => noise switch
    {
        "Perlin" => NoiseType.Perlin,
        "Crater" => NoiseType.Crater,
        "Diamond Square" => NoiseType.DiamondSquare,
        "Random" => NoiseType.Random,
        _ => NoiseType.River
    };

    public static CellType GetSelectedCellType(String selectedBlock) => selectedBlock switch
    {
        "Grass" => CellType.Grass,
        "Water" => CellType.Water,
        "Flower" => CellType.Flower,
        "Tree" => CellType.Tree,
        "Snow" => CellType.Snow,
        "Sand" => CellType.Sand,
        "Dirt" => CellType.Dirt,
        "Fire" => CellType.Fire,
        _ => CellType.Stone
    };
    
    public static DisasterType GetSelectedDisasterType(String selectedDisaster) => selectedDisaster switch
    {
        "Drought" => DisasterType.Drought,
        "Flood" => DisasterType.Flood,
        _ => DisasterType.Wildfire
    };
}