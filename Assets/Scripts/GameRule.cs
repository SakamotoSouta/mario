﻿using UnityEngine;
using System.Collections;

public class GameRule : MonoBehaviour {
	public static int MaxScene = 3;
	public static int Life = 3;
	public int Score;
	private int CoinCount;
	public int Coin1UpNum;
	private static int MaxLife = 3;
	public float GameTime;
	public GUIStyle customGuiStyle;
	[HideInInspector]
	public bool endFlag = false;
	// ゲームシーンのオブジェクト
	private GameObject notActiveObject;
	private GameObject Player;
	private PlayerController pc;
	// 遷移時間
	public int WaitTime;
	public bool activeObjectFlag = true;
	private GameObject staticObject;

	// Use this for initialization
	void Start () {
		notActiveObject = GameObject.Find ("NotActiveObject");
		CoinCount = 0;
		Player = GameObject.FindGameObjectWithTag("Player");
		pc = Player.GetComponent ("PlayerController") as PlayerController;
	}
	
	// Update is called once per frame
	void Update () {
		if(!pc.Goal){
			GameTime -= Time.deltaTime;
			if (GameTime < 0) {
				GameTime = 0;
				TimeOver();
			}

		}
	}

	public static void InitGame (){
		Life = MaxLife;
	}
	private void TimeOver(){

		pc.Damage = true;
		if (!endFlag) {
			StartCoroutine (Restart ());
			endFlag = true;
		}
 	}

	public IEnumerator Restart(){
		yield return new WaitForSeconds(WaitTime);
		Life--;
		if(Life < 0){
			GameOver();
			yield break;
		}
		Application.LoadLevel(Application.loadedLevelName);
	}

	// 初期化
	public void SceneInit (){
		CameraCtrl CameraController = Camera.main.GetComponent ("CameraCtrl") as CameraCtrl;
		CameraController.InitCamera ();
	}

	// クリア
	public IEnumerator ClearGame (){
		yield return new WaitForSeconds(WaitTime);
		staticObject = GameObject.Find("staticObject");
		if (MaxScene > Application.loadedLevel + 1) {
			MaxScene = Application.loadedLevel + 1;
			// 値の保存
			staticObject staticObjectScript = staticObject.GetComponent("staticObject") as staticObject;
			staticObjectScript.Score = Score;
			staticObjectScript.State = pc.State;
			DontDestroyOnLoad(staticObject);
			Application.LoadLevel(MaxScene);
		}
		else{
			Destroy(staticObject);
			GameOver();
		}
	}

	// Lifeの取得
	public int GetLife(){
		return Life;
	}

	public int GetScore(){
		return Score;
	}

	public void AddScore(int Value){
		Score += Value;
	}

	public void GetCoin(){
		CoinCount++;
		AddScore(100);
		if(CoinCount >= Coin1UpNum){
			CoinCount = 0;
			Life1Up();
		}
	}

	// パイプに入ったとき
	public void PipeInChangeScene(Vector3 afterPosition, string sceneName){
		// 位置の設定
		pc.transform.position = afterPosition;
		// オリジナル→サブ
		if (activeObjectFlag) {
			Application.LoadLevelAdditive(sceneName);
			notActiveObject.SetActive (false);
			activeObjectFlag = false;
		}
		// サブ→オリジナル
		else{
			notActiveObject.SetActive (true);
			activeObjectFlag = true;
			GameObject Delete = GameObject.Find("DeleteObject");
			Destroy(Delete);
			GameObject OutPipe = GameObject.Find("OutPipe");
			PipeActionController PipeAction = OutPipe.GetComponent("PipeActionController") as PipeActionController;
			PipeAction.SetPipeAction(new Vector3(0, 180, 0));
		}
	}

	public void Life1Up(){
		Life++;
	}

	void GameOver(){
		Application.LoadLevel ("Result");
	}
	
}
