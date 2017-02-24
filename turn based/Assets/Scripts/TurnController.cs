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
        //maxTime = 20;
        //numPlayers = 0;
        //timer = maxTime;
        timerText.text = "player1 turn";
    }


    void Update()
    {
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
            //GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = false; 
        }


    }

    public void TogglePlayer()
    {
        Debug.Log("intoggle");


        if (GameObject.Find("Player 1").activeSelf)
        {
            Debug.Log("termina player1");
            GameObject.Find("Player 1").GetComponent<PlayerController>().enabled = false;
            GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = true;
            timerText.text = "player2 turn";
            Debug.Log("Toggled");
        }
        else 
        {
            Debug.Log("termina player2");
            GameObject.Find("Player 1").GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = false;
            timerText.text = "player1 turn";
            Debug.Log("Toggled");
        }


    }

    public void ResetTimer()
    {
        timer = maxTime;
    }

    public void SwitchTurn()
    {
        //ResetTimer();
        TogglePlayer();
    }

    [Command]
    void CmdToogleToTwo(){
        GameObject.Find("Player 1").GetComponent<PlayerController>().enabled = false;
        GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = true;
    }

    [Command]
    void CmdToogleToOne()
    {
        GameObject.Find("Player 1").GetComponent<PlayerController>().enabled = true;
        GameObject.Find("Player 2").GetComponent<PlayerController>().enabled = false;
    }

}

