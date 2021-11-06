using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AStarTest
{

    private GameObject gridObj;
    private GameObject hexPrefab;
    private GridGenerator generator;
    private HexagonType grass;
    private HexagonType mountain;
    private HexagonType water;
    private GameObject pathFinderObj;
    private IPathFinder pathFinder;


    [SetUp]
    public void SetUpGridGenerator()
    {
        gridObj = new GameObject("Grid");
        generator = gridObj.AddComponent<GridGenerator>();

        hexPrefab = Resources.Load<GameObject>("Hexagon");
        generator.HexPrefab = hexPrefab;
        
        grass = Resources.Load<HexagonType>("Grass");
        mountain = Resources.Load<HexagonType>("Mountain");
        water = Resources.Load<HexagonType>("Water");
        generator.Types = new HexagonType[3] { grass, mountain, water };
        
        gridObj.AddComponent<SmartHexagonConnector>();
        
        pathFinderObj = new GameObject("PathFinder");
        pathFinder = pathFinderObj.AddComponent<JamCityPathFinder>();

    }

    [Test]
    public void AStar3x3Test()
    {
        generator.GridSize = 3;
        Dictionary<int, HexagonType> hexagons = new Dictionary<int, HexagonType>()
        {
            { 0, grass},
            { 1, mountain},
            { 2, mountain},
            { 3, mountain},
            { 4, grass},
            { 5, grass},
            { 6, mountain},
            { 7, water},
            { 8, grass},
        };

        generator.InitializeWithDescriptor(hexagons);
        Hexagon[] path = pathFinder.FindPath(generator.Hexagons[0], generator.Hexagons[8]);

        Assert.AreEqual(0, path[0].Index);
        Assert.AreEqual(4, path[1].Index);
        Assert.AreEqual(5, path[2].Index);
        Assert.AreEqual(8, path[3].Index);
    }
}
