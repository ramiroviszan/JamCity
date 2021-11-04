using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public int GridSize;
    public GameObject HexPrefab;
    private List<Transform> hexagons;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        hexagons = new List<Transform>(GridSize * GridSize);
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int z = 0; z < GridSize; z++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                CreateHexagon(x, z);
            }
        }
    }

    private void CreateHexagon(int x, int z)
    {
        Transform hexTransform = Instantiate(HexPrefab).transform;
        hexTransform.parent = transform;
        hexTransform.position = new Vector3(x, 0, z);
        hexTransform.name = "hex-" + x + "-" + z;
        hexagons.Add(hexTransform);
    }

    #region TestMethods
    public bool CountHexagons(int equalsCount)
    {
        return equalsCount == hexagons.Count;
    }

    public bool ValidatePositions(List<float[]> positions)
    {
        bool xValid;
        bool zValid;
        for (int i = 0; i < GridSize*GridSize; i++)
        {
            xValid = hexagons[i].position.x == positions[i][0];
            zValid = hexagons[i].position.z == positions[i][1];
            if(!xValid || !zValid)
            {
                return false;
            }
        }
        return true;
    }
    #endregion
}
