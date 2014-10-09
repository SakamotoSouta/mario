using UnityEngine;
using System.Collections;

public class GameRule : MonoBehaviour {
	private static int Life;
	private static int MaxLife = 3;
	public float GameTime;
	public GUIStyle customGuiStyle;
	// 遷移時間
	public int WaitTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GameTime -= Time.deltaTime;
		Debug.Log (""+GameTime);
		if (GameTime < 0) {
			GameTime = 0;
			TimeOver();
		}
	}

	public static void InitGame (){
		Life = MaxLife;
	}
	private void TimeOver(){
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		PlayerCtrl pc = Player.GetComponent ("PlayerCtrl") as PlayerCtrl;
		pc.Damage = true;
		StartCoroutine( Restart());
 	}

	public IEnumerator Restart(){
		yield return new WaitForSeconds(WaitTime);
		Life--;
		if(Life < 0){
			GameOver();
			yield break;
		}
		Application.LoadLevel("Game");
	}

	// 仮のUI
	void OnGUI(){
		int time = (int)GameTime;
		GUI.Label (new Rect(0,0,100,100), ""+Life, customGuiStyle);
		GUI.Label (new Rect(400,0,500,100), ""+time, customGuiStyle);

	}
	void GameOver(){
		Application.LoadLevel ("Title");
	}
}
