using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool followMouse = false;
    public LayerMask Groundlayer;
    bool didHit;
    Vector3 CameraTarget;
    public Transform Parent;
    public int boardIndex;
    private BoardFlip board;
    TilePosition tile;

    //obj pos - cam pos
    public void OnPointerDown(PointerEventData eventData)
    {
        Parent.SetParent(null);
        followMouse = true;
        if (tile)
        {
            tile.haveTile = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        followMouse = false;

        TilePosition closestTile = null;
        float closestDistance = float.MaxValue;
        foreach (var boardTile in board.Tiles)
        {
            var currentDistance = Vector3.Distance(boardTile.transform.position, Parent.transform.position);
            if (currentDistance < closestDistance && currentDistance < 1f)
            {
                closestDistance = currentDistance;
                closestTile = boardTile;
            }
        }

        if (closestTile && !closestTile.haveTile)
        {
            Parent.transform.position = closestTile.transform.position;
            transform.localPosition = Vector3.zero;
            tile.haveTile = true;
        }
    }

    private void Awake()
    {
        board = GameObject.Find($"Board {boardIndex}").GetComponent<BoardFlip>();
        tile = transform.parent.parent.GetComponent<TilePosition>();
    }

    void Update()
    {
        if (followMouse == true)
        {
            Vector2 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Groundlayer);
            Parent.transform.position = hit.point;

            transform.position = Parent.transform.position - Camera.main.transform.forward * 0.4f;

            //mousePosition = Input.mousePosition;
            //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
    }
}