using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridGenerator : MonoBehaviour
{
    //Editor variables
    public int GridSize;
    public GameObject HexPrefab;
    public HexagonType[] Types;
    public float DelayFactor = 20f;

    //Public properties
    public List<Hexagon> Hexagons { get; private set; }

    //Private fields
    private IGridConnector connector;
    private Transform hexTransform;
    private HexagonSpawnAnimator animator;

    //Constants
    private const float HEXAGON_SIZE = 1f;
    private const float Z_DISPLACEMENT = 0.75f;

    private void Awake()
    {
        connector = GetComponent<IGridConnector>();
    }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Hexagons = new List<Hexagon>(GridSize * GridSize);
        CreateGrid();
        if(connector == null)
        {
            connector = GetComponent<IGridConnector>();
        }
        connector.ConnectGrid(Hexagons);
    }

    public void InitializeWithDescriptor(Dictionary<int, HexagonType> descriptor)
    {
        Hexagons = new List<Hexagon>(GridSize * GridSize);
        CreateGrid(descriptor);
        if (connector == null)
        {
            connector = GetComponent<IGridConnector>();
        }
        connector.ConnectGrid(Hexagons);
    }

    private void CreateGrid()
    {
        for (int z = 0; z < GridSize; z++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                Hexagon hex = CreateHexagon(x, z);
                hex.Index = Hexagons.Count;
                hex.XGrid = x;
                hex.ZGrid = z;
                SetRandomHexagonType(hex);
                Hexagons.Add(hex);
            }
        }
    }

    private void CreateGrid(Dictionary<int, HexagonType> descriptor)
    {
        int index = 0;
        for (int z = 0; z < GridSize; z++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                Hexagon hex = CreateHexagon(x, z);
                hex.Index = Hexagons.Count;
                hex.XGrid = x;
                hex.ZGrid = z;
                hex.HexType = descriptor[index];
                Hexagons.Add(hex);
                index++;
            }
        }
    }

    private Hexagon CreateHexagon(int x, int z)
    {
        hexTransform = Instantiate(HexPrefab).transform;
        hexTransform.name = "hex-" + x + "-" + z;
        hexTransform.parent = transform;
        animator = hexTransform.GetComponent<HexagonSpawnAnimator>();
        float startDelayInSecs = 1 + (GridSize * GridSize - Hexagons.Count) / DelayFactor;
        animator.SetDestination(ToWorldPosition(x, z), startDelayInSecs);
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

    private void SetRandomHexagonType(Hexagon hex)
    {
        int random = UnityEngine.Random.Range(0, Types.Length);
        hex.HexType = Types[random];
    }

}
