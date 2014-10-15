using UnityEngine;
using System.Collections;

public class enemyCtrl : MonoBehaviour {
	public enum ENEMY_STATE{
		ACTIVE = 0,
		DEAD,
		ENEMY_STATE_MAX
	}
	public enum ENEMY_TYPE{
		TYPE1 = 0,
		TYPE2,
		TYPE_MAX
	}
	public ENEMY_TYPE Type;
	public ENEMY_STATE State;
	public Vector3 Velocity;
	public float Speed;
	public GameObject Type2Item;


	// Use this for initialization
	void Start () {
		State = ENEMY_STATE.ACTIVE;
		Velocity.x = Speed;
	}

	// Update is called once per frame
	void Update () {
		if(State == ENEMY_STATE.ACTIVE){
			transform.Translate (Velocity);
			
			if(transform.position.y < -5){
				Destroy(gameObject);
			}
		}
		else if(State == ENEMY_STATE.DEAD){
			if(Type == ENEMY_TYPE.TYPE1){
				transform.localScale = new Vector3(1f, 0.5f, 1f);
				StartCoroutine(Dead(3f));
			}
			else if(Type == ENEMY_TYPE.TYPE2){
				Instantiate(Type2Item, transform.position, transform.rotation);
				Destroy (gameObject);

			}
		}

	}

	IEnumerator Dead(float time){
		yield return new WaitForSeconds(time);
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "NotBreakObject" || other.collider.tag == "ItemShoot") {
			Velocity = Vector3.Reflect(-Velocity, new Vector3(0, 1f, 0));
		}// if
		else if(other.gameObject.tag == "Enemy"){
			enemyCtrl ec = other.collider.GetComponent("enemyCtrl")as enemyCtrl;
			if(Velocity.x < ec.Velocity.x){
				Velocity.x *= -1;
			}
			else if(Velocity.x == ec.Velocity.x){
				Velocity.x *= -1;
			}
		}
	}

	public void SetState(ENEMY_STATE state){
		State = state;
	}

}
