using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

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
        Assert.AreEqual(1, generator.Hexagons.Count);
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
        Assert.IsTrue(ArePositionsEqualsTo(positions));
    }

    private  bool ArePositionsEqualsTo(List<Vector3> positions)
    {
        bool valid;
        for (int i = 0; i < generator.GridSize * generator.GridSize; i++)
        {
            valid = generator.Hexagons[i].transform.position == positions[i];
            if (!valid)
            {
                return false;
            }
        }
        return true;
    }

    [Test]
    public void Grid1x1Type()
    {
        generator.GridSize = 1;
        generator.Initialize();
        Assert.AreEqual(type, generator.Hexagons[0]);
    }

    [Test]
    public void GridConnection()
    {
        generator.GridSize = 3;
        generator.Initialize();
        Dictionary<int, int[]> connections = new Dictionary<int, int[]>()
        {
            {0, new int[3] { 1, 3, 4 } },
            {1, new int[4] { 0, 2, 4, 5} },
            {2, new int [2] {1, 5} },
            {3, new int [3] {0, 4, 6} },
            {4, new int [6] {0, 1, 3, 5, 6, 7} },
            {5, new int [5] {1, 2, 4, 7, 8} },
            {6, new int [3] {3, 4, 7} },
            {7, new int [4] {4, 5, 6, 8} },
            {8, new int [2] {5, 7} }
        };
        List<Hexagon> hexagons = generator.Hexagons;

        Assert.IsTrue(AreConnectionsEqualsTo(connections, hexagons));
    }

    private bool AreConnectionsEqualsTo(Dictionary<int, int[]> connections, List<Hexagon> hexagons)
    {
        bool isValid;
        foreach (var key in connections.Keys)
        {
            isValid = hexagons[key].Neighbours
                .Cast<Hexagon>()
                .Select(h => h.Index)
                .All(i => connections[key].Contains(i));

            if(!isValid)
            {
                return false;
            }
        }
        return true;
    }

}
