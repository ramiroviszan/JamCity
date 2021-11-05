using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Camera))]
public class PathSelector : MonoBehaviour
{

    private Camera cam;
    private RaycastHit hitInformation;
    private PathFinder pathFinder;
    private Hexagon hexStart;
    private Hexagon hexEnd;
    private Hexagon[] path;
    private Renderer hexRenderer;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        pathFinder = GetComponent<PathFinder>();
    }

    private void Update()
    {
        Select();
    }

    private void Select()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray touchWorldPosition = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(touchWorldPosition, out hitInformation))
            {
                OnHexagonSelected();
            }
        }
    }

    private void OnHexagonSelected()
    {
        Hexagon hex = hitInformation.transform.GetComponent<Hexagon>();
        if (hex && hex.HexType.name != Hexagon.FORBIDDEN_TYPE)
        {
            if (hexStart == null)
            {
                hexStart = hex;
                SetStart();
            }
            else
            {
                hexEnd = hex;
                SetEnd();
            }
        }
            
    }

    private void SetStart()
    {
        ClearPreviousPath();
        PaintHex(hexStart, Color.green);
    }

    private void SetEnd()
    {
        PaintHex(hexEnd, Color.green);
        path = pathFinder.FindPath(hexStart, hexEnd);
        PaintPath();
        hexStart = null;
        hexEnd = null;
    }

    private void ClearPreviousPath()
    {
        if(path != null)
        {
            foreach (var hex in path)
            {
                PaintHex(hex, Color.white);
            }
            path = null;
        }
        
    }

    private void PaintPath()
    {
        foreach (var hex in SkipStartAndEnd())
        {
            PaintHex(hex, Color.red);
        }
    }

    private IEnumerable<Hexagon> SkipStartAndEnd()
    {
        return path.Skip(1).Take(path.Length - 2);
    }

    private void PaintHex(Hexagon target, Color color)
    {
        hexRenderer = target.GetComponent<Renderer>();
        if (hexRenderer)
        {
            hexRenderer.material.SetColor("_Color", color);
        }
    }

}
