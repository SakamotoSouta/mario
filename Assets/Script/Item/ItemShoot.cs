using UnityEngine;
using System.Collections;

public class ItemShoot : MonoBehaviour {
	private float rebornCounter = 0;
	public Vector3 Velocity;
	public float Speed;
	public GameObject Enemy;
	public enum ITEM_SHOOT_STATE{
		STAY,
		SHOOT,
		ITEM_SHOOT_STATE_MAX
	}

	public ITEM_SHOOT_STATE State;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		switch(State){
			// 待機中
		case ITEM_SHOOT_STATE.STAY:
			rigidbody.WakeUp();
			Velocity = new Vector3(0, 0, 0);
			rebornCounter+=Time.deltaTime;
			if(rebornCounter > 30){
				// 復活
				Instantiate(Enemy, transform.position, transform.rotation);
				Destroy (gameObject);
			}
			break;
		case ITEM_SHOOT_STATE.SHOOT:
			rebornCounter = 0;
			transform.Translate(Velocity);
			break;
		default:
			break;
		}

		if(transform.position.y < -5){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision other){
		// フィールドオブジェクトにあたった場合跳ね返る
		if (other.gameObject.tag == "NotBreakObject") {
			Velocity = Vector3.Reflect(Velocity, new Vector3(-1f, 0f, 0f));
		}// if


		if (other.gameObject.tag == "Enemy" && State == ITEM_SHOOT_STATE.SHOOT) {
			enemyCtrl ec = other.collider.GetComponent("enemyCtrl")as enemyCtrl;
			ec.Velocity = new Vector3(0f, 0f, -0.1f);
		}
	}

	public void SetState(ITEM_SHOOT_STATE state){
		State = state;
	}
}
