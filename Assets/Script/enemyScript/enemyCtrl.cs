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
	private Vector3 Velocity;
	public float Speed;
	public GameObject Type2Item;
	private Vector3 oldPosition;


	// Use this for initialization
	void Start () {
		State = ENEMY_STATE.ACTIVE;
		Velocity.x = Speed;
	}

	// Update is called once per frame
	void Update () {
		if(State == ENEMY_STATE.ACTIVE){
			if(Input.GetKey(KeyCode.Q)){
				State = ENEMY_STATE.DEAD;
			}
			oldPosition = transform.position;
			transform.Translate (Velocity);
			transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
			
			if(transform.position.y < -5){
				Destroy(gameObject);
			}
		}
		else if(State == ENEMY_STATE.DEAD){
			if(Type == ENEMY_TYPE.TYPE1){
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

	private void OnTriggerEnter(Collider other){
		if (other.tag == "NotBreakObject" || other.tag == "Enemy") {
			Velocity = Vector3.Reflect(-Velocity, new Vector3(0f, -1, 0));
			transform.position = oldPosition;
		}// if
		else if(other.tag == "Floor"){
			Velocity.y = -1;
		}
	}

}
