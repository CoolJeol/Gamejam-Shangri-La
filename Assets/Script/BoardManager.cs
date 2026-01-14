using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    public GameObject background;
    public List<BoardFlip> boards;
    public int currentBoardIndex;
    public Transform boardStartPosition;
    public Transform boardEndPosition;

    private Camera mainCamera;
    const float TIME = 0.1f;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    private IEnumerator Start()
    {
        foreach (var board in boards)
        {
            foreach (var tilePosition in board.Tiles)
            {
                tilePosition.tile.Init(board);
            }
        }
        yield return new WaitForSeconds(1);
        yield return BigBoardFlip();
        yield return new WaitForSeconds(0.7f);
        BoardFlyAway();
        yield return new WaitForSeconds(0.1f);
        ZoomCamera();
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(boards[currentBoardIndex].StartBoardFlip());
        boards[currentBoardIndex].Init();
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame && !LeanTween.isTweening())
        {
            boards[currentBoardIndex].transform.LeanRotateY( boards[currentBoardIndex].transform.localEulerAngles.y + 90, 0.2f);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && !LeanTween.isTweening())
        {
            boards[currentBoardIndex].transform.LeanRotateY( boards[currentBoardIndex].transform.localEulerAngles.y - 90, 0.2f);
        }
    }

    private void ZoomCamera()
    {
        LeanTween.value(gameObject, 2.7f, 1f, 0.5f)
            .setOnUpdate(val =>
            {
                mainCamera.orthographicSize = val;
            });

        LeanTween.scale(background, new Vector3(0.356f, 0.02225f, 0.2f) * 1.035f, 0.5f);
    }

    public void SwapBoards()
    {
        if (currentBoardIndex == boards.Count - 1)
        {
            SendBoardAway(boards[^1].transform);
            return;
        }

        SendBoardAway(boards[currentBoardIndex].transform);
        currentBoardIndex++;
        SendBoardIn(boards[currentBoardIndex].transform);
    }

    public void SendBoardAway(Transform board)
    {
        board.LeanMove(boardEndPosition.position, 2).setEaseOutBounce();
    }

    public void SendBoardIn(Transform board)
    {
        boards[currentBoardIndex].Init();
        board.position = boardStartPosition.position;
        board.LeanMove(Vector3.zero, 2).setEaseOutBounce()
            .setOnComplete(() => StartCoroutine(boards[currentBoardIndex].StartBoardFlip()));
    }

    IEnumerator BigBoardFlip()
    {
        DoAnimation(boards[1]);

        yield return new WaitForSeconds(TIME);

        DoAnimation(boards[4]);
        DoAnimation(boards[2]);

        yield return new WaitForSeconds(TIME);

        DoAnimation(boards[3]);
        DoAnimation(boards[0]);
        DoAnimation(boards[6]);

        yield return new WaitForSeconds(TIME);

        DoAnimation(boards[5]);
        DoAnimation(boards[7]);

        yield return new WaitForSeconds(TIME);

        DoAnimation(boards[8]);
    }

    void DoAnimation(BoardFlip tile)
    {
        AudioManager.Instance.PlayFlipSound();
        tile.transform.LeanMoveLocalY(2f, TIME).setOnComplete(() => ResetYPosition(tile.transform));
        tile.transform.LeanRotateAround(new Vector3(1, 0, -1), 180f, TIME * 2f);
    }

    void ResetYPosition(Transform trans)
    {
        trans.LeanMoveLocalY(0, TIME);
    }

    void BoardFlyAway()
    {
        boards[1].transform.LeanMove(new Vector3(-8, 0, -8), 0.2f);
        boards[2].transform.LeanMove(new Vector3(0, 0, -8), 0.2f);
        boards[3].transform.LeanMove(new Vector3(8, 0, -8), 0.2f);
        boards[4].transform.LeanMove(new Vector3(-8, 0, 0), 0.2f);
        boards[5].transform.LeanMove(new Vector3(8, 0, 0), 0.2f);
        boards[6].transform.LeanMove(new Vector3(-8, 0, 8), 0.2f);
        boards[7].transform.LeanMove(new Vector3(0, 0, 8), 0.2f);
        boards[8].transform.LeanMove(new Vector3(8, 0, 8), 0.2f);
    }
}