using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// スピード定数
	const float DashSpeed = 8.0f;
	const float DefaultSpeed = 6.0f;
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
	// ファイアーボール
	public GameObject FireBallPrefab;
	// アイテム
	private GameObject Item;
	private ItemController ItemController;
	// ジャンプ力
	public float jumpPawer;
	// 前回のY座標
	private float oldPositionY;
	private float positionTemp;

	// ダッシュフラグ
	private bool Dash;
	// スピード
	private float Speed = DefaultSpeed;
	// 重力
	const float Gravity = 35.0f;
	// ジャンプの長押しカウンター
	private float jumpCounter;
	// 速度
	public Vector3 Velocity;
	// フィールドに接しているか
	[HideInInspector]
	public bool onGround;
	// ゴールフラグ
	public bool Goal = false;
	// ジャンプフラグ
	public bool Jump = false;
	// 死亡フラグ
	public  bool Damage = false;
	// ゲームルール管理者
	private GameRule rule;
	// キャラクターコントローラー
	private CharacterController Col;
	// 前回の向きキャラを入力された方向へ向かせるため
	private int oldVector;
	// ブロックにあたっているかどうか
	private bool blockHit = false;
	[HideInInspector]
	// プレイヤーを操作できるかどうか
	public bool PlayerControllFlag = true;
	// ゴールのポールに接しているかどうか
	public bool HitGoalPole = false;
	public float GoalPoleSpeed;

	// プレイヤーの声を管理しているところ
	private PlayerSEManager PlayerSE;
	// SE管理者
	private GameObject SEControllerObject;
	private SEController SE;


	// Use this for initialization
	void Start () {
		// SE 
		SEControllerObject = GameObject.Find ("SEController");
		SE = SEControllerObject.GetComponent ("SEController") as SEController;
		// アイテム管理者の取得
		Item = GameObject.Find ("ItemRoot");
		ItemController = Item.GetComponent("ItemController") as ItemController;
		// ゲーム管理者取得
		GameObject GameRule = GameObject.Find ("GameRule");
		rule = GameRule.GetComponent ("GameRule") as GameRule;

		// エフェクトの初期化
		InvinsibleEffect = transform.Find ("InvinsibleEffect").GetComponent<ParticleSystem> ();
		InvinsibleEffect.Stop ();
		// プレイヤーの向きベクトルの初期化
		oldVector = 1;

		transform.localRotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
		// キャラクターコントローラーの取得
		Col = gameObject.GetComponent("CharacterController") as CharacterController;

		// 声の管理者取得
		PlayerSE = GetComponent<PlayerSEManager> ();
	}
	
	// Update is called once per frame
	void Update (){
		// プレイヤー操作
		PlayerControll ();

		// Z判定 常に0
		if (transform.position.z > 0 || transform.position.z < 0) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		}


		// とりあえず飛んでれば着地していない
		if(!Col.isGrounded){
			onGround = false;
		}

		// ゴールポールにあたったとき
		if(HitGoalPole){
			PlayerControllFlag = false;
			transform.Translate(0f, -GoalPoleSpeed, 0f);
			Jump = false;
			Damage = false;
			
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}



		if(!Damage && PlayerControllFlag){
			// 重力処理
			Velocity.y -= Gravity * Time.deltaTime;
 			Col.Move (Velocity * Time.deltaTime);

		}

		// 地面に接していたら
		if (Col.isGrounded) {
			blockHit = false;
			Velocity.y = 0f;
		}// if

	}


	// プレイヤー操作
	void PlayerControll(){
		// プレイヤーが操作可能な場合のアップデート
		if(PlayerControllFlag){

			// LSHIFTでダッシュフラグを立てる
			if(Input.GetKey(KeyCode.LeftShift)){
				Dash = true;
			}
			else{
				Dash = false;
			}

			if(State == PLAYER_STATE.PLAYER_FIRE){
				// ファイアーボール
				if(Input.GetKeyDown(KeyCode.Q)){
					GameObject Fb = Instantiate(FireBallPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation) as GameObject;
					Fireball FireBall = Fb.GetComponent("Fireball") as Fireball;
					FireBall.GenerateFireBall(new Vector3(0f, 0f, 1f));
					//　SEの再生
					SE.SEPlay(SEController.SE_LABEL.SE_FIREBALL);
				}
			}
			if(Dash){
				Speed = DashSpeed;
			}
			else{
				Speed = DefaultSpeed;
			}
			if (!Damage) {
				// 左右入力で移動
				Velocity.x = Input.GetAxis ("Horizontal") * Speed;
			}
			
			
			
			if (Velocity.x > 0) {
				oldVector = 1;
			}
			else if (Velocity.x < 0) {
				oldVector = -1;
			}
			
			transform.rotation = Quaternion.LookRotation (new Vector3 (oldVector, 0f, 0f));
			
			// ジャンプ処理
			if(Input.GetKeyDown(KeyCode.Space)){
				if(Col.isGrounded){	
					// ジャンプSEの再生
					PlayerSE.PlayerSEPlay(PlayerSEManager.PLAYER_SE_LABEL.PLAYER_SE_JUMP);
					Jump = true;
				}
			}
			// 長押し対応
			if (Input.GetKey(KeyCode.Space) && Jump) {
				
				if(jumpCounter < 10f && Jump){
					jumpCounter++;
					Velocity.y = jumpPawer;
				}
			}// if
			
			if(Input.GetKeyUp(KeyCode.Space)){
				jumpCounter = 100;
			}
			
			if (Jump) {
				if(!blockHit){
					RaycastHit hit;
					GameObject head = GameObject.Find("Character1_Spine1");
					Vector3 fromPos = head.transform.position;
					Vector3 direction = new Vector3(0, 1, 0);
					float length = 1.0f * transform.localScale.y;
					// 上方向にレイを飛ばしてブロックにあたっていないか判定
					Debug.DrawRay(fromPos, direction.normalized * length, Color.green, 1, false);
					if (Physics.Raycast(fromPos, direction, out hit, length)) {
						if(hit.collider.tag == "Block"){
							GameObject block = hit.collider.gameObject;
							Block b = block.GetComponent("Block")as Block;
							b.HitBlock();
							Velocity.y = 0;
							blockHit = true;
							jumpCounter = 100;
						}
					}
				}
			}
			else{
				jumpCounter = 0;
			}
			
			// 穴判定
			if (transform.position.y < -5) {
				if(!rule.endFlag){
					PlayerSE.PlayerSEPlay(PlayerSEManager.PLAYER_SE_LABEL.PLAYER_SE_DEAD);
					StartCoroutine (rule.Restart ());
				}
				rule.endFlag = true;
			}
		}
	}
	
	// ゴール
	public void GoalIn(){
		// ゴールSEを再生
		PlayerSE.PlayerSEPlay(PlayerSEManager.PLAYER_SE_LABEL.PLAYER_SE_CLEAR);
		HitGoalPole = false;
		PlayerControllFlag = false;
		Goal = true;
		transform.rotation = Quaternion.Euler(0, 180, 0);
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		CameraCtrl cc = camera.GetComponent ("CameraCtrl") as CameraCtrl;
		cc.CameraOffet.z = -8;
		cc.CameraOffet.y = 10;
		Speed = 0;
		Velocity.x = 0;
		Velocity.y = 0;
	}

	public void GetStar(){
		StartCoroutine (StateInvincible ());
	}

	// 無敵時間
	IEnumerator StateInvincible(){
		// BGMの変更
		GameObject BGMController = GameObject.Find ("BGMController");
		BGMController bgm = BGMController.GetComponent ("BGMController") as BGMController;
		bgm.StartInbisible ();
		Invincible = true;
		InvinsibleEffect.Play ();
		yield return new WaitForSeconds (InvincibleTime);
		Invincible = false;
		InvinsibleEffect.Stop();
		bgm.EndInbinsible ();
	}

	// プレイヤーの死亡
	void PlayerDead(){
		// 死亡SEの再生
		PlayerSE.PlayerSEPlay(PlayerSEManager.PLAYER_SE_LABEL.PLAYER_SE_DEAD);
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
			// プレイヤーパワーダウンSEの再生
			PlayerSE.PlayerSEPlay(PlayerSEManager.PLAYER_SE_LABEL.PLAYER_SE_PAWER_DOWN);
			State = PLAYER_STATE.PLAYER_NORMAL;
			break;
		case PLAYER_STATE.PLAYER_FIRE:
			// プレイヤーパワーダウンSEの再生
			PlayerSE.PlayerSEPlay(PlayerSEManager.PLAYER_SE_LABEL.PLAYER_SE_PAWER_DOWN);
			State = PLAYER_STATE.PLAYER_NORMAL;
			break;
		default:
			break;
		}
	
		SetState (State);
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
		// プレイヤーパワーアップSEの再生
		PlayerSE.PlayerSEPlay(PlayerSEManager.PLAYER_SE_LABEL.PLAYER_SE_PAWER_UP);
		SetState (State);

	}

	// ステータスの設定
	public void SetState(PLAYER_STATE state){
		State = state;
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

	// アイテム取得
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
