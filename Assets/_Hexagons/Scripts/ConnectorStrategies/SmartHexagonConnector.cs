using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartHexagonConnector : MonoBehaviour, IGridConnector
{
    private List<Hexagon> hexagons;
    private int gridSize;
    private Hexagon currentHex;

    public void ConnectGrid(List<Hexagon> allHexagons)
    {
        hexagons = allHexagons;
        gridSize = (int)Mathf.Sqrt(hexagons.Count);
        foreach (var hex in hexagons)
        {
            currentHex = hex;
            ConnectHexagon();
        }
    }

    private void ConnectHexagon()
    {
        int x = currentHex.XGrid;
        int z = currentHex.ZGrid;

        ConnectLeft(x, z);
        ConnectRight(x, z);

        if (IsOddRow(z))
        {
            ConnectOddBottomLeft(x, z);
            ConnectOddBottomRight(x, z);
            ConnectOddTopLeft(x, z);
            ConnectOddTopRight(x, z);
        }
        else
        {
            ConnectEvenBottomLeft(x, z);
            ConnectEvenBottomRight(x, z);
            ConnectEvenTopLeft(x, z);
            ConnectEvenTopRight(x, z);
        }
    }

    private static bool IsOddRow(int z)
    {
        return z % 2 != 0;
    }

    private void ConnectLeft(int x, int z)
    {
        if (x > 0)
            currentHex.AddNeighbour(hexagons[x - 1 + z * gridSize]);
    }

    private void ConnectRight(int x, int z)
    {
        if (x < gridSize - 1) 
            currentHex.AddNeighbour(hexagons[x + 1 + z * gridSize]);
    }

    private void ConnectOddBottomLeft(int x, int z)
    {
        if (x > 0 && z > 0) 
            currentHex.AddNeighbour(hexagons[(x - 1) + (z - 1) * gridSize]);
    }

    private void ConnectOddBottomRight(int x, int z)
    {
        if (z > 0) 
            currentHex.AddNeighbour(hexagons[x + (z - 1) * gridSize]);
    }

    private void ConnectOddTopLeft(int x, int z)
    {
        if (x > 0 && z < gridSize - 1) 
            currentHex.AddNeighbour(hexagons[(x - 1) + (z + 1) * gridSize]);
    }

    private void ConnectOddTopRight(int x, int z)
    {
        if (z < gridSize - 1) 
            currentHex.AddNeighbour(hexagons[x + (z + 1) * gridSize]);
    }
    private void ConnectEvenBottomLeft(int x, int z)
    {
        if (z > 0) currentHex.AddNeighbour(hexagons[x + (z - 1) * gridSize]);
    }

    private void ConnectEvenBottomRight(int x, int z)
    {
        if (x < gridSize - 1 && z > 0) 
            currentHex.AddNeighbour(hexagons[(x + 1) + (z - 1) * gridSize]);
    }

    private void ConnectEvenTopLeft(int x, int z)
    {
        if (z < gridSize - 1) 
            currentHex.AddNeighbour(hexagons[x + (z + 1) * gridSize]);
    }

    private void ConnectEvenTopRight(int x, int z)
    {
        if (x < gridSize - 1 && z < gridSize - 1) 
            currentHex.AddNeighbour(hexagons[(x + 1) + (z + 1) * gridSize]);
    }

}
