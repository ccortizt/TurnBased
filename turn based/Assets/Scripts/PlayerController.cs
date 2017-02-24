using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    private int currentPosX;
    private int currentPosY;

    private int movesLeft;

    private bool canMoveLeft;
    private bool canMoveRight;
    private bool canMoveUp;
    private bool canMoveDown;

    GameObject gameBoardManager;

    void Start()
    {

        gameBoardManager = GameObject.Find("GameManager");

        movesLeft = 1;

        ResetCanMoves();

        CmdPutObject();

        if (GameObject.Find("Player 1") == null)
        {
            gameObject.name = "Player 1";
        }
        else
        {
            gameObject.name = "Player 2";
        }
        GameObject.Find("GameManager").GetComponent<TurnController>().AddPlayer(gameObject);

        if (isLocalPlayer)
            UpdateCurrentPos();

    }

    void Update()
    {
        if (movesLeft > 0)
        {
            if (isLocalPlayer)
            {

                if (Input.GetKeyUp(KeyCode.UpArrow))
                {

                    if (!CanMoveToPosition(transform.position.x, transform.position.y, "up", transform.position.x, transform.position.y + 1))
                    {
                        CantMoveThere(transform.position.x, transform.position.y + 1);
                    }
                    else
                    {
                        transform.position = transform.position + transform.up;
                        
                        if (IsCurrentPosition(transform.position.x, transform.position.y))
                        {
                            
                            ResetCanMoves();
                        }

                        else
                        {                            
                            canMoveUp = false;
                            canMoveDown = true;
                            canMoveLeft = false;
                            canMoveRight = false;
                           

                        }
                    }

                }

                if (Input.GetKeyUp(KeyCode.DownArrow))
                {

                    if (!CanMoveToPosition(transform.position.x, transform.position.y, "down", transform.position.x, transform.position.y - 1))
                    {
                        CantMoveThere(transform.position.x, transform.position.y - 1);
                    }
                    else
                    {
                        transform.position = transform.position - transform.up;
                        
                        if (IsCurrentPosition(transform.position.x, transform.position.y))
                        {
                        
                            ResetCanMoves();
                        }
                        else
                        {
                            
                            canMoveUp = true;
                            canMoveDown = false;
                            canMoveLeft = false;
                            canMoveRight = false;

                           
                        }
                    }
                }


                if (Input.GetKeyUp(KeyCode.RightArrow))
                {

                    if (!CanMoveToPosition(transform.position.x, transform.position.y, "right", transform.position.x + 1, transform.position.y))
                    {
                        CantMoveThere(transform.position.x + 1, transform.position.y);
                    }
                    else
                    {
                        

                        transform.position = transform.position + transform.right;
                        

                        if (IsCurrentPosition(transform.position.x, transform.position.y))
                        {
                            
                            ResetCanMoves();
                        }
                        else
                        {
                            
                            canMoveUp = false;
                            canMoveDown = false;
                            canMoveLeft = true;
                            canMoveRight = false;

                            
                        }
                    }
                }

                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {

                    if (!CanMoveToPosition(transform.position.x, transform.position.y, "left", transform.position.x - 1, transform.position.y))
                    {
                       
                        CantMoveThere(transform.position.x - 1, transform.position.y);
                    }
                    else
                    {
                        
                        transform.position = transform.position - transform.right;
                        
                        if (IsCurrentPosition(transform.position.x, transform.position.y))
                        {
                            
                            ResetCanMoves();
                        }
                        else
                        {
                            
                            canMoveUp = false;
                            canMoveDown = false;
                            canMoveLeft = false;
                            canMoveRight = true;

                            
                        }
                    }
                }


                if (Input.GetKeyUp(KeyCode.Space))
                {

                    Debug.Log(gameObject.name + " pushed!");
                    CmdPutObject();

                }
            }
        }

    }

    public void CantMoveThere(float x, float y)
    {
        Debug.Log("CANT MOVE THERE " + x + " " + y);
    }

    private void ResetCanMoves()
    {
        if (isLocalPlayer)
            canMoveLeft = canMoveRight = canMoveUp = canMoveDown = true;
    }


    private void UpdateCurrentPos()
    {
        currentPosX = xPosTranformed(transform.position.x);
        currentPosY = yPosTranformed(transform.position.y);
        
    }

    private int xPosTranformed(float n)
    {
        return (int)n + 5;
    }

    private int yPosTranformed(float n)
    {
        Debug.Log(n + " "+( -n + 5));
        return (int)-n + 5;
    }

    private bool IsCurrentPosition(float x, float y)
    {
        Debug.Log("Current " + currentPosX + " " + currentPosY);
        Debug.Log("Next " + xPosTranformed(x) + " " + yPosTranformed(y));
        return (xPosTranformed(x) == currentPosX && yPosTranformed(y) == currentPosY);
    }

    private bool IsInEdge(float x, float y, string dir)
    {

        if (dir.Equals("up"))
        {
            if (y == 5)
            {
                Debug.LogError("is in upper edge");
                return true;
            }
            else
                return false;
        }
        else if (dir.Equals("down"))
        {
            if (y == -5)
            {
                Debug.LogError("is in bottom edge");
                return true;
            }
            else
                return false;
        }
        else if (dir.Equals("right"))
        {
            if (x == 5)
            {
                Debug.LogError("is in right edge");
                return true;
            }
            else
                return false;
        }

        else if (dir.Equals("left"))
        {
            if (x == -5)
            {
                Debug.LogError("is in left edge");
                return true;
            }
            else
                return false;
        }
        else
        {
            Debug.LogError("INVALID DIR ERROR");
            return true;
        }

    }

    public bool IsBlockFilled(float x, float y)
    {
        return gameBoardManager.GetComponent<GameBoardController>().IsBlockFilled(xPosTranformed(x), yPosTranformed(y));
    }

    public bool CanMoveDir(string dir)
    {
        if (dir.Equals("up"))
        {
            if (!canMoveUp)
                Debug.LogError("Cant move up twice");
            return canMoveUp;
        }

        if (dir.Equals("down"))
        {
            if (!canMoveDown)
                Debug.LogError("Cant move down twice");
            return canMoveDown;
        }

        if (dir.Equals("right"))
        {
            if (!canMoveRight)
                Debug.LogError("Cant move right twice");
            return canMoveRight;
        }

        if (dir.Equals("left"))
        {
            if (!canMoveLeft)
                Debug.LogError("Cant move left twice");
            return canMoveLeft;
        }

        else
        {
            Debug.LogError("INVALID MOVE DIR CHECK");
            return true;
        }

    }

    public bool CanMoveToPosition(float x, float y, string dir, float x1, float y1)
    {
        if (IsInEdge(x, y, dir))
        {
            Debug.LogError("ESTA AL BORDE");
            return false;
        }

        if (!CanMoveDir(dir))
        {
            Debug.LogError("DIRECCION NO HABILITADA");
            Debug.Log(" u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);
            return false;
        }

        if (IsBlockFilled(x1, y1) && !IsCurrentPosition(x1, y1))
        {
            Debug.LogError("POcIsION OCUPADA Y NO INICIAL");
            return false;
        }
        else
        {
            return true;
        }

    }

    

    [Command]
    void CmdPutObject()
    {

        var p1 = (GameObject)Instantiate(Resources.Load("Prefabs/Cube1"), new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.identity);

        NetworkServer.Spawn(p1);
        movesLeft -= 1;
        ResetCanMoves();
        UpdateCurrentPos();
        gameBoardManager.GetComponent<GameBoardController>().FillBlock(currentPosX, currentPosY);
        //other cases here;

        if (movesLeft == 0)
        {
            movesLeft += 1;
            //Debug.Log("SWITCHING " + gameObject.name);
            ////gameBoardManager.GetComponent<TurnController>().SwitchTurn();
            //gameBoardManager.GetComponent<TurnController>().TogglePlayer();

        }

    }

}



