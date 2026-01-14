using System;
using System.Collections.Generic;
using UnityEngine;

public class TilePosition : MonoBehaviour
{
    public MoveTile tile;
    
    public List<Neighbours> neighbours = new List<Neighbours>();
    
    
    [Serializable]
    public struct Neighbours
    {
        public TilePosition neighbour;
        public Direction direction;
    }
}

[Flags]
public enum Direction
{
    Nothing,
    Cardinal,
    Diagonal
}
