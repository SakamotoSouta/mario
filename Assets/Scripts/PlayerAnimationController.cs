using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {
	// アニメーションID
	private Animator Anim;
	private int SpeedID;
	private int JumpID;
	private int DamageID;
	private int GoalID;
	// 着地判定を調べる回数
	private readonly int landingCheckLimit = 100;
	// 着地判定チェックを行う時間間隔
	private readonly float waitTime = 0.05F;
	// 着地モーションへの移項を許可する距離
	private readonly float landingDistance = 5F;

	// アニメーションのデフォルトの再生速度
	private float defaultSpeed;

	private PlayerController PlayerController;
	private GameObject GameRule;
	private GameRule Rule;

	// Use this for initialization
	void Start () {
		// ゲーム管理者の取得
		GameRule = GameObject.Find ("GameRule");
		Rule = GameRule.GetComponent ("GameRule") as GameRule;

		// プレイヤーコントローラーの取得
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		PlayerController = Player.GetComponent("PlayerController") as PlayerController;

		// アニメーションの取得
		Anim = GetComponent<Animator>();
		
		// アニメーションイベントの取得
		SpeedID = Animator.StringToHash ("Speed");
		JumpID = Animator.StringToHash ("Jump");
		DamageID = Animator.StringToHash ("Damage");
		GoalID = Animator.StringToHash ("Goal");	
	}
	
	// Update is called once per frame
	void Update () {
		AnimSet();
	}

	void AnimSet(){
		// アニメーションの設定
		if (PlayerController.Velocity.x < 0f) {
			Anim.SetFloat (SpeedID, PlayerController.Velocity.x * -1); 
		}// if
		else {
			Anim.SetFloat (SpeedID, PlayerController.Velocity.x); 
		}// else
		
		Anim.SetBool (DamageID, PlayerController.Damage);
		Anim.SetBool (JumpID, PlayerController.Jump);
		Anim.SetBool (GoalID, PlayerController.Goal);
	}

	// 着地判定
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

	void OnDead(){
		Anim.speed = 0f;
		if (!Rule.endFlag) {
			StartCoroutine(Rule.Restart());
			Rule.endFlag = true;
		}

	}

	// アニメーションイベント
	void OnJumpStart(){
		defaultSpeed = Anim.speed;
		PlayerController.Velocity.y = PlayerController.jumpPawer;
	}
	void OnJumpTopPoint(){
		Anim.speed = 0f;
		StartCoroutine(CheckLanding());
	}
	
	void OnJumpHitEnd(){
		PlayerController.Jump = false;
	}
	void OnJumpEnd(){
		PlayerController.Jump = false;
	}

	void AnimEnd(){
		StartCoroutine (Rule.ClearGame());
	}

}
