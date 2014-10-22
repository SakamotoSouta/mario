using UnityEngine;
using System.Collections;

public class GoalPole : MonoBehaviour {
	public int GoleScore;
	GameObject RuleObject;
	GameRule Rule;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			RuleObject = GameObject.Find ("GameRule");
			Rule = RuleObject.GetComponent ("GameRule") as GameRule;
			Rule.AddScore(GoleScore);
			PlayerController PlayerController= other.GetComponent("PlayerController") as PlayerController;
			PlayerController.HitGoalPole = true;
		}
	}
}
