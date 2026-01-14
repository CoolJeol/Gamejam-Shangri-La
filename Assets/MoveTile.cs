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

    public bool canMoveTile;

    Camera mainCamera;

    //obj pos - cam pos
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canMoveTile)
            return;

        Parent.SetParent(null);
        followMouse = true;
        if (tilePosition)
        {
            tilePosition.tile = null;
            tilePosition = null;
            AudioManager.Instance.PlayPickUpSound();
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
        tilePosition = transform.parent.parent.GetComponent<TilePosition>();
        tilePosition.tile = this;
        tileCondition = GetComponent<TileCondition>();
        mainCamera = Camera.main;
    }

    public void Init(BoardFlip boardFlip)
    {
        board = boardFlip;
    }

    void Update()
    {
        if (followMouse == true)
        {
            Vector2 pos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(pos);
            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, Groundlayer);
            Parent.transform.position = hit.point;

            transform.position = Parent.transform.position - mainCamera.transform.forward * 1.5f;
        }
    }
}