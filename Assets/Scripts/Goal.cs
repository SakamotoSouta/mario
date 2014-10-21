using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	GameObject Player;
	PlayerController Pc;

	// Use this for initialization
	void Start () {
		if(Application.loadedLevelName == "Game"){
			Player = GameObject.FindGameObjectWithTag ("Player");
			Pc = Player.GetComponent ("PlayerController") as PlayerController;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Pc.GoalIn ();
			Debug.Log ("gole");
		}
	}
}
