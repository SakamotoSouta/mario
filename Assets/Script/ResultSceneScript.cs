using UnityEngine;
using System.Collections;

public class ResultSceneScript : MonoBehaviour {

	public int WaitTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine (ResultCtrl ());
	}

	IEnumerator ResultCtrl(){
		// 設定した時間待機してタイトルへ遷移
		yield return new WaitForSeconds (WaitTime);
		Application.LoadLevel ("Title");
	}
}
