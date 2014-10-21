using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// プレイヤーの状態
	public enum PLAYER_STATE{
		PLAYER_NORMAL = 0,
		PLAYER_SUPER,
		PLAYER_FIRE,
		PLAYER_MAX
	}
	public PLAYER_STATE State;

	// エフェクト
	ParticleSystem InvinsibleEffect;

	// 無敵
	public bool Invincible = false;
	public int InvincibleTime;
	public GameObject FireBallPrefab;

	private GameObject Item;
	ItemController ItemController;
	public float jumpPawer;
	private float Inertia = 0.9f;
	private float Speed = 7.0f;
	[HideInInspector]
	public Vector3 Velocity;
	[HideInInspector]
	public bool onGround;
	public bool Goal = false;
	public bool Jump = false;
	public  bool Damage = false;
	private GameRule rule;
	private CharacterController Col;
	private int oldVector;
	private bool blockHit = false;
	[HideInInspector]
	// プレイヤーを操作できるかどうか
	public bool PlayerControllFlag = true;


	// Use this for initialization
	void Start () {
		// アイテム管理者の取得
		Item = GameObject.Find ("ItemRoot");
		ItemController = Item.GetComponent("ItemController") as ItemController;
		// ゲーム管理者取得
		GameObject GameRule = GameObject.Find ("GameRule");
		rule = GameRule.GetComponent ("GameRule") as GameRule;
		// プレイヤーのステートの初期化
		State = PLAYER_STATE.PLAYER_NORMAL;

		// エフェクトの初期化
		InvinsibleEffect = transform.Find ("InvinsibleEffect").GetComponent<ParticleSystem> ();
		InvinsibleEffect.Stop ();
		// プレイヤーの向きベクトルの初期化
		oldVector = 1;

		transform.localRotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
		// キャラクターコントローラーの取得
		Col = gameObject.GetComponent("CharacterController") as CharacterController;
	}
	
	// Update is called once per frame
	void Update () {
		// Z判定入れるとバグるアニメーションのところを修正するときに実装する
		/*
		if (transform.position.z > 0 || transform.position.z < 0) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		}
		*/

		if(PlayerControllFlag){
			if(State == PLAYER_STATE.PLAYER_FIRE){
				if(Input.GetKeyDown(KeyCode.Q)){
					GameObject Fb = Instantiate(FireBallPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
					Fireball FireBall = Fb.GetComponent("Fireball") as Fireball;
					FireBall.GenerateFireBall(new Vector3(0f, 0f, 1f));
				}
			}

			if (!Damage) {
				// 左右入力で移動
				Velocity.x = Input.GetAxis ("Horizontal") * Speed;
			}



			if (Velocity.x > 0) {
					oldVector = 1;
			} else if (Velocity.x < 0) {
					oldVector = -1;
			}

			transform.rotation = Quaternion.LookRotation (new Vector3 (oldVector, 0f, 0f));



			if (Input.GetKeyDown (KeyCode.Space) && Col.isGrounded) {
				Jump = true;
			}// if

			if (Jump) {
				if(!blockHit){
					RaycastHit hit;
					GameObject head = GameObject.Find("Character1_Spine1");
					Vector3 fromPos = head.transform.position;
					Vector3 direction = new Vector3(0, 1, 0);
					float length = 0.3f;
					// 上方向にレイを飛ばしてブロックにあたっていないか判定
					//Debug.DrawRay(fromPos, direction.normalized * length, Color.green, 1, false);
					if (Physics.Raycast(fromPos, direction, out hit, length)) {
						if(hit.collider.tag == "Block"){
							GameObject block = hit.collider.gameObject;
							Block b = block.GetComponent("Block")as Block;
							b.HitBlock();
							blockHit = true;
						}
					}
				}
			}

			// 穴判定
			if (transform.position.y < -5) {
				if(!rule.endFlag){
					StartCoroutine (rule.Restart ());
				}
				rule.endFlag = true;
			}
		}

		if(!Damage && PlayerControllFlag){
			// 重力処理
			Velocity.y += Physics.gravity.y * Time.deltaTime;
 			Col.Move (Velocity * Time.deltaTime);

			// 慣性
			Velocity *= Inertia;
		}
		// 地面に接していたら
		if (Col.isGrounded) {
			blockHit = false;
			Velocity.y = 0f;
		}// if
	}
	
	// ゴール
	public void GoalIn(){
		PlayerControllFlag = false;
		Goal = true;
		Jump = false;
		Damage = false;
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		CameraCtrl cc = camera.GetComponent ("CameraCtrl") as CameraCtrl;
		cc.CameraOffet.z = -5;
		cc.CameraOffet.y = 0;

		transform.rotation = Quaternion.Euler(0, 180, 0);
		Speed = 0;
		Velocity.x = 0;
		Velocity.y = 0;
	}

	public void GetStar(){
		StartCoroutine (StateInvincible ());
	}

	// 無敵時間
	IEnumerator StateInvincible(){
		Invincible = true;
		InvinsibleEffect.Play ();
		yield return new WaitForSeconds (InvincibleTime);
		Invincible = false;
		InvinsibleEffect.Stop();

	}


	void PlayerDead(){
		Jump = false;
		Col.enabled = false;
		Damage = true;
	}

	// 敵にあたった時
	public void PlayerDamage(){
		switch (State) {
		case PLAYER_STATE.PLAYER_NORMAL:
			PlayerDead();
			break;
		case PLAYER_STATE.PLAYER_SUPER:
			State = PLAYER_STATE.PLAYER_NORMAL;
			break;
		case PLAYER_STATE.PLAYER_FIRE:
			State = PLAYER_STATE.PLAYER_NORMAL;
			break;
		default:
			break;
		}
		switch(State){
		case PLAYER_STATE.PLAYER_NORMAL:
			transform.localScale = new Vector3(1, 1, 1);
			break;
		case PLAYER_STATE.PLAYER_SUPER:
			transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			break;
		case PLAYER_STATE.PLAYER_FIRE:
			transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			break;
		}
	}

	// プレイヤーのパワーアップ
	public void PlayerPawerUp(){
		// 現在の状態によってパワーアップ
		switch (State) {
		case PLAYER_STATE.PLAYER_NORMAL:
			State = PLAYER_STATE.PLAYER_SUPER;
			break;
		case PLAYER_STATE.PLAYER_SUPER:
			State = PLAYER_STATE.PLAYER_FIRE;
			break;
		case PLAYER_STATE.PLAYER_FIRE:
			rule.AddScore(500);
			break;
		default:
			break;
		}
		switch(State){
		case PLAYER_STATE.PLAYER_NORMAL:
			transform.localScale = new Vector3(1, 1, 1);
			break;
		case PLAYER_STATE.PLAYER_SUPER:
			transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			break;
		case PLAYER_STATE.PLAYER_FIRE:
			transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
			break;
		}

	}

	// プレイヤーアイテム取得
	public void PlayerGetItem(GameObject Item, ItemController.ITEM_TYPE Type){
		ItemController.GetItem(Type);
		Destroy(Item.gameObject);
	}


	// レイヤー制御であたり判定を一定時間消す
	public IEnumerator NotHitJudge(float InvinsibleTime ,string Layer1, string Layer2){
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer(Layer1), LayerMask.NameToLayer(Layer2), true);
		yield return new WaitForSeconds (InvinsibleTime);
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer(Layer1), LayerMask.NameToLayer(Layer2), false);
	}
}
