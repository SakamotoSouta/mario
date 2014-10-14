using UnityEngine;
using System.Collections;

public class ItemStar : MonoBehaviour {
	private Vector3 Velocity;
	GameObject Player;
	PlayerCtrl pc;
	private ParticleSystem effect;

	// Use this for initialization
	void Start () {
		effect = transform.Find ("InvinsibleEffect").GetComponent<ParticleSystem> ();
		effect.Play();
		Player = GameObject.FindGameObjectWithTag("Player");
		pc = Player.GetComponent("PlayerCtrl") as PlayerCtrl;
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

	void OnTriggerEnter(Collider other){
		if(other.tag == "Enemy" || other.tag == "NotBreakObject" || other.tag == "Block"){
			Velocity.x *= -1;
		}
		if (other.tag == "Player") {
			pc.GetStar();
			Destroy(gameObject);
		}
	}
}
