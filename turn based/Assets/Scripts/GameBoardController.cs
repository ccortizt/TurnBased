using UnityEngine;
using System.Collections;


public class GameBoardController: MonoBehaviour{

    enum GridBlock{ empty, filled, speed, locked, warp };
    private System.Enum[,] gameBoard;
    public int size;

    public int numWarps;
    public int numLocks;
    public int numSpeeds;

    void Start()
    {
        size = 11;
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        SetBoardSize(size);
        ClearBoard();
        SetWarpBlocks(); //2spaces
        SetLockedBlocks(); //8Spaces
        SetSpeedBlocks(); //10Spaces        
        //-5,0  --  5,0 -- 0,-5 -- 0,5    
    }

    private void SetBoardSize(int size)
    {
        gameBoard = new System.Enum[size, size];
    }

    private void ClearBoard()
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++) 
                gameBoard[i, j] = GridBlock.empty;
    }

    private void SetWarpBlocks()
    {
        int i = 0;
        while ( i < numWarps)
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);
            if (IsBlockEmpty(x, y))
            {
                gameBoard[x, y] = GridBlock.warp;
                i++;
            }
        }
        
    }

    private void SetLockedBlocks()
    {
        int i = 0;
        while (i < numLocks)
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);
            if (IsBlockEmpty(x, y))
            {
                gameBoard[x, y] = GridBlock.locked;
                i++;
            }
        }
    }

    private void SetSpeedBlocks()
    {
        int i = 0;
        while (i < numSpeeds)
        {
            int x = Random.Range(0, size);
            int y = Random.Range(0, size);
            if (IsBlockEmpty(x,y))
            {
                gameBoard[x, y] = GridBlock.speed;
                i++;
            }
        }
    }

    public void FillBlock(int x, int y)
    {
        gameBoard[x, y] = GridBlock.filled;
    }

    
    public bool IsBlockEmpty(int x, int y){
        return gameBoard[x, y].Equals(GridBlock.empty);
    }

    public bool IsBlockFilled(int x, int y)
    {
        if (x < 0 || x > size - 1 || y < 0 || y > size - 1)
            return true;

        return gameBoard[x, y].Equals(GridBlock.filled);
    }

    public bool IsBlockSpeed(int x, int y)
    {
        return gameBoard[x, y].Equals(GridBlock.speed);
    }

    public bool IsBlockLocked(int x, int y)
    {
        return gameBoard[x, y].Equals(GridBlock.locked);
    }

}