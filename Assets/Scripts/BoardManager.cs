using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }
    public GameObject boardCellPrefab;
    public int boardSize = 15;
    public BoardCell[,] boardCells;
    private bool isBlackTurn = true;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        InitializeBoard();
    }

    void InitializeBoard()
    {
        boardCells = new BoardCell[boardSize, boardSize];

        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                GameObject cellObj = Instantiate(boardCellPrefab, new Vector2(x, y), Quaternion.identity);
                BoardCell cell = cellObj.GetComponent<BoardCell>();
                cell.x = x;
                cell.y = y;
                boardCells[x, y] = cell;
            }
        }
    }

    public void MakeMove(int x, int y, bool isBlack)
    {
        boardCells[x, y].PlacePiece(isBlack);
    }

    public bool CheckWin()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (boardCells[x, y].HasPiece() && IsWinningMove(x, y))
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool IsWinningMove(int x, int y)
    {
        int[] dx = { 1, 0, 1, 1 };
        int[] dy = { 0, 1, 1, -1 };
        bool isBlack = boardCells[x, y].IsBlack();

        for (int dir = 0; dir < 4; dir++)
        {
            int count = 1;
            for (int step = 1; step < 5; step++)
            {
                int nx = x + dx[dir] * step;
                int ny = y + dy[dir] * step;
                if (nx < 0 || nx >= boardSize || ny < 0 || ny >= boardSize) break;
                if (boardCells[nx, ny].HasPiece() && boardCells[nx, ny].IsBlack() == isBlack) count++;
                else break;
            }
            for (int step = 1; step < 5; step++)
            {
                int nx = x - dx[dir] * step;
                int ny = y - dy[dir] * step;
                if (nx < 0 || nx >= boardSize || ny < 0 || ny >= boardSize) break;
                if (boardCells[nx, ny].HasPiece() && boardCells[nx, ny].IsBlack() == isBlack) count++;
                else break;
            }
            if (count >= 5) return true;
        }
        return false;
    }

    public void ResetBoard()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                if (boardCells[x, y].HasPiece())
                {
                    Destroy(boardCells[x, y].currentPiece);
                    boardCells[x, y].currentPiece = null;
                }
            }
        }
    }
    public BoardCell GetCellAt(int x, int y)
    {
        if (x < 0 || x >= boardSize || y < 0 || y >= boardSize) return null;
        return boardCells[x, y];
    }
}
