using UnityEngine;
using System.Collections;

public class ItemMushroom : MonoBehaviour {
	private Vector3 Velocity;

	// Use this for initialization
	void Start () {
		Velocity = new Vector3 (0.05f, 0.0f ,0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.WakeUp ();
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		transform.Translate (Velocity);

		if(transform.position.y < -5){
			Destroy(gameObject);
		}
	}

	// あたり判定
	void OnCollisionEnter(Collision other){
		if(other.collider.tag == "NotBreakObject" || other.collider.tag == "Enemy"){
			Velocity.x *= -1;
		}

	}
}
