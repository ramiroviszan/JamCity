using PathFinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour, IAStarNode
{

    //Public properties
    public int Index { get; set; }

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

    public int XGrid { get; set; }
    public int ZGrid { get; set; }

    public float CostTo(IAStarNode neighbour)
    {
        return hexType.Cost;
    }

    public float EstimatedCostTo(IAStarNode target)
    {
        return Vector3.Distance(transform.position, ((Hexagon)target).transform.position);
    }

    public void AddNeighbour(Hexagon hexagon)
    {
        if (hexagon.HexType.name != Constants.FORBIDDEN_TYPE)
        {
            neighbours.Add(hexagon);
        }
    }
}
