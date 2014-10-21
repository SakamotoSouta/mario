using UnityEngine;
using System.Collections;

public class PlayerHitManager : MonoBehaviour {
	private PlayerController PlayerController;

	// Use this for initialization
	void Start () {
		PlayerController = GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//止まっているときのあたり判定
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "ItemShoot"){
			ItemShoot Shoot = other.collider.GetComponent("ItemShoot") as ItemShoot;
			// 甲羅が待機状態かつ自分が無敵でない
			if(Shoot.State == ItemShoot.ITEM_SHOOT_STATE.STAY && !PlayerController.Invincible){
				if(PlayerController.Velocity.x > 5f || PlayerController.Velocity.x < -5f){
					Shoot.State = ItemShoot.ITEM_SHOOT_STATE.SHOOT;
					Shoot.Velocity = new Vector3(Shoot.Speed * Vector3.Normalize(new Vector3(PlayerController.Velocity.x, 0f, 0f)).x, 0f, 0f);
				}
			}
			
			else if(Shoot.State == ItemShoot.ITEM_SHOOT_STATE.SHOOT){
				if(PlayerController.State != PlayerController.PLAYER_STATE.PLAYER_NORMAL){
					StartCoroutine(PlayerController.NotHitJudge(1, "Player", "ItemShoot"));
				}
				PlayerController.PlayerDamage();
			}
			else if(PlayerController.Invincible){
				Destroy(other.gameObject);
			}
		}
		// パワーアップ
		if(other.gameObject.tag == "ItemPawerUp"){
			PlayerController.PlayerGetItem(other.gameObject, ItemController.ITEM_TYPE.ITEM_PAWERUP);
		}

		// スター
		if(other.gameObject.tag == "ItemStar"){
			PlayerController.PlayerGetItem(other.gameObject, ItemController.ITEM_TYPE.ITEM_STAR);
		}
		
		if (other.gameObject.layer == LayerMask.NameToLayer ("Field") || other.gameObject.tag == "Block") {
			PlayerController.onGround = true;
		}
		else {
			PlayerController.onGround = false;
		}
		
	}
	// キャラクターコントローラーのあたり判定動いているとき
	void OnControllerColliderHit(ControllerColliderHit other){
		
		// 甲羅にあたった場合
		if(other.gameObject.tag == "ItemShoot"){
			ItemShoot Shoot = other.collider.GetComponent("ItemShoot") as ItemShoot;
			// 甲羅が待機状態かつ自分が無敵でない
			if(Shoot.State == ItemShoot.ITEM_SHOOT_STATE.STAY && !PlayerController.Invincible){
				if(PlayerController.Velocity.x > 5f || PlayerController.Velocity.x < -5f){
					Shoot.State = ItemShoot.ITEM_SHOOT_STATE.SHOOT;
					Shoot.Velocity = new Vector3(Shoot.Speed * Vector3.Normalize(new Vector3(PlayerController.Velocity.x, 0f, 0f)).x, 0f, 0f);
				}
			}
			// 甲羅にあたったとき（蹴った瞬間をはじくために速度も判断）
			else if(Shoot.State == ItemShoot.ITEM_SHOOT_STATE.SHOOT && Shoot.Velocity.x > 5f){
				if(PlayerController.State != PlayerController.PLAYER_STATE.PLAYER_NORMAL){
					StartCoroutine(PlayerController.NotHitJudge(1, "Player", "ItemShoot"));
				}
				PlayerController.PlayerDamage();
			}
			else if(PlayerController.Invincible){
				Destroy(other.gameObject);
			}
		}
		// パワーアップ
		if(other.gameObject.tag == "ItemPawerUp"){
			PlayerController.PlayerGetItem(other.gameObject, ItemController.ITEM_TYPE.ITEM_PAWERUP);
		}
		
		// スター
		if(other.gameObject.tag == "ItemStar"){
			PlayerController.PlayerGetItem(other.gameObject, ItemController.ITEM_TYPE.ITEM_STAR);
		}
		if (other.gameObject.layer == LayerMask.NameToLayer ("Field") || other.gameObject.tag == "Block") {
			PlayerController.onGround = true;
		}
		else {
			PlayerController.onGround = false;
		}
	}

}
