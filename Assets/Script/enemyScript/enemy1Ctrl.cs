using UnityEngine;
using System.Collections;

public class enemy1Ctrl : MonoBehaviour {
	private Vector3 Velocity;
	public float Speed;

	// Use this for initialization
	void Start () {
		Velocity.x = Speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Velocity);
		if(transform.position.y < -5){
			Destroy(this);
		}
	}

	private void OnTriggerEnter(Collider other){
		if (other.tag == "NotBreakObject") {
			Velocity.x *= -1;
		}// if
	}
}
