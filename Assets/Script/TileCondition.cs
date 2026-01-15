using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class TileCondition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Direction direction;
    public bool wantSomething;
    public TileType wantThisTileNextToIt;
    public CornerThing cornerType;
    public string description;
    public TileType tileType;
    public bool tileIsHappy;
    MoveTile moveTile;
    private Material Shader;
    

    public enum CornerThing
    {
        Nothing,
        WantCorner,
        DontWantCorner
    }

    private void Awake()
    {
        moveTile = GetComponent<MoveTile>();
        Shader = GetComponent<MeshRenderer>().materials[^1];
    }

    public virtual void CheckCondition()
    {
        if (tileType is TileType.Nothing or TileType.Well)
        {
            tileIsHappy = true;
            return;
        }

        if (moveTile.tilePosition.IsCorner && cornerType == CornerThing.DontWantCorner)
        {
            tileIsHappy = false;
            return;
        }

        if (!moveTile.tilePosition.IsCorner && cornerType == CornerThing.WantCorner)
        {
            tileIsHappy = false;
            return;
        }

       

        tileIsHappy = !wantSomething;

        foreach (var neighbour in moveTile.tilePosition.neighbours)
        {
            if (wantThisTileNextToIt != TileType.Nothing && neighbour.neighbour.tile.tileCondition.tileType == wantThisTileNextToIt)
            {
                tileIsHappy = true;
                return;
            }
            
            if (direction.HasFlag(neighbour.direction) &&
                neighbour.neighbour.tile && neighbour.neighbour.tile.tileCondition.tileType != TileType.Nothing)
            {
                if (wantSomething)
                {
                    tileIsHappy = true;
                    return;
                }
                else
                {
                    tileIsHappy = false;
                    return;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TileDescription.Instance.SetDescription(description, transform.GetChild(0).name);

        float Size;
        Size = Shader.GetFloat("_Size");
        Size = 0.01f;
        Shader.SetFloat("_Size", Size);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TileDescription.Instance.HideDescription();

        float Size;
        Size = Shader.GetFloat("_Size");
        Size = 0;
        Shader.SetFloat("_Size", Size);
    }
}

public enum TileType
{
    Nothing,
    Lamp,
    Pavilion,
    Stage,
    Well,
    Other
}