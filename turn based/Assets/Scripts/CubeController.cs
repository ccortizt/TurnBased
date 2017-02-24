using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CubeController : NetworkBehaviour {


    private Vector3 screenPoint;
    private float startPosX;
    private float startPosY;
    //float distance = 10;
	void Start () {
		
	}
	

	void Update () {
		
	}

    void OnMouseDown()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }


}


