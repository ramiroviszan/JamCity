using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(IPathFinder))]
public class PathSelector : MonoBehaviour
{

    private Camera cam;
    private RaycastHit hitInformation;
    private IPathFinder pathFinder;
    private Hexagon hexStart;
    private Hexagon hexEnd;
    private Hexagon[] path;
    private Renderer hexRenderer;
    private Vector3 position;
    private const float MOVE_UP_AMOUNT = 0.2f;

    void Awake()
    {
        cam = GetComponent<Camera>();
        pathFinder = GetComponent<IPathFinder>();
    }

    void Update()
    {
        Select();
    }

    private void Select()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
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
        if (hex && hex.HexType.name != Constants.FORBIDDEN_TYPE)
        {
            if (hexStart == null)
            {
                hexStart = hex;
                SetStart();
            }
            else if(hex != hexStart)
            {
                hexEnd = hex;
                SetEnd();
            }
        }
            
    }

    private void SetStart()
    {
        ClearPreviousPath();
        MoveUp(hexStart);
        PaintHex(hexStart, Color.green);
    }

    private void SetEnd()
    {
        MoveUp(hexEnd);
        PaintHex(hexEnd, Color.green);
        path = pathFinder.FindPath(hexStart, hexEnd);
        if(path.Length > 0)
        {
            PaintPath();
            hexStart = null;
            hexEnd = null;
        } else
        {
            ClearStartAndEnd();
        }
    }

    private void ClearStartAndEnd()
    {
        PaintHex(hexStart, Color.white);
        PaintHex(hexEnd, Color.white);
        MoveDown(hexStart);
        MoveDown(hexEnd);
        hexStart = null;
        hexEnd = null;
    }

    private void ClearPreviousPath()
    {
        if(path != null)
        {
            foreach (var hex in path)
            {
                MoveDown(hex);
                PaintHex(hex, Color.white);
            }
            path = null;
        }
        
    }


    private void PaintPath()
    {
        foreach (var hex in SkipStartAndEnd())
        {
            MoveUp(hex);
            PaintHex(hex, Color.red);
        }
    }

    private void MoveUp(Hexagon hex)
    {
        position = hex.transform.position;
        position.y += MOVE_UP_AMOUNT;
        hex.transform.position = position;
    }

    private void MoveDown(Hexagon hex)
    {
        position = hex.transform.position;
        position.y = 0f;
        hex.transform.position = position;
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
