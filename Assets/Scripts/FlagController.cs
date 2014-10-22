using UnityEngine;
using System.Collections;

public class FlagController : MonoBehaviour {

	GameObject Player;
	PlayerController PlayerController;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerController = Player.GetComponent ("PlayerController") as PlayerController;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerController.HitGoalPole) {
			transform.Translate(0f, 0.012f, 0f);
		}
	}
}
