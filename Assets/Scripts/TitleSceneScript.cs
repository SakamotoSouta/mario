using UnityEngine;
using System.Collections;

public class TitleSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// エンターで画面遷移
		if(Input.GetKey(KeyCode.Return)){
			GameRule.InitGame();
			Application.LoadLevel("Game");
		}
	}
	
}
