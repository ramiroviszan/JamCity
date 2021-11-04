using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public int GridSize;
    public GameObject HexPrefab;
    public HexagonType[] Types;
    private List<Hexagon> hexagons;
    private const float HEXAGON_SIZE = 1f;
    private const float Z_DISPLACEMENT = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        hexagons = new List<Hexagon>(GridSize * GridSize);
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int z = 0; z < GridSize; z++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                Hexagon hex = CreateHexagon(x, z);
                SetHexagonType(hex);
                hexagons.Add(hex);
            }
        }
    }

    private Hexagon CreateHexagon(int x, int z)
    {
        Transform hexTransform = Instantiate(HexPrefab).transform;
        hexTransform.parent = transform;
        hexTransform.position = ToWorldPosition(new Vector2(x, z));
        hexTransform.name = "hex-" + x + "-" + z;
        return hexTransform.GetComponent<Hexagon>();
 
    }

    private Vector3 ToWorldPosition(Vector2 gridPos)
    {
        float offset = 0f;
        if (gridPos.y % 2 != 0)
        {
            offset = HEXAGON_SIZE / 2f;
        }
        float x = gridPos.x - offset;
        float z = gridPos.y * Z_DISPLACEMENT;

        return new Vector3(x, 0, z);
    }


    private void SetHexagonType(Hexagon hex)
    {
        int random = UnityEngine.Random.Range(0, Types.Length);
        hex.HexType = Types[random];
    }


    #region TestHelpers
    public bool IsCountOfHexagonsEqualsTo(int equalsCount)
    {
        return equalsCount == hexagons.Count;
    }

    public bool ArePositionsEqualsTo(List<Vector3> positions)
    {
        bool valid;
        for (int i = 0; i < GridSize*GridSize; i++)
        {
            valid = hexagons[i].transform.position == positions[i];
            if(!valid)
            {
                return false;
            }
        }
        return true;
    }

    public bool TypeAtPositionEqualsTo(int pos, HexagonType type)
    {
        return hexagons[pos].HexType == type;
    }

    #endregion
}
