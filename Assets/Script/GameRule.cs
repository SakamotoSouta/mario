﻿using UnityEngine;
using System.Collections;

public class GameRule : MonoBehaviour {
	public static int Life = 3;
	public int Score;
	private int CoinCount;
	public int Coin1UpNum;
	private static int MaxLife = 3;
	public float GameTime;
	public GUIStyle customGuiStyle;

	private GameObject Player;
	private PlayerCtrl pc;
	// 遷移時間
	public int WaitTime;

	// Use this for initialization
	void Start () {
		Score = 0;
		CoinCount = 0;
		Player = GameObject.FindGameObjectWithTag("Player");
		pc = Player.GetComponent ("PlayerCtrl") as PlayerCtrl;
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

	public IEnumerator ClearGame (string NextScene){
		yield return new WaitForSeconds(WaitTime);

		Application.LoadLevel(NextScene);
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
		if(CoinCount > Coin1UpNum){
			CoinCount = 0;
			Life1Up();
		}
	}

	public void Life1Up(){
		Life++;
	}

	void GameOver(){
		Application.LoadLevel ("Result");
	}
}
