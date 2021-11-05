using PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour, IAStarNode
{
    public int Index;
    private HexagonType hexType;
    public HexagonType HexType {
        get {
            return hexType;
        } 
        set { 
            hexType = value;
            GetComponent<MeshRenderer>().material = hexType.Material;
        }
    }

    private List<Hexagon> neighbours = new List<Hexagon>(6);
    public IEnumerable<IAStarNode> Neighbours => neighbours;

    public float CostTo(IAStarNode neighbour)
    {
        return hexType.Cost;
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        return 0;
    }

    public void AddNeighbour(Hexagon hexagon)
    {
        neighbours.Add(hexagon);
    }
}
