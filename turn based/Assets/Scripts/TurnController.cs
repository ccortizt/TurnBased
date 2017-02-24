using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TurnController : NetworkBehaviour
{

    int numPlayers;

    private float timer;
    public int maxTime;

    public Text timerText;
    public Text player1text;
    public Text player2text;

    void Start()
    {
       
        timerText.text = "player1 turn";
    }


    void Update()
    {
        //if(numPlayers >= 2){
        //    if (isServer)
        //        RpcPlayerState();
        //    else
        //        CmdPlayerState();
        //}
        
       
        //if (numPlayers == 2)
        //{
        //    if (timer > 0)
        //    {
        //        timer -= Time.deltaTime;                
        //    }
        //    else
        //    {
        //        SwitchTurn();
        //        Debug.Log("SWITCHTIME");
        //    }

        //    timerText.text = timer.ToString();
        //}

    }

    public void AddPlayer(GameObject player)
    {
        numPlayers++;      

        if (numPlayers == 2)
        {            
            GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = false; 
        }

    }

    [ClientRpc]
    public void RpcTogglePlayer()
    {
        Debug.Log("intoggle");

        if (GameObject.Find("Player 1").GetComponent<PlayerController>().enabled)
        {
            Debug.LogError("termina player1");
            GameObject.Find("Player 1").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = true;
            timerText.text = "player2 turn";
            Debug.LogError("Toggled");
        }
        else 
        {
            Debug.LogError("termina player2");
            GameObject.Find("Player 1").GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = false;
            timerText.text = "player1 turn";
            Debug.LogError("Toggled");
        }

    }

    [Command]
    public void CmdTogglePlayer()
    {
        Debug.Log("intoggle");

        if (GameObject.Find("Player 1").GetComponent<PlayerController>().enabled)
        {
            Debug.LogError("termina player1");
            GameObject.Find("Player 1").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = true;
            timerText.text = "player2 turn";
            Debug.LogError("Toggled");
        }
        else
        {
            Debug.LogError("termina player2");
            GameObject.Find("Player 1").GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = false;
            timerText.text = "player1 turn";
            Debug.LogError("Toggled");
        }


    }

    public void ResetTimer()
    {
        timer = maxTime;
    }

    public void SwitchTurn()
    {
        //ResetTimer();
        //TogglePlayer();
    }

  

    [ClientRpc]
    private void RpcPlayerState()
    {
        Debug.LogError("1 is active: " + GameObject.Find("Player 1").GetComponent<PlayerController>().enabled);
        Debug.LogError( "2 is active: " + GameObject.Find("Player 2").GetComponent<PlayerController>().enabled);
    }

    [Command]
    private void CmdPlayerState()
    {
        Debug.LogError( "1 is active: " + GameObject.Find("Player 1").GetComponent<PlayerController>().enabled);
        Debug.LogError("2 is active: " + GameObject.Find("Player 2").GetComponent<PlayerController>().enabled);
    }



}

