using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] BoardManager boardManager;
    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject retryUI;
    [SerializeField] GameObject wturnUI;
    [SerializeField] GameObject bturnUI;
    [SerializeField] TextMeshProUGUI winText;

    bool isBlackTurn = true;
    bool isEnded = false;

    private void Awake()
    {
        bturnUI.SetActive(true);
        finishUI.SetActive(false);
        retryUI.SetActive(false);
    }

    private void Update()
    {
        if (isEnded) return;

        if (CheckGameOver())
        {
            EndGame();
        }
    }

    bool CheckGameOver()
    {
        return boardManager.CheckWin();
    }

    void ChangeTurn()
    {
        isBlackTurn = !isBlackTurn;
        UpdateTurnUI();
    }

    void UpdateTurnUI()
    {
        if (isBlackTurn)
        {
            wturnUI.SetActive(false);
            bturnUI.SetActive(true);
        }
        else
        {
            wturnUI.SetActive(true);
            bturnUI.SetActive(false);
        }
    }

    void EndGame()
    {
        isEnded = true;

        string winner = isBlackTurn ? "Black" : "White";
        Debug.Log(winner + " wins!");
        finishUI.SetActive(true);
        winText.text = (winner + " wins!");
        retryUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnCellClicked(BoardCell cell)
    {
        if (cell.HasPiece())
        {
            Debug.Log("Cell is already occupied");
            return;
        }

        cell.PlacePiece(isBlackTurn);
        boardManager.MakeMove(cell.x, cell.y, isBlackTurn);

        if (CheckGameOver())
        {
            EndGame();
        }
        else
        {
            ChangeTurn();
        }
    }

    public void RestartGame()
    {
        isEnded = false;
        Time.timeScale = 1;
        boardManager.ResetBoard();
        finishUI.SetActive(false);
        retryUI.SetActive(false);
        isBlackTurn = true;
        UpdateTurnUI();
    }
}
