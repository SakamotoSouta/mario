using UnityEngine;
using System.Collections;

public class ItemStar : MonoBehaviour {
	private Vector3 Velocity;
	private ParticleSystem effect;
	private Rigidbody Rb;
	// Use this for initialization
	void Start () {
		effect = transform.Find ("energyBlast").GetComponent<ParticleSystem> ();
		effect.Play();
		Rb = gameObject.GetComponent ("Rigidbody") as Rigidbody;
		Velocity = new Vector3 (-0.1f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		transform.Translate (Velocity);

		if(transform.position.y < -5){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision other){
		Velocity.x *= -1;
		Rb.AddForce (0, 400f, 0);

	}
}
