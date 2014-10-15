using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {
	// アニメーション
	private Animator Anim;
	private int SpeedID;
	private int JumpID;
	private int DamageID;
	private int GoalID;

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
	// アニメーションのデフォルトの再生速度
	private float defaultSpeed;
	// 無敵
	public bool Invincible = false;
	public int InvincibleTime;
	// 着地判定を調べる回数
	private readonly int landingCheckLimit = 100;
	// 着地判定チェックを行う時間間隔
	private readonly float waitTime = 0.05F;
	// 着地モーションへの移項を許可する距離
	private readonly float landingDistance = 5F;
	public float jumpPawer;
	public float Inertia;
	public float Speed;
	[HideInInspector]
	public Vector3 Velocity;
	public bool Goal = false;
	public bool Jump = false;
	public  bool Damage = false;
	public GameRule rule;
	private float pawer;
	private CharacterController Col;
	private int oldVector;
	private bool blockHit = false;
	public GameObject FireBallPrefab;

	// Use this for initialization
	void Start () {
		// プレイヤーのステートの初期化
		State = PLAYER_STATE.PLAYER_NORMAL;

		// エフェクトの初期化
		InvinsibleEffect = transform.Find ("InvinsibleEffect").GetComponent<ParticleSystem> ();
		InvinsibleEffect.Stop ();
		// プレイヤーの向きベクトルの初期化
		oldVector = 1;
		// アニメーションの取得
		Anim = GetComponent<Animator>();
		// アニメーションイベントの取得
		SpeedID = Animator.StringToHash ("Speed");
		JumpID = Animator.StringToHash ("Jump");
		DamageID = Animator.StringToHash ("Damage");
		GoalID = Animator.StringToHash ("Goal");
		transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
		// キャラクターコントローラーの取得
		Col = gameObject.GetComponent("CharacterController") as CharacterController;
	}
	
	// Update is called once per frame
	void Update () {
		if(!Goal){
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



			if (Input.GetKey (KeyCode.Space) && Col.isGrounded) {
					pawer = Input.GetAxis ("Jump") * jumpPawer;
					Jump = true;
			}// if

			if (Jump) {
				CheckLanding ();
				if(!blockHit){
					RaycastHit hit;
					GameObject head = GameObject.Find("Character1_Spine1");
					Vector3 fromPos = head.transform.position;
					Vector3 direction = new Vector3(0, 1, 0);
				float length = 0.1f;
					// 上方向にレイを飛ばしてブロックにあたっていないか判定
					Debug.DrawRay(fromPos, direction.normalized * length, Color.green, 1, false);
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
				StartCoroutine (rule.Restart ());
			}
		}
		if(!Damage){
			Velocity.y += Physics.gravity.y * Time.deltaTime;
			Col.Move (Velocity * Time.deltaTime);

			// 慣性
			Velocity *= Inertia;
		}
		// 地面に接していたら
		if (Col.isGrounded) {
			Velocity.y = 0f;
		}// if
		// アニメーションの設定
		AnimSet ();


	}

	void AnimSet(){
		// アニメーションの設定
		if (Velocity.x < 0f) {
			Anim.SetFloat (SpeedID, Velocity.x * -1); 
		}// if
		else {
			Anim.SetFloat (SpeedID, Velocity.x); 
		}// else

		Anim.SetBool (DamageID, Damage);
		Anim.SetBool (JumpID, Jump);
		Anim.SetBool (GoalID, Goal);
	}

	IEnumerator CheckLanding()
	{
		// 規定回数チェックして成功しない場合も着地モーションに移行する
		for(int count = 0;count < landingCheckLimit; count++)
		{
			var raycast = new RaycastHit();
			var raycastSuccess = Physics.Raycast(transform.position, Vector3.down, out raycast);
			// レイを飛ばして、成功且つ一定距離内であった場合、着地モーションへ移行させる
			if(raycastSuccess && raycast.distance < landingDistance) break;
			yield return new WaitForSeconds(waitTime);
		}
		Anim.speed = defaultSpeed;
		yield break;
	}

	// ゴール
	public void GoalIn(){
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

	// あたり判定
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

	void OnDead(){
		Anim.speed = 0f;
		StartCoroutine(rule.Restart());
	}

	// アニメーションイベント
	void OnJumpStart(){
		defaultSpeed = Anim.speed;
		Velocity.y = pawer;
	}
	void OnJumpTopPoint(){
		Anim.speed = 0f;
		StartCoroutine(CheckLanding());
	}

	void OnJumpHitEnd(){
		Jump = false;
		blockHit = false;
	}
	void OnJumpEnd(){
	}
	void AnimEnd(){
		StartCoroutine (rule.ClearGame("Result"));
	}

	// キャラクターコントローラーのあたり判定
	void OnControllerColliderHit(ControllerColliderHit other){
		if(other.gameObject.tag == "ItemShoot"){
			ItemShoot Shoot = other.collider.GetComponent("ItemShoot") as ItemShoot;
			if(Shoot.State == ItemShoot.ITEM_SHOOT_STATE.STAY && !Invincible){
				if(Velocity.x > 5f || Velocity.x < -5f){
					Shoot.State = ItemShoot.ITEM_SHOOT_STATE.SHOOT;
					Shoot.Velocity = new Vector3(Shoot.Speed * Vector3.Normalize(new Vector3(Velocity.x, 0f, 0f)).x, 0f, 0f);
				}
			}

			else if(Shoot.State == ItemShoot.ITEM_SHOOT_STATE.SHOOT){
				PlayerDamage();
			}
			else if(Invincible){
				Destroy(other.gameObject);
			}
		}
		// パワーアップ
		if(other.gameObject.tag == "ItemPawerUp"){
			GameObject Item = GameObject.Find("ItemRoot");
			ItemController ItemController = Item.GetComponent("ItemController") as ItemController;
			ItemController.GetItem(ItemController.ITEM_TYPE.ITEM_PAWERUP);
			Destroy(other.gameObject);
		}
		// 花
		if(other.gameObject.tag == "ItemStar"){
			GameObject Item = GameObject.Find("ItemRoot");
			ItemController ItemController = Item.GetComponent("ItemController") as ItemController;
			ItemController.GetItem(ItemController.ITEM_TYPE.ITEM_STAR);
			Destroy(other.gameObject);
		}
		// コイン
		if(other.gameObject.tag == "ItemCoin"){
			GameObject Item = GameObject.Find("ItemRoot");
			ItemController ItemController = Item.GetComponent("ItemController") as ItemController;
			ItemController.GetItem(ItemController.ITEM_TYPE.ITEM_COIN);
			Destroy(other.gameObject);
		}
		// スター
		if(other.gameObject.tag == "ItemStar"){
			GameObject Item = GameObject.Find("ItemRoot");
			ItemController ItemController = Item.GetComponent("ItemController") as ItemController;
			ItemController.GetItem(ItemController.ITEM_TYPE.ITEM_STAR);
			Destroy(other.gameObject);
		}
	}

	// レイヤー制御であたり判定を一定時間消す
	public IEnumerator NotHitJudge(float InvinsibleTime ,string Layer1, string Layer2){
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer(Layer1), LayerMask.NameToLayer(Layer2), true);
		yield return new WaitForSeconds (InvinsibleTime);
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer(Layer1), LayerMask.NameToLayer(Layer2), false);

	}
}
