using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JamCityPathFinder : MonoBehaviour, IPathFinder
{
    public Hexagon[] FindPath(Hexagon start, Hexagon end)
    {
        return AStar.GetPath(start, end).Cast<Hexagon>().ToArray();
    }
}
