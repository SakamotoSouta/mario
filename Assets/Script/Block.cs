using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

	// ブロックの種類
	public enum BLOCK_TYPE{
		ITEM_BLOCK = 0,
		BLREAK_BLOCK,
		NONE_BLOCK,
		BLOCK_MAX
	}
	public int BlockLife;
	public BLOCK_TYPE BlockType;
	public ItemController.ITEM_TYPE ItemType; 

	// Use this for initialization
	void Start () {
		if (ItemType == ItemController.ITEM_TYPE.ITEM_COIN) {
			BlockLife = Random.Range(1, 5);
		}
		else {
			BlockLife = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void HitBlock(){
		// ブロックにあたった場合タイプによって処理を変える
		switch (BlockType) {
		case BLOCK_TYPE.ITEM_BLOCK:
			HitItemBlock();
			break;
		case BLOCK_TYPE.BLREAK_BLOCK:
			break;
		case BLOCK_TYPE.NONE_BLOCK:
			break;
		default:
			break;
		}
	}

	void HitItemBlock(){
		BlockLife--;
		if (ItemType == ItemController.ITEM_TYPE.ITEM_COIN) {
			GameObject GameRule = GameObject.Find("GameRule");
			GameRule Rule = GameRule.GetComponent("GameRule") as GameRule;
			Rule.GetCoin();
		}
		GameObject Root= GameObject.Find ("ItemRoot");
		ItemController ItemCtrl = Root.GetComponent ("ItemController") as ItemController;
		ItemCtrl.GenerateItem (ItemType, transform.position);
		if (BlockLife == 0) {
			gameObject.renderer.material.color = new Color(1, 1, 1, 0);
			BlockType = BLOCK_TYPE.NONE_BLOCK;
		}
	}

	void HitBreakBlock(){
		Destroy (gameObject);
	}

	void HitNoneBlock(){

	}
}
