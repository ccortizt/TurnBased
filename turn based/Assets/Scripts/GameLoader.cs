using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour {

    void Awake()
    {
        Instantiate(Resources.Load("Prefabs/NetworkManager"));
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
