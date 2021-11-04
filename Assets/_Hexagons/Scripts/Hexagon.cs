using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
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
}
