using UnityEngine;
using System.Collections;

public class StaticObject : MonoBehaviour {
	public int Score;
	public PlayerController.PLAYER_STATE State;
	// Use this for initialization
	void Start () {
		Score = 0;
		State = PlayerController.PLAYER_STATE.PLAYER_NORMAL;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
