﻿using UnityEngine;
using System.Collections;

public class BonusPipe : MonoBehaviour {
	private GameObject Player;
	private PlayerCtrl pc;
	GameObject GameRuleObject;
	GameRule Rule;
	[HideInInspector]
	public bool inPipe = false;

	// Use this for initialization
	void Start () {
		GameRuleObject = GameObject.Find ("GameRule");
		Rule = GameRuleObject.GetComponent ("GameRule") as GameRule;
		Player = GameObject.FindGameObjectWithTag("Player");
		// キャラクターコントローラーを取得
		pc = Player.GetComponent("PlayerCtrl")as PlayerCtrl;	
	}
	
	// Update is called once per frame
	void Update () {
		// パイプアクションが終わったら
		if (inPipe && pc.Bonus) {
			inPipe = false;
			pc.Bonus = false;
			// あたり判定を戻す
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pipe"), false);
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

			Rule.PipeOutChangeScene();
			GameObject Delete = GameObject.Find("DeleteObject");
			Destroy(Delete);

		}

		RaycastHit hit;
		Vector3 fromPos = new Vector3(transform.position.x ,transform.position.y, transform.position.z + 0.01f);
		Vector3 direction = new Vector3(-1, 0, 0);
		float length = 2.0f;
		// 下方向にレイを飛ばして判定
		Debug.DrawRay(fromPos, direction.normalized * length, Color.green, 1, false);
		if (Physics.Raycast(fromPos, direction, out hit, length)) {		
			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") && pc.Bonus){
				if(Input.GetKeyDown(KeyCode.RightArrow) && !pc.waitPipe){
					// レイヤーのあたり判定を消す
					Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pipe"));
					pc.waitPipe = true;
					pc.transform.rotation = Quaternion.Euler( 0, 90, 0);
					pc.Velocity = new Vector3(0, 0, 0);
				}
			}
		}

		if(pc.transform.position.x > 15f && pc.waitPipe && pc.Bonus){
			inPipe = true;
		}
	}
}