using UnityEngine;
using System.Collections;

public class ItemShoot : MonoBehaviour {
	public Vector3 Velocity;
	public float Speed;
	public GameObject Enemy;
	private enum ITEM_SHOOT_STATE{
		STAY,
		SHOOT,
		ITEM_SHOOT_STATE_MAX
	}

	ITEM_SHOOT_STATE State;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		switch(State){
			// 待機中
		case ITEM_SHOOT_STATE.STAY:
			StartCoroutine("Reborn");
			break;
		case ITEM_SHOOT_STATE.SHOOT:
			transform.Translate(Velocity);
			break;
		default:
			break;
		}
	}

	IEnumerator Reborn(){
		yield return new WaitForSeconds (30f);
		if(State != ITEM_SHOOT_STATE.STAY){
			yield break;
		}
		// 復活
		Instantiate(Enemy, transform.position, transform.rotation);
		Destroy (gameObject);

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "NotBreakObject") {
			Velocity = Vector3.Reflect(Velocity, new Vector3(-1f, 0f, 0f));
		}// if
		if(other.tag == "Player"){
			PlayerCtrl pc = other.GetComponent("PlayerCtrl") as PlayerCtrl;
			if(pc.Speed > 1f && State == ITEM_SHOOT_STATE.STAY){
				State = ITEM_SHOOT_STATE.SHOOT;
				Velocity = new Vector3(Speed * Vector3.Normalize(new Vector3(pc.Velocity.x, 0f, 0f)).x, 0f, 0f);
			}
		}
		if (other.tag == "Enemy" && State == ITEM_SHOOT_STATE.SHOOT) {
			enemyCtrl ec = other.GetComponent("enemyCtrl")as enemyCtrl;
			ec.Velocity = new Vector3(0f, 0f, -0.1f);
		}
	}
}
