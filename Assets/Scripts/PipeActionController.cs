using UnityEngine;
using System.Collections;

public class PipeActionController : MonoBehaviour {
	public enum PIPE_ACTION_TYPE{
		PIPE_ACTION_IN,
		PIPE_ACTION_OUT,
		PIPE_ACTION_MAX
	}
	public enum IN_KEY{
		DOWN = 0,
		UP,
		RIGHT,
		LEFT,
		MAX
	}
	
	public string InPipeNextScene;
	public IN_KEY InKey; 
	public PIPE_ACTION_TYPE PipeActionType;
	// アニメーション方向
	public Vector3 actionVector;
	public float Speed;
	// プレイヤー情報
	private GameObject PlayerObject;
	private PlayerController PlayerController;
	// フラグ
	private bool PipeAnictionFlag = false;
	// 出る座標
	public Vector3 outPosition;

	// Use this for initialization
	void Start () {
		// プレイヤーの取得
		PlayerObject = GameObject.FindGameObjectWithTag("Player");
		PlayerController = PlayerObject.GetComponent ("PlayerController") as PlayerController ;
		// 取り付けられたオブジェクトのレイヤーでタイプ判断
		if(PipeActionType == PIPE_ACTION_TYPE.PIPE_ACTION_IN){
			gameObject.AddComponent<InPipeController>();
		}
		else if(PipeActionType == PIPE_ACTION_TYPE.PIPE_ACTION_OUT){

		}
	}
	
	// Update is called once per frame
	void Update () {
		// パイプにもぐるあいだ
		if(PipeAnictionFlag){
			PlayerObject.transform.Translate(actionVector * Speed);
		}
	}

	// パイプアクションの始まり
	public void SetPipeAction(Vector3 playerRotation){
		// プレイヤーの角度を変更
		PlayerObject.transform.rotation = Quaternion.Euler(playerRotation);
		// プレイヤーの速度を0にする
		PlayerController.Velocity = new Vector3(0f, 0f, 0f);
		// ポジションを合わせる
		PlayerObject.transform.position = new Vector3(PlayerObject.transform.position.x, PlayerObject.transform.position.y, transform.position.z);
		PipeAnictionFlag = true;
		PlayerController.PlayerControllFlag = false;
		// あたり判定をけす
		//Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pipe"));
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
	}

	// パイプと離れた瞬間
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			PipeAnictionFlag = false;
			PlayerController.PlayerControllFlag = true;
			// あたり判定をもどす
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pipe"), false);
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
		}
	}

	// パイプアクションフラグの取得
	public bool GetPipeFlag(){
		return PipeAnictionFlag;
	}

	
}
