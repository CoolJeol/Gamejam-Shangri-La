using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileCondition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Direction direction;
    public bool wantSomething;
    public CornerThing cornerType;
    public string description;
    public TileType tileType;
    public bool tileIsHappy;
    MoveTile moveTile;

    public enum CornerThing
    {
        Nothing,
        WantCorner,
        DontWantCorner
    }

    private void Awake()
    {
        moveTile = GetComponent<MoveTile>();
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TileDescription.Instance.HideDescription();
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