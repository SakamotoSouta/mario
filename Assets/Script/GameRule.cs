using UnityEngine;
using System.Collections;

public class GameRule : MonoBehaviour {
	private static int Life;
	private static int MaxLife = 3;
	// 遷移時間
	public int Time;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void InitGame (){
		Life = MaxLife;
	}

	public IEnumerator Restart(){
		yield return new WaitForSeconds(Time);
		Life--;
		if(Life < 0){
			GameOver();
			yield break;
		}
		Application.LoadLevel("Game");
	}

	// 仮のUI
	void OnGUI(){
		GUI.Label (new Rect(0,0,1000,1000), ""+Life);
	}
	void GameOver(){
		Application.LoadLevel ("Title");
	}
}
