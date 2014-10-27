using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {
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
	private Animator Anim;
	private int SpeedID;

	public ENEMY_TYPE Type;
	public ENEMY_STATE State;
	public Vector3 Velocity;
	public float Speed;
	public GameObject Type2Item;
	private Vector3 Look;
	Rigidbody physics;
	// Use this for initialization
	void Start () {
		Anim = GetComponent<Animator> ();
		SpeedID = Animator.StringToHash ("Speed"); 

		State = ENEMY_STATE.ACTIVE;
		Velocity.z = Speed;
		physics = GetComponent<Rigidbody> ();
		Look = new Vector3 (-1, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (Application.loadedLevelName != "FieldCreateTool") {
								
			physics.isKinematic = false;
				
			if (transform.position.y < -5) {
				Destroy (gameObject);
			
			}
		
		
			if (State == ENEMY_STATE.DEAD) {
				if (Type == ENEMY_TYPE.TYPE1) {
					transform.localScale = new Vector3 (transform.localScale.x, 1.5f, transform.localScale.z);	
					StartCoroutine (Dead (3f));
				} 
			
				else if (Type == ENEMY_TYPE.TYPE2) {
					Instantiate (Type2Item, transform.position, Quaternion.Euler(0,0,0));	
					Destroy (gameObject);
				}
			}
			else if(State != ENEMY_STATE.DEAD){
				transform.Translate (Velocity);
				if(Velocity.z > 0){
					Anim.SetFloat (SpeedID, Velocity.z);
				}
				else{
					Anim.SetFloat (SpeedID, Velocity.z * -1);
				}
				transform.rotation = Quaternion.LookRotation (Look);
			}
			if (transform.position.y < -5) {
				Destroy (gameObject);
				
			}

		}
		else{
			physics.isKinematic = true;
		}


	}

	IEnumerator Dead(float time){
		gameObject.layer = LayerMask.NameToLayer("UI");
		gameObject.tag = "Floor";
		yield return new WaitForSeconds(time);
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "NotBreakObject" || other.collider.tag == "ItemShoot") {
			Look.x *= -1;
		}// if
		else if(other.gameObject.tag == "Enemy"){
			enemyController ec = other.collider.GetComponent("enemyController")as enemyController;
			if(Velocity.z < ec.Velocity.z){
				Look.x *= -1;
			}
			else if(Velocity.x == ec.Velocity.z){
				Look.x *= -1;
			}
		}
	}

	public void SetState(ENEMY_STATE state){
		State = state;
	}

}
