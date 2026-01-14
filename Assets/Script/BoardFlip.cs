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

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(StartBoardFlip());
        }

        IEnumerator StartBoardFlip()
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
        }
    }

    void DoAnimation(TilePosition tile)
    {
        tile.transform.LeanMoveLocalY(0.6f, TIME).setOnComplete(()=> ResetYPosition(tile.transform));
        tile.transform.LeanRotateAround(new Vector3(1, 0, -1), 180f, TIME * 2f);
    }

    void ResetYPosition(Transform trans)
    {
        trans.LeanMoveLocalY(0, TIME);
    }
}