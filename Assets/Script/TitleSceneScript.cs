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
			Application.LoadLevel("Game");
		}
	}

	// 仮でGUIで出力
	void OnGUI(){
		GUI.Label (new Rect(Screen.width / 2 - 100, Screen.height / 2, Screen.width / 2 + 100, Screen.height / 2 + 100), "Press START");
	}
}
