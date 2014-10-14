using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
	GameObject Player;
	PlayerCtrl pc;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		// キャラクターコントローラーを取得
		pc = Player.GetComponent("PlayerCtrl")as PlayerCtrl;
	}
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		// 地面についていないかつ敵にあたっている
		if(pc.Jump && other.tag == "Enemy"){
			enemyCtrl ec = other.GetComponent("enemyCtrl")as enemyCtrl;
			ec.SetState(enemyCtrl.ENEMY_STATE.DEAD);

			pc.Velocity.y += pc.jumpPawer / 2;
		}
		else if(pc.Jump && other.tag == "ItemShoot"){
			ItemShoot Item = other.GetComponent("ItemShoot") as ItemShoot;
			Item.SetState(ItemShoot.ITEM_SHOOT_STATE.STAY);
		}
	}
}
