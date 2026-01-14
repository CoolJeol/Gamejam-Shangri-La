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
    public TilePosition tilePosition;
    public TileCondition tileCondition;

    Camera mainCamera;
    
    //obj pos - cam pos
    public void OnPointerDown(PointerEventData eventData)
    {
        Parent.SetParent(null);
        followMouse = true;
        if (tilePosition)
        {
            tilePosition.tile = null;
            tilePosition = null;
        }

        tileCondition.tileIsHappy = false;
        board.UpdateConditions();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        followMouse = false;

        TilePosition closestTile = null;
        float closestDistance = float.MaxValue;
        foreach (var boardTile in board.Tiles)
        {
            var currentDistance = Vector3.Distance(boardTile.transform.position, Parent.transform.position);
            if (currentDistance < closestDistance && currentDistance < 0.45f)
            {
                closestDistance = currentDistance;
                closestTile = boardTile;
            }
        }

        if (closestTile && !closestTile.tile)
        {
            Parent.transform.position = closestTile.transform.position;
            transform.localPosition = Vector3.zero;
            tilePosition = closestTile;
            tilePosition.tile = this;
            Parent.transform.SetParent(tilePosition.transform);
            AudioManager.Instance.PlayPlaceSound();
           board.UpdateConditions();
        }
    }

    private void Awake()
    {
        board = GameObject.Find($"Board {boardIndex}").GetComponent<BoardFlip>();
        tilePosition = transform.parent.parent.GetComponent<TilePosition>();
        tilePosition.tile = this;
        tileCondition = GetComponent<TileCondition>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (followMouse == true)
        {
            Vector2 pos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(pos);
            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Groundlayer);
            Parent.transform.position = hit.point;

            transform.position = Parent.transform.position - mainCamera.transform.forward * 0.4f;

            //mousePosition = Input.mousePosition;
            //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }
    }
}