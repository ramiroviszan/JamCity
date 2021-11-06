using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteForceConnector : MonoBehaviour, IGridConnector
{
    private const float MAX_DISTANCE = 1.2f;

    public void ConnectGrid(List<Hexagon> hexagons)
    {
        float dist;
        foreach (var hex1 in hexagons)
        {
            foreach (var hex2 in hexagons)
            {
                if (hex1 != hex2)
                {
                    dist = Vector3.Distance(hex1.transform.position, hex2.transform.position);
                    if (dist <= MAX_DISTANCE)
                    {
                        hex1.AddNeighbour(hex2);
                    }
                }
            }
        }
    }

}
