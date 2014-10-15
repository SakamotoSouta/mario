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
	
	}
	// あたり判定
	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			PlayerCtrl pc = Player.GetComponent("PlayerCtrl")as PlayerCtrl;
			StartCoroutine(pc.NotHitJudge(1, "Player", "Enemy"));
			if(!pc.Invincible){
				pc.PlayerDamage();
			}
			else{
				Destroy(other.gameObject);
			}
		}
	}
}
