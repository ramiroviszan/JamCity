using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridGenerationTest
{
    private GameObject gridObj;
    private GameObject prefab;
    private GridGenerator generator;
    private HexagonType type;


    [SetUp]
    public void SpawnGridGenerator()
    {
        gridObj = new GameObject("Grid");
        generator = gridObj.AddComponent<GridGenerator>();
        prefab = Resources.Load<GameObject>("Hexagon"); 
        generator.HexPrefab = prefab;
        type = Resources.Load<HexagonType>("Grass");
        generator.Types = new HexagonType[1] { type };
    }
    

    [Test]
    public void Grid1x1()
    {

        generator.GridSize = 1;
        generator.Initialize();
        Assert.IsTrue(generator.IsCountOfHexagonsEqualsTo(1));
    }

    [Test]
    public void Grid3x3()
    {
        generator.GridSize = 3;
        generator.Initialize();
        List<Vector3> positions = new List<Vector3>()
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(2, 0, 0),
            new Vector3(-0.5f, 0, 0.75f),
            new Vector3(0.5f, 0, 0.75f),
            new Vector3(1.5f, 0, 0.75f),
            new Vector3(0, 0, 1.5f),
            new Vector3(1, 0, 1.5f),
            new Vector3(2, 0, 1.5f)
        };
        Assert.IsTrue(generator.ArePositionsEqualsTo(positions));
    }

    [Test]
    public void Grid1x1Type()
    {
        generator.GridSize = 1;
        generator.Initialize();
        Assert.IsTrue(generator.TypeAtPositionEqualsTo(0, type));
    }

}
