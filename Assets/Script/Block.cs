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
	public BLOCK_TYPE BlockType;
	public ItemControler.ITEM_TYPE ItemType; 

	// Use this for initialization
	void Start () {

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
		GameObject Root= GameObject.Find ("ItemRoot");
		ItemControler ItemCtrl = Root.GetComponent ("ItemControler") as ItemControler;
		ItemCtrl.GenerateItem (ItemType, transform.position);
		Debug.Log ("" + transform.position);
		BlockType = BLOCK_TYPE.NONE_BLOCK;
	}

	void HitBreakBlock(){
		Destroy (gameObject);
	}

	void HitNoneBlock(){

	}
}
