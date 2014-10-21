using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
	private GameObject Player;
	private PlayerController pc;
	GameObject GameRuleObject;
	GameRule Rule;
	[HideInInspector]
	public bool inPipe = false;
	private RaycastHit hit;
	private GameObject leg ;
	private Vector3 fromPos;
	private Vector3 direction;
	private float length = 0.2f;
	// Use this for initialization
	void Start () {
		GameRuleObject = GameObject.Find ("GameRule");
		Rule = GameRuleObject.GetComponent ("GameRule") as GameRule;

		Player = GameObject.FindGameObjectWithTag("Player");
		// キャラクターコントローラーを取得
		pc = Player.GetComponent("PlayerController")as PlayerController;

		leg = GameObject.Find("Character1_LeftToeBase");
		direction = new Vector3 (0, -1, 0);

	}

	// Update is called once per frame
	void Update () {

		if (!pc.onGround) {

			// 下方向にレイを飛ばして判定
			fromPos = new Vector3(leg.transform.position.x ,leg.transform.position.y, leg.transform.position.z + 0.01f);
			Debug.DrawRay(fromPos, direction.normalized * length, Color.green, 1, false);

			if (Physics.Raycast(fromPos, direction, out hit, length)) {
				if(hit.collider.tag == "Enemy"){
					enemyController ec = hit.collider.GetComponent("enemyController")as enemyController;
					ec.SetState(enemyController.ENEMY_STATE.DEAD);

					pc.Velocity.y += pc.jumpPawer / 2;
				}
			}
		}	

	}

	// 触れた瞬間
	void OnTriggerEnter(Collider other){
		if(other.collider.tag == "ItemShoot" && pc.Jump){
			ItemShoot Item = other.collider.GetComponent("ItemShoot") as ItemShoot;
			Item.SetState(ItemShoot.ITEM_SHOOT_STATE.STAY);
		}
	}
}
