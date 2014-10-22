using UnityEngine;
using System.Collections;

public class ItemStar : MonoBehaviour {
	private Vector3 Velocity;
	private ParticleSystem effect;

	// Use this for initialization
	void Start () {
		effect = transform.Find ("InvinsibleEffect").GetComponent<ParticleSystem> ();
		effect.Play();
		Rigidbody Rb = gameObject.GetComponent("Rigidbody") as Rigidbody;
		Rb.AddForce (100f, 500f, 0);
		Velocity = new Vector3 (0.1f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Velocity);

		if(transform.position.y < -5){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "NotBreakObject" || other.gameObject.tag == "Block"){
			Velocity.x *= -1;
		}
	}
}
