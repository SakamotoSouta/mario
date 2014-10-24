using UnityEngine;
using System.Collections;

public class HitArea : MonoBehaviour {
	private GameObject Player;
	private PlayerController pc;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		pc = Player.GetComponent ("PlayerController")as PlayerController;
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody.WakeUp ();
	}
	// あたり判定
	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy" && !pc.Damage) {

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
