using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public int GridSize;
    public GameObject HexPrefab;
    public HexagonType[] Types;
    public List<Hexagon> Hexagons { get; private set; }
    private const float HEXAGON_SIZE = 1f;
    private const float Z_DISPLACEMENT = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Hexagons = new List<Hexagon>(GridSize * GridSize);
        CreateGrid();
        ConnectGrid();
    }

    private void CreateGrid()
    {
        for (int z = 0; z < GridSize; z++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                Hexagon hex = CreateHexagon(x, z);
                SetHexagonType(hex);
                hex.Index = Hexagons.Count;
                Hexagons.Add(hex);
            }
        }
    }

    private Hexagon CreateHexagon(int x, int z)
    {
        Transform hexTransform = Instantiate(HexPrefab).transform;
        hexTransform.parent = transform;
        hexTransform.position = ToWorldPosition(x, z);
        hexTransform.name = "hex-" + x + "-" + z;
        return hexTransform.GetComponent<Hexagon>();
 
    }

    private Vector3 ToWorldPosition(float x, float z)
    {
        float offset = 0f;
        if (OddRow(z))
        {
            offset = HEXAGON_SIZE / 2f;
        }
        float xCoord = x - offset;
        float zCoord = z * Z_DISPLACEMENT;

        return new Vector3(xCoord, 0, zCoord);
    }

    private bool OddRow(float z)
    {
        return z % 2 != 0;
    }

    private void SetHexagonType(Hexagon hex)
    {
        int random = UnityEngine.Random.Range(0, Types.Length);
        hex.HexType = Types[random];
    }

    private void ConnectGrid()
    {
        BruteForceConnect connector = new BruteForceConnect(Hexagons);
        connector.Connect();
    }
}
