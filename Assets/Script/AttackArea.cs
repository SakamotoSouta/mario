using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
	private GameObject Player;
	private PlayerCtrl pc;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		// キャラクターコントローラーを取得
		pc = Player.GetComponent("PlayerCtrl")as PlayerCtrl;
	}
	// Update is called once per frame
	void Update () {
		if (!pc.onGround) {
			RaycastHit hit;
			GameObject leg = GameObject.Find("Character1_LeftToeBase");
			Vector3 fromPos = new Vector3(leg.transform.position.x ,leg.transform.position.y, leg.transform.position.z + 0.01f);
			Vector3 direction = new Vector3(0, -1, 0);
			float length = 0.2f;
			// 下方向にレイを飛ばして判定
			Debug.DrawRay(fromPos, direction.normalized * length, Color.green, 1, false);
			if (Physics.Raycast(fromPos, direction, out hit, length)) {
				if(hit.collider.tag == "Enemy"){
					enemyCtrl ec = hit.collider.GetComponent("enemyCtrl")as enemyCtrl;
					ec.SetState(enemyCtrl.ENEMY_STATE.DEAD);
					
					pc.Velocity.y += pc.jumpPawer / 2;
				}
			}
		}	
	}


	void OnTriggerEnter(Collider other){
		if(other.collider.tag == "ItemShoot" && pc.Jump){
			ItemShoot Item = other.collider.GetComponent("ItemShoot") as ItemShoot;
			Item.SetState(ItemShoot.ITEM_SHOOT_STATE.STAY);
		}
	}

}
