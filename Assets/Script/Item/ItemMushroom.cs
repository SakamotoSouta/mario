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
		transform.Translate (Velocity);

		if(transform.position.y < -5){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "NotBreakObject" || other.tag == "Enemy"){
			Velocity.x *= -1;
		}
		// プレイヤーと当ったら消去
		if(other.tag == "Player"){
			GameObject Player = GameObject.FindGameObjectWithTag("Player");
			PlayerCtrl pc = Player.GetComponent("PlayerCtrl") as PlayerCtrl;
			pc.PlayerPawerUp();
			Destroy(gameObject);
		}
	}
}
