using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

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
    public Sprite sprite;


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
            if (wantThisTileNextToIt is not (TileType.Nothing or TileType.Well) &&
                neighbour.neighbour.tile && neighbour.neighbour.tile.tileCondition.tileType == wantThisTileNextToIt)
            {
                tileIsHappy = true;
                return;
            }

            if (direction.HasFlag(neighbour.direction) &&
                neighbour.neighbour.tile && neighbour.neighbour.tile.tileCondition.tileType is not (TileType.Nothing or TileType.Well))
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
        if (moveTile.board.boardIsHappy)
        {
            TileDescription.Instance.HideDescription();
            
            float Size2;
            var have2 = Shader.HasFloat("_Size");
            if (!have2)
                return;
            Size2 = 0;
            Shader.SetFloat("_Size", Size2);
            return;
        }

        TileDescription.Instance.SetDescription(description, transform.GetChild(0).name, sprite);

        float Size;
        var have = Shader.HasFloat("_Size");
        if (!have)
            return;
        Size = 1.04f;
        Shader.SetFloat("_Size", Size);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TileDescription.Instance.HideDescription();

        float Size;
        var have = Shader.HasFloat("_Size");
        if (!have)
            return;
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