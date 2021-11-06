using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class JamCityPathFinder : MonoBehaviour, IPathFinder
{
    public Hexagon[] FindPath(Hexagon start, Hexagon end)
    {
        try
        {
            return AStar.GetPath(start, end).Cast<Hexagon>().ToArray();
        } catch (ArgumentNullException e)
        {
            return new Hexagon[0];
        }
    }
}
