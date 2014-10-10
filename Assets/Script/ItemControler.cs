using UnityEngine;
using System.Collections;

public class ItemControler : MonoBehaviour {
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

		GameObject root = GameObject.Find ("ItemRoot");
		switch(Item){
			// アイテムの種類判断
		// パワーアップアイテムはプレイヤーのステートを見て判断とりあえずキノコ
		case ITEM_TYPE.ITEM_PAWERUP:
			ItemObject = ItemMushroomPrefab; 
			break;
		case ITEM_TYPE.ITEM_STAR:
			ItemObject = ItemStarPrefab;
			break;
		case ITEM_TYPE.ITEM_COIN:
			ItemObject = ItemCoinPrefab;
			break;

		}
		GameObject item = Instantiate (ItemObject, position, transform.rotation) as GameObject;
		item.transform.parent = root.transform;

	}
}
