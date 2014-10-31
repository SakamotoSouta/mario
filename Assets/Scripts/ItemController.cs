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
	// SE管理者
	private GameObject SEControllerObject;
	private SEController SE;
	// Use this for initialization
	void Start () {
		// SEの取得
		SEControllerObject = GameObject.Find("SEController");
		SE = SEControllerObject.GetComponent("SEController") as SEController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	// 生成
	public void GenerateItem(ITEM_TYPE Item, Vector3 position){
		position.y += 1;
		var root = GameObject.Find ("ItemRoot");
		switch(Item){
			// アイテムの種類判断
		case ITEM_TYPE.ITEM_PAWERUP:
			SE.SEPlay(SEController.SE_LABEL.SE_PAWERUPITEM_GENERATE);
			var Player = GameObject.FindGameObjectWithTag("Player");
			var pc = Player.GetComponent("PlayerController") as PlayerController;
			if(pc.State == PlayerController.PLAYER_STATE.PLAYER_NORMAL){
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
		var item = Instantiate (ItemObject, position, ItemObject.transform.rotation) as GameObject;
		item.transform.parent = root.transform;

	}

	public void GetItem(ITEM_TYPE Item){
		var Player = GameObject.FindGameObjectWithTag ("Player");
		var pc = Player.GetComponent ("PlayerController") as PlayerController;
		switch(Item){
		case ITEM_TYPE.ITEM_PAWERUP:
			pc.PlayerPawerUp();
			break;
		case ITEM_TYPE.ITEM_COIN:
			var GameRule = GameObject.Find("GameRule");
			var Rule = GameRule.GetComponent("GameRule") as GameRule;
			SE.SEPlay(SEController.SE_LABEL.SE_COIN);
			Rule.GetCoin();
			break;
		case ITEM_TYPE.ITEM_STAR:
			pc.GetStar();
			break;
		default:
			break;
		}

	}
}
