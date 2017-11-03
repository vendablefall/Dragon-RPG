using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour {
	GameObject player;
	

	// Use this for initialization
	void Start () {
		 player = GameObject.FindGameObjectWithTag("Player");
		// Debug.Log(player.transform.position.ToString());
	}
	
	// Update is called once per frame
	void Update () {	
		transform.position = player.transform.position;
	}
}
