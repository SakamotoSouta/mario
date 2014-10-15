using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {
	// ブロックの種類
	public enum ITEM_TYPE{
		ITEM_PAWERUP = 0,
		ITEM_STAR,
		ITEM_COIN,
		ITEM_MAX
	}
	private GameObject ItemObject;
	public GameObject ItemMushroomPrefab;
	public GameObject ItemStarPrefab;
	public GameObject ItemFlowerPrefab;
	public GameObject ItemCoinPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	// 生成
	public void GenerateItem(ITEM_TYPE Item, Vector3 position){
		position.y += 1;
		GameObject root = GameObject.Find ("ItemRoot");
		switch(Item){
			// アイテムの種類判断
		case ITEM_TYPE.ITEM_PAWERUP:
			GameObject Player = GameObject.FindGameObjectWithTag("Player");
			PlayerCtrl pc = Player.GetComponent("PlayerCtrl") as PlayerCtrl;
			if(pc.State == PlayerCtrl.PLAYER_STATE.PLAYER_NORMAL){
				ItemObject = ItemMushroomPrefab; 
			}
			else{
				ItemObject = ItemFlowerPrefab;
			}
			break;
		case ITEM_TYPE.ITEM_STAR:
			ItemObject = ItemStarPrefab;
			break;
		case ITEM_TYPE.ITEM_COIN:
			ItemObject = ItemCoinPrefab;
			break;

		}
		GameObject item = Instantiate (ItemObject, position, ItemObject.transform.rotation) as GameObject;
		item.transform.parent = root.transform;

	}

	public void GetItem(ITEM_TYPE Item){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerCtrl pc = Player.GetComponent ("PlayerCtrl") as PlayerCtrl;
		switch(Item){
		case ITEM_TYPE.ITEM_PAWERUP:
			pc.PlayerPawerUp();
			break;
		case ITEM_TYPE.ITEM_COIN:
			GameObject GameRule = GameObject.Find("GameRule");
			GameRule Rule = GameRule.GetComponent("GameRule") as GameRule;

			Rule.GetCoin();
			Destroy(gameObject);
			break;
		case ITEM_TYPE.ITEM_STAR:
			pc.GetStar();
			break;
		default:
			break;
		}

	}
}
