using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GridGenerationTest
{
    private GameObject gridObj;
    private GameObject prefab;
    // A Test behaves as an ordinary method

    [SetUp]
    public void SpawnGridGenerator()
    {
        gridObj = new GameObject("Grid");
        prefab = new GameObject("hex");
        prefab.AddComponent<Hexagon>();
    }

    [Test]
    public void Grid1x1()
    {
        GridGenerator generator = gridObj.AddComponent<GridGenerator>();
        generator.GridSize = 1;
        generator.HexPrefab = prefab;
        generator.Initialize();
        Assert.IsTrue(generator.CountHexagons(1));
    }

    [Test]
    public void Grid3x3()
    {
        GridGenerator generator = gridObj.AddComponent<GridGenerator>();
        generator.GridSize = 3;
        generator.HexPrefab = prefab;
        generator.Initialize();
        Assert.IsTrue(generator.CountHexagons(9));
        List<float[]> positions = new List<float[]>()
        {
            new float[]{0, 0},
            new float[]{1, 0},
            new float[]{2, 0},
            new float[]{0.5f, 0.75f},
            new float[]{1.5f, 0.75f},
            new float[]{2.5f, 0.75f},
            new float[]{0, 1.5f},
            new float[]{1, 1.5f},
            new float[]{2, 1.5f}
        };
        Assert.IsTrue(generator.ValidatePositions(positions));
    }



}
