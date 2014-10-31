using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	private Vector3 Velocity;
	private ParticleSystem effect;
	public float Speed;

	// Use this for initialization
	void Start () {
		effect = transform.Find ("fx_fumefx_fireball").GetComponent<ParticleSystem> ();
		effect.Play();
		var Rb = gameObject.GetComponent("Rigidbody") as Rigidbody;
		Rb.AddForce (100f, -200f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Velocity);

		if(transform.position.y < -5){
			Destroy(gameObject);
		}	
	}

	public void GenerateFireBall(Vector3 frontVector){
		Velocity = frontVector * Speed;	
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "NotBreakObject" || other.gameObject.tag == "Block"){
			Destroy(gameObject);
		}
		if(other.collider.tag == "Enemy"){
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
