using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardFlip : MonoBehaviour
{
    public List<TilePosition> Tiles;
    private const float TIME = 0.1f;

    private void Awake()
    {
        LeanTween.init();
    }

    public void ActivateTiles()
    {
        foreach (var tilePosition in Tiles)
        {
            if (!tilePosition.tile)
            {
                return;
            }

            if (tilePosition.tile.tileCondition.tileType == TileType.Well)
            {
                tilePosition.tile.canMoveTile = false;
                continue;
            }

            tilePosition.tile.canMoveTile = true;
        }
    }

    public void Init()
    {
        foreach (var tilePosition in Tiles)
        {
            if (tilePosition.tile)
            {
                tilePosition.tile.EnableChild();
            }
        }
    }

    public IEnumerator StartBoardFlip()
    {
        DoAnimation(Tiles[0]);

        yield return new WaitForSeconds(TIME);

        DoAnimation(Tiles[3]);
        DoAnimation(Tiles[1]);

        yield return new WaitForSeconds(TIME);

        DoAnimation(Tiles[2]);
        DoAnimation(Tiles[4]);
        DoAnimation(Tiles[6]);

        yield return new WaitForSeconds(TIME);

        DoAnimation(Tiles[5]);
        DoAnimation(Tiles[7]);

        yield return new WaitForSeconds(TIME);

        DoAnimation(Tiles[8]);
        
        yield return new WaitForSeconds(TIME);
        ActivateTiles();
    }

    void DoAnimation(TilePosition tile)
    {
        tile.transform.LeanMoveLocalY(0.6f, TIME).setOnComplete(() => ResetYPosition(tile.transform));
        tile.transform.LeanRotateAround(new Vector3(1, 0, -1), 180f, TIME * 2f);
        AudioManager.Instance.PlayFlipSound();
    }

    void ResetYPosition(Transform trans)
    {
        trans.LeanMoveLocalY(0, TIME);
    }

    public void UpdateConditions()
    {
        bool allTilesHappy = true;
        int tileCount = 0;
        foreach (var tile in Tiles)
        {
            if (tile.tile)
            {
                tileCount++;
                tile.tile.tileCondition.CheckCondition();
                if (!tile.tile.tileCondition.tileIsHappy)
                {
                    allTilesHappy = false;
                }
            }
        }

        if (allTilesHappy && tileCount == 9)
        {
            foreach (var tilePosition in Tiles)
            {
                tilePosition.tile.canMoveTile = false;
            }

            StartCoroutine(BoardDone());
        }
    }

    IEnumerator BoardDone()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.PlayBoardDoneSound();
        yield return new WaitForSeconds(2.5f);
        BoardManager.Instance.SwapBoards();
    }
}