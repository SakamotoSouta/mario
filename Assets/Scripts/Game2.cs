using UnityEngine;
using System.Collections;

public class Game2 : MonoBehaviour {
	GameObject RuleObject;
	GameRule Rule;
	// Use this for initialization
	void Start () {
		RuleObject = GameObject.Find ("GameRule");
		Rule = RuleObject.GetComponent ("GameRule") as GameRule;
		Rule.SceneInit ();
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerController PlayerController = Player.GetComponent ("PlayerController") as PlayerController;

		// 前回のスコア、残機、ステータスの取得
		GameObject staticObject= GameObject.Find("staticObject");
		if(staticObject){
			StaticObject staticObjectScript = staticObject.GetComponent("StaticObject") as StaticObject;
			Rule.Score = staticObjectScript.Score;
			staticObjectScript.Score = 0;
			PlayerController.SetState(staticObjectScript.State);
			staticObjectScript.State = PlayerController.PLAYER_STATE.PLAYER_NORMAL;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
