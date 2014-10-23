using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	GameObject Player;
	PlayerController Pc;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			Player = GameObject.FindGameObjectWithTag ("Player");
			Pc = Player.GetComponent ("PlayerController") as PlayerController;
			Pc.GoalIn ();
		}
	}
}
