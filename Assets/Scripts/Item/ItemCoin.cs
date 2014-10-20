using UnityEngine;
using System.Collections;

public class ItemCoin : MonoBehaviour {
	public enum COIN_TYPE{
		BLOCK_COIN = 0,
		OBJECT_COIN,
		COIN_TYPE_MAX
	}


	public COIN_TYPE coinType;
	private GameObject ItemRoot;
	private ItemController ItemController;
	private float Counter = 0;


	// Use this for initialization
	void Start () {
		// アイテム管理者の取得
		ItemRoot = GameObject.Find("ItemRoot");
		ItemController = ItemRoot.GetComponent ("ItemController") as ItemController;

		if (coinType == COIN_TYPE.BLOCK_COIN) {
			CapsuleCollider hit = gameObject.GetComponent("CapsuleCollider") as CapsuleCollider;
			hit.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f, 0f, 5f);
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

	// あたった瞬間
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			ItemController.GetItem(ItemController.ITEM_TYPE.ITEM_COIN);
			Destroy(gameObject);
		}
	}
}
