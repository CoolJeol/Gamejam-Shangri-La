using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileCondition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Direction direction;
    public bool wantSomething;
    public string description;
    public TileType tileType;
    public bool tileIsHappy;
    MoveTile moveTile;

    private void Awake()
    {
        moveTile = GetComponent<MoveTile>();
    }

    public virtual void CheckCondition()
    {
        if (tileType == TileType.Nothing)
        {
            tileIsHappy = true;
            return;
        }

        if (wantSomething)
        {
            foreach (var neighbour in moveTile.tilePosition.neighbours)
            {
                if (neighbour.direction == direction &&
                    neighbour.neighbour.tile && neighbour.neighbour.tile.tileCondition.tileType != TileType.Nothing)
                {
                    tileIsHappy = true;
                    return;
                }
            }
        }
        else
        {
            foreach (var neighbour in moveTile.tilePosition.neighbours)
            {
                if (neighbour.direction == direction &&
                    neighbour.neighbour.tile && neighbour.neighbour.tile.tileCondition.tileType == TileType.Nothing)
                {
                    tileIsHappy = true;
                    return;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TileDescription.Instance.SetDescription(description, transform.parent.name);
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
    Other
}