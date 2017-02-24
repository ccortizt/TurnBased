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
            Debug.LogError("player 1 set");
            gameObject.name = "Player 1";
        }
        else
        {
            Debug.LogError("player 2 set");
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
                        Debug.LogError("current: " + currentPosX + " " + currentPosY);
                    }
                    else
                    {
                        transform.position = transform.position + transform.up;
                        
                        if (IsCurrentPosition(transform.position.x, transform.position.y))
                        {
                            
                            ResetCanMoves();
                            Debug.LogError("regreso u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);
                        }

                        else
                        {
                            
                            canMoveUp = false;
                            canMoveDown = true;
                            canMoveLeft = false;
                            canMoveRight = false;
                            Debug.LogError("no regreso u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);

                        }
                    }

                }

                if (Input.GetKeyUp(KeyCode.DownArrow))
                {

                    if (!CanMoveToPosition(transform.position.x, transform.position.y, "down", transform.position.x, transform.position.y - 1))
                    {
                        CantMoveThere(transform.position.x, transform.position.y - 1);
                        Debug.LogError("current: " + currentPosX + " " + currentPosY);
                    }
                    else
                    {
                        transform.position = transform.position - transform.up;
                        
                        if (IsCurrentPosition(transform.position.x, transform.position.y))
                        {
                        
                            ResetCanMoves();
                            Debug.LogError("regreso u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);
                        }
                        else
                        {
                            
                            canMoveUp = true;
                            canMoveDown = false;
                            canMoveLeft = false;
                            canMoveRight = false;
                            Debug.LogError("no regreso u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);
                           
                        }
                    }
                }


                if (Input.GetKeyUp(KeyCode.RightArrow))
                {

                    if (!CanMoveToPosition(transform.position.x, transform.position.y, "right", transform.position.x + 1, transform.position.y))
                    {
                        
                        CantMoveThere(transform.position.x + 1, transform.position.y);
                        Debug.LogError("current: " + currentPosX + " " + currentPosY);
                    }
                    else
                    {                        

                        transform.position = transform.position + transform.right;
                        

                        if (IsCurrentPosition(transform.position.x, transform.position.y))
                        {
                            
                            ResetCanMoves();
                            Debug.LogError("regreso u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);
                        }
                        else
                        {
                            
                            canMoveUp = false;
                            canMoveDown = false;
                            canMoveLeft = true;
                            canMoveRight = false;

                            Debug.LogError("no regreso u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);
                        }
                    }
                }

                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {

                    if (!CanMoveToPosition(transform.position.x, transform.position.y, "left", transform.position.x - 1, transform.position.y))
                    {
                       
                        CantMoveThere(transform.position.x - 1, transform.position.y);
                        Debug.LogError("current: " + currentPosX + " " + currentPosY);
                    }
                    else
                    {
                        
                        transform.position = transform.position - transform.right;
                        
                        if (IsCurrentPosition(transform.position.x, transform.position.y))
                        {
                            
                            ResetCanMoves();
                            Debug.LogError("regreso u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);
                        }
                        else
                        {
                            
                            canMoveUp = false;
                            canMoveDown = false;
                            canMoveLeft = false;
                            canMoveRight = true;
                            Debug.LogError("no regreso u " + canMoveUp + " d " + canMoveDown + " r " + canMoveRight + " l " + canMoveLeft);
                            
                        }
                    }
                }


                if (Input.GetKeyUp(KeyCode.Space))
                {

                    Debug.LogError(gameObject.name + " pushed!");
                    CmdPutObject();

                }
            }
        }

    }

    public void CantMoveThere(float x, float y)
    {
        Debug.LogError("CANT MOVE THERE " + x + " " + y);
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
        Debug.LogError("CurrentUpdate " + currentPosX + " " + currentPosY);
        
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
        Debug.LogError("Current " + currentPosX + " " + currentPosY);
        Debug.LogError("Next " + xPosTranformed(x) + " " + yPosTranformed(y));
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
            return canMoveUp;
        }

        if (dir.Equals("down"))
        {
            return canMoveDown;
        }

        if (dir.Equals("right"))
        {
            return canMoveRight;
        }

        if (dir.Equals("left"))
        {
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
                
        Debug.LogError("before reset turn data");
        movesLeft -= 1;
        ResetCanMoves();
        Debug.LogError("before update");
        UpdateCurrentPos();
        Debug.LogError("After update");
        gameBoardManager.GetComponent<GameBoardController>().FillBlock(currentPosX, currentPosY);
        //other cases here;
        NetworkServer.Spawn(p1);
        if (movesLeft == 0)
        {
            movesLeft += 1;
            Debug.Log("SWITCHING " + gameObject.name);
            ////gameBoardManager.GetComponent<TurnController>().SwitchTurn();
            try
            {
                if(isServer){
                    gameBoardManager.GetComponent<TurnController>().RpcTogglePlayer();
                    Debug.LogError("RPCTOGGLE");
                }

                else
                {
                    gameBoardManager.GetComponent<TurnController>().CmdTogglePlayer();
                    Debug.LogError("CMDTOGGLE");
                }
                    
            }
            catch (System.Exception e)
            {

            }
            

        }

    }

   
}



