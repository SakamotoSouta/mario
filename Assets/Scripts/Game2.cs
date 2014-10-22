using UnityEngine;
using System.Collections;

public class Game2 : MonoBehaviour {
	GameObject RuleObject;
	GameRule Rule;
	// Use this for initialization
	void Start () {
		GameObject staticObject= GameObject.Find("staticObject");
		StaticObject staticObjectScript = staticObject.GetComponent("StaticObject") as StaticObject;

		RuleObject = GameObject.Find ("GameRule");
		Rule = RuleObject.GetComponent ("GameRule") as GameRule;
		Rule.SceneInit ();
		Rule.Score = staticObjectScript.Score;
		staticObjectScript.Score = 0;
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerController PlayerController = Player.GetComponent ("PlayerController") as PlayerController;
		PlayerController.SetState(staticObjectScript.State);
		staticObjectScript.State = PlayerController.PLAYER_STATE.PLAYER_NORMAL;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
