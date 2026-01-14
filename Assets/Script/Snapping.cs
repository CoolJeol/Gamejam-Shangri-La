using System;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    public int boardIndex;
    private BoardFlip board;
    private void Awake()
    {
        board = GameObject.Find($"Board {boardIndex}").GetComponent<BoardFlip>();
    }

    void OnRelease()
    {
        GameObject closestTile = null;
        float closestDistance = float.MaxValue;
        foreach (var boardTile in board.Tiles)
        {
            var currentDistance = Vector3.Distance(boardTile.transform.position, transform.position);
            if ( currentDistance < closestDistance && currentDistance < 1f)
            {
                closestDistance = currentDistance;
                closestTile = boardTile.gameObject;
            }

            if (closestTile)
            {
                transform.SetParent(closestTile.transform);
                transform.position = Vector3.zero;
            }
        }
    }
}
