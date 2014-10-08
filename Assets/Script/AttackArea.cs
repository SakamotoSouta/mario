using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
	GameObject Player;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		// キャラクターコントローラーを取得
		CharacterController cc = Player.GetComponent ("CharacterController")as CharacterController;
		// 地面についていないかつ敵にあたっている
		if(!cc.isGrounded && other.tag == "Enemy"){
			enemyCtrl ec = other.GetComponent("enemyCtrl")as enemyCtrl;
			ec.SetState(enemyCtrl.ENEMY_STATE.DEAD);
			PlayerCtrl pc = Player.GetComponent("PlayerCtrl")as PlayerCtrl;
			pc.Velocity.y += pc.jumpPawer / 2;
		}
	}
}
