using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
	private GameObject Player;
	private PlayerController pc;
	private GameObject GameRuleObject;
	private RaycastHit hit;
	private GameObject leg ;
	private Vector3 fromPos;
	private Vector3 direction;
	private float length = 0.2f;
	// SE
	private GameObject SEControllerObject;
	private SEController se;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		// キャラクターコントローラーを取得
		pc = Player.GetComponent("PlayerController")as PlayerController;

		leg = GameObject.Find("Character1_Reference");
		direction = new Vector3 (0, -1, 0);
		SEControllerObject = GameObject.Find("SEController");
		se = SEControllerObject.GetComponent("SEController") as SEController;
	}

	// Update is called once per frame
	void Update () {

		if (!pc.onGround) {

			// 下方向にレイを飛ばして判定
			fromPos = new Vector3(leg.transform.position.x ,leg.transform.position.y, leg.transform.position.z + 0.01f);
			Debug.DrawRay(fromPos, direction.normalized * length, Color.green, 1, false);

			if (Physics.Raycast(fromPos, direction, out hit, length)) {
				if(hit.collider.tag == "Enemy"){
					var ec = hit.collider.GetComponent("enemyController")as enemyController;
					ec.SetState(enemyController.ENEMY_STATE.DEAD);
					// SEの再生
					se.SEPlay(SEController.SE_LABEL.SE_TREAD);
					pc.Velocity.y += pc.jumpPawer / 2;
				}
			}
			if (Physics.Raycast(fromPos, direction, out hit, length)) {
				if(hit.collider.tag == "ItemShoot"){
					var Item = hit.collider.GetComponent("ItemShoot") as ItemShoot;
					Item.SetState(ItemShoot.ITEM_SHOOT_STATE.STAY);
					// SEの再生
					se.SEPlay(SEController.SE_LABEL.SE_TREAD);
				}
			}
		}	

	}

	// 触れた瞬間
	void OnTriggerEnter(Collider other){
		if(other.collider.tag == "ItemShoot" && pc.Jump){
			var Item = other.collider.GetComponent("ItemShoot") as ItemShoot;
			Item.SetState(ItemShoot.ITEM_SHOOT_STATE.STAY);
			// SEの再生
			se.SEPlay(SEController.SE_LABEL.SE_TREAD);
		}
	}
}
