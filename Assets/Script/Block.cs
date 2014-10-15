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
	private bool Move = false;
	private float count = 0;

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
		if (Move) {
			count += 0.1f;
			transform.Translate(0,Mathf.Sin(count) * 0.01f,0);
			if(count > Mathf.PI * 2){
				Move = false;
				count = 0;
			}
		}
	}

	public void HitBlock(){
		// ブロックにあたった場合タイプによって処理を変える
		switch (BlockType) {
		case BLOCK_TYPE.ITEM_BLOCK:
			HitItemBlock();
			break;
		case BLOCK_TYPE.BLREAK_BLOCK:
			HitBreakBlock();
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
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		PlayerCtrl pc = Player.GetComponent ("PlayerCtrl") as PlayerCtrl;
		if (pc.State != PlayerCtrl.PLAYER_STATE.PLAYER_NORMAL) {
			Destroy (gameObject);
		}
		else {
			Move = true;
		}
	}

	void HitNoneBlock(){

	}
}
