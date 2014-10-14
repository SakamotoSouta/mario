using UnityEngine;
using System.Collections;

public class ItemCoin : MonoBehaviour {
	public enum COIN_TYPE{
		BLOCK_COIN = 0,
		OBJECT_COIN,
		COIN_TYPE_MAX
	}

	public COIN_TYPE coinType;
	private float Counter = 0;
	public GameObject GameRule;
	private GameRule Rule;

	// Use this for initialization
	void Start () {
		GameRule = GameObject.Find("GameRule");
		Rule = GameRule.GetComponent ("GameRule") as GameRule;
		if (coinType == COIN_TYPE.BLOCK_COIN) {
			CapsuleCollider hit = gameObject.GetComponent("CapsuleCollider") as CapsuleCollider;
			hit.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0.01f, 0);
		switch(coinType){
		case COIN_TYPE.BLOCK_COIN:
			Counter+=0.1f;
			transform.Translate(0, 0, Mathf.Sin(Counter) * -0.05f);
			if(Counter > Mathf.PI * 2){
				Destroy(gameObject);
			}
			break;
		case COIN_TYPE.OBJECT_COIN:
			break;
		default:
			break;
		}
	
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			if(coinType == COIN_TYPE.OBJECT_COIN){
				Rule.GetCoin();
				Destroy(gameObject);
			}
		}

	}
}
