using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
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
		if (inPipe && !pc.Bonus) {
			inPipe = false;
			pc.Bonus = true;
			// あたり判定を戻す
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pipe"), false);
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
			Rule.PipeInChangeScene();
		}
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

				//　パイプに乗っているときに使途を押した場合
				if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Pipe") && !pc.Bonus){
					if(Input.GetKeyDown(KeyCode.DownArrow) && !pc.waitPipe){
						// レイヤーのあたり判定を消す
						Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Pipe"));
						Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
						pc.transform.position = new Vector3(hit.collider.transform.position.x, pc.transform.position.y, hit.collider.transform.position.z);	
						pc.transform.rotation = Quaternion.Euler(0, 180, 0);
						pc.Velocity = new Vector3(0, 0, 0);
						pc.waitPipe = true;
					}
				}
			}
		}	

		// パイプに入り切ったとき
		if(pc.waitPipe && !pc.Bonus){
			if(pc.transform.position.y < -3.49503  && !pc.outPipe){
				inPipe = true;
				pc.waitPipe = false;
			}
			// パイプからでる処理
			else if(pc.transform.position.y > -0.7715993f && pc.outPipe){
				pc.waitPipe = false;
				pc.outPipe = false;
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
