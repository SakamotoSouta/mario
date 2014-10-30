using UnityEngine;
using System.Collections;

public class GameRule : MonoBehaviour {
	public static int GameStageNum = 3;
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
	[HideInInspector]
	public bool activeObjectFlag = true;
	private GameObject staticObject;
	private StaticObject staticObjectScript;
	// Use this for initialization
	void Start () {
		notActiveObject = GameObject.Find ("NotActiveObject");
		CoinCount = 0;
		Player = GameObject.FindGameObjectWithTag("Player");
		pc = Player.GetComponent ("PlayerController") as PlayerController;
		staticObject = GameObject.Find ("staticObject");
		if(staticObject){
			staticObjectScript = staticObject.GetComponent ("StaticObject") as StaticObject;
		}
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
			Life = 0;
			yield break;
		}
		FadeManager.Instance.LoadLevel(Application.loadedLevelName,1.0f);
	}

	// 初期化
	public void SceneInit (){
		CameraCtrl CameraController = Camera.main.GetComponent ("CameraCtrl") as CameraCtrl;
		CameraController.InitCamera ();
	}

	// クリア
	public IEnumerator ClearGame (){
		yield return new WaitForSeconds(WaitTime);
		// 次のシーン番号を代入
		int Next = Application.loadedLevel + 1;
		if (Next < GameStageNum) {
			if(staticObject){
				// 値の保存
				staticObjectScript.Score = Score;
				staticObjectScript.State = pc.State;
				DontDestroyOnLoad(staticObject);
			}
			FadeManager.Instance.LoadLevel(Next, 1.0f);
		}
		else{
			if(staticObject){
				Destroy(staticObject);
			}
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
		FadeManager.Instance.LoadLevel("Result",1.0f);
	}
	
}
