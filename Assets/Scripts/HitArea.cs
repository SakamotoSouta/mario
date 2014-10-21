using UnityEngine;
using System.Collections;

public class HitArea : MonoBehaviour {
	GameObject Player;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.WakeUp ();
	}
	// あたり判定
	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			PlayerController pc = Player.GetComponent("PlayerController")as PlayerController;
			if(!pc.Invincible){
				if(pc.State != PlayerController.PLAYER_STATE.PLAYER_NORMAL){
					StartCoroutine(pc.NotHitJudge(1f, "Player", "Enemy"));
				}
				pc.PlayerDamage();
			}
			else{
				Destroy(other.gameObject);
			}
		}
	}
}
