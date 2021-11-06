using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartHexagonConnector : MonoBehaviour, IGridConnector
{

    public void ConnectGrid(List<Hexagon> hexagons)
    {
        foreach (var hex in hexagons)
        {
            ConnectHexagon(hex, hexagons);
        }
    }

    private void ConnectHexagon(Hexagon hex, List<Hexagon> hexagons)
    {
        int x = hex.XGrid;
        int z = hex.ZGrid;
        int gridSize = (int) Mathf.Sqrt(hexagons.Count);

        // Left
        if (x > 0) hex.AddNeighbour(hexagons[x - 1 + z * gridSize]);

        // Right 
        if (x < gridSize - 1) hex.AddNeighbour(hexagons[x + 1 + z * gridSize]);

        if (z % 2 == 0)
        {
            // Bottom Left
            if (z > 0) hex.AddNeighbour(hexagons[x + (z - 1) * gridSize]);

            // Bottom Right 
            if (x < gridSize - 1 && z > 0) hex.AddNeighbour(hexagons[(x + 1) + (z - 1) * gridSize]);

            // Top Left 
            if (z < gridSize - 1) hex.AddNeighbour(hexagons[x  + (z + 1) * gridSize]);

            // Top Right 
            if (x < gridSize -1 && z < gridSize - 1) hex.AddNeighbour(hexagons[(x + 1) + (z + 1) * gridSize]);

         
        }
        else
        {
            // Bottom Left 
            if (x > 0 && z > 0) hex.AddNeighbour(hexagons[(x - 1) + (z - 1) * gridSize]);

            // Bottom Right
            if (z > 0) hex.AddNeighbour(hexagons[x + (z - 1) * gridSize]);

            // Top Left  
            if (x > 0 && z < gridSize - 1) hex.AddNeighbour(hexagons[(x - 1) + (z + 1) * gridSize]);

            // Top Right 
            if (z < gridSize - 1) hex.AddNeighbour(hexagons[x  + (z + 1) * gridSize]);
        }
    }
}
