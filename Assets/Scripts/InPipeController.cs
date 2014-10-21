using UnityEngine;
using System.Collections;

public class InPipeController : MonoBehaviour {

	public PipeActionController.IN_KEY inKey;
	// パイプに入った先のシーンの名前
	public string NextScene;

	private PipeActionController PipeAction;

	private bool InFlag = false;
	private RaycastHit hit;
	private Vector3 fromPos;
	public Vector3 direction;
	private float length = 1.0f;
	private KeyCode key;
	private Vector3 outPosition;
	private Vector3 lookAt;

	// Use this for initialization
	void Start () {
		PipeAction = GetComponent <PipeActionController>();
		inKey = PipeAction.InKey;
		outPosition = PipeAction.outPosition;
		NextScene = PipeAction.InPipeNextScene;

		switch(inKey){
		case PipeActionController.IN_KEY.DOWN:
			direction = new Vector3(0, 1, 0);
			break;
		case PipeActionController.IN_KEY.UP:
			direction = new Vector3(0, -1, 0);
			break;
		case PipeActionController.IN_KEY.RIGHT:
			direction = new Vector3(-1, 0, 0);
			break;
		case PipeActionController.IN_KEY.LEFT:
			direction = new Vector3(0, 1, 0);
			break;
		default:
			break;
		}
		fromPos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		// パイプに入る処理
		Debug.DrawRay(fromPos, direction.normalized * length, Color.red, 1, false);
		if (Physics.Raycast(fromPos, direction, out hit, length)) {
			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
				switch(inKey){
				case PipeActionController.IN_KEY.DOWN:
					key = KeyCode.DownArrow;
					lookAt = new Vector3(0, 180, 0);
					break;
				case PipeActionController.IN_KEY.UP:
					key = KeyCode.UpArrow;
					lookAt = new Vector3(0, 180, 0);
					break;
				case PipeActionController.IN_KEY.RIGHT:
					lookAt = new Vector3(0, 90, 0);
					key = KeyCode.RightArrow;
					break;
				case PipeActionController.IN_KEY.LEFT:
					lookAt = new Vector3(0, 270, 0);
					key = KeyCode.LeftArrow;
					break;
				default:
					break;
				}
				if(Input.GetKeyDown(key)){
					InFlag = true;
					PipeAction.SetPipeAction(lookAt);
				}
			}
		}

		if(!PipeAction.GetPipeFlag() && InFlag){
			InFlag = false;
			InPipeChangeScene();
		}

	}


	void InPipeChangeScene(){
		GameObject GameRuleObject = GameObject.Find ("GameRule");
		GameRule Rule = GameRuleObject.GetComponent ("GameRule") as GameRule;
		Rule.PipeInChangeScene (outPosition, NextScene);
	}
}
