using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {
	// アニメーション
	private Animator Anim;
	private int SpeedID;
	private int JumpID;
	private int DamageID;

	// アニメーションのデフォルトの再生速度
	private float defaultSpeed;
	// 着地判定を調べる回数
	private readonly int landingCheckLimit = 100;
	// 着地判定チェックを行う時間間隔
	private readonly float waitTime = 0.05F;
	// 着地モーションへの移項を許可する距離
	private readonly float landingDistance = 5F;
	public float jumpPawer;
	public float Inertia;
	public float Speed;
	public Vector3 Velocity;
	public bool Jump = false;
	public  bool Damage = false;
	public GameRule rule;
	private float pawer;
	private CharacterController Col;
	private int oldVector;

	// Use this for initialization
	void Start () {
		oldVector = 1;
		// アニメーションの取得
		Anim = GetComponent<Animator> ();
		// アニメーションイベントの取得
		SpeedID = Animator.StringToHash ("Speed");
		JumpID = Animator.StringToHash ("Jump");
		DamageID = Animator.StringToHash ("Damage");
		transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
		// キャラクターコントローラーの取得
		Col = GetComponent ("CharacterController") as CharacterController;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Damage) {
			// 左右入力で移動
			Velocity.x = Input.GetAxis ("Horizontal") * Speed;
			Velocity.y += Physics.gravity.y * Time.deltaTime;
			Col.Move (Velocity * Time.deltaTime);
		}

		// 慣性
		Velocity *= Inertia;
		// 地面に接していたら
		if(Col.isGrounded){
			Velocity.y = 0f;
		}// if
		if (Velocity.x > 0) {
			oldVector = 1;
		}
		else if (Velocity.x < 0) {
			oldVector = -1;
		}

		transform.rotation = Quaternion.LookRotation (new Vector3 (oldVector, 0f, 0f));



		if (Input.GetKey(KeyCode.Space) && Col.isGrounded) {
			pawer = Input.GetAxis("Jump") * jumpPawer;
			Jump = true;
		}// if

		if (Jump) {
			CheckLanding ();
		}

		// 穴判定
		if(transform.position.y < -5){
			StartCoroutine(rule.Restart());
		}
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
	}

	// あたり判定
	public void PlayerDamage(){
		Jump = false;
		Col.enabled = false;
		Damage = true;
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
	}
	void OnJumpEnd(){
	}
	
}
