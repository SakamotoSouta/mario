using UnityEngine;
using System.Collections;

public class DeleteButton : MonoBehaviour {

	GameObject FieldRoot;
	UIButtonMessage Message;
	// Use this for initialization
	void Start () {
		// ルートの設定
		FieldRoot = GameObject.FindGameObjectWithTag ("FieldRoot");
		Message = GetComponent<UIButtonMessage> ();
		Message.target = FieldRoot;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Message) {
			Application.Quit();
		}
	}
}
