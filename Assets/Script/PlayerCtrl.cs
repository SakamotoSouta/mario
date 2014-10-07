using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {
	// アニメーション
	private Animator Anim;
	private int SpeedID;
	private int JumpID;

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
	private float pawer;
	private Vector3 Velocity;
	private CharacterController Col;
	private bool Jump = false;

	// Use this for initialization
	void Start () {
		// アニメーションの取得
		Anim = GetComponent<Animator> ();
		// アニメーションイベントの取得
		SpeedID = Animator.StringToHash ("Speed");
		JumpID = Animator.StringToHash ("Jump");
		transform.rotation = Quaternion.LookRotation(new Vector3(1f, 0f, 0f));
		// キャラクターコントローラーの取得
		Col = GetComponent ("CharacterController") as CharacterController;
	}
	
	// Update is called once per frame
	void Update () {
		// 左右入力で移動
		Velocity.x = Input.GetAxis ("Horizontal") * Speed;
		pawer = Input.GetAxis("Jump") * jumpPawer;
		Velocity.y += Physics.gravity.y * Time.deltaTime;
		Col.Move (Velocity * Time.deltaTime);

		// 慣性
		Velocity *= Inertia;
		// 地面に接していたら
		if(Col.isGrounded){
			transform.rotation = Quaternion.LookRotation(new Vector3(Velocity.x + 0.001f, 0f, 0f));
			Velocity.y = 0f;
		}// if

		if (Input.GetKey(KeyCode.Space) && Col.isGrounded) {
			Jump = true;
		}// if

		if (Jump) {
			CheckLanding ();
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
		
		Anim.SetBool (JumpID, Jump);
	}

	IEnumerator CheckLanding()
	{
		// 規定回数チェックして成功しない場合も着地モーションに移行する
		for(int count = 0;count < landingCheckLimit; count++)
		{
			var raycast = new RaycastHit();
			var raycastSuccess = Physics.Raycast(transform.position, Vector3.down, out raycast);
			// レイを飛ばして、成功且つ一定距離内であった場合、着地モーションへ移項させる
			if(raycastSuccess && raycast.distance < landingDistance) break;
			yield return new WaitForSeconds(waitTime);
		}
		Anim.speed = defaultSpeed;
	}

	void OnJumpStart(){
		defaultSpeed = Anim.speed;
		Velocity.y = pawer;
	}
	void OnJumpTopPoint(){
		Anim.speed = 0f;
		StartCoroutine(CheckLanding());
	}
	void OnJumpEnd(){
		Jump = false;
	}
	
}
