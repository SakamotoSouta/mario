using UnityEngine;
using UnityEditor;
using System.Collections;


public class ToolController : MonoBehaviour {
	const float RayCastMaxDistance = 100.0f;
	public GameObject Parent;
	// 現在のプレハブ
	public GameObject Prefab;
	// プレハブ
	public GameObject fieldPrefab;
	public GameObject stairPrefab;
	public GameObject breakBlockPrefab;
	public GameObject enemy1Prefab;
	public GameObject enemy2Prefab;
	public GameObject goalPrefab;
	public GameObject itemBlockPrefab;
	public GameObject pipePrefab;

	// さくじょ
	private bool deleteFlag = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Application.Quit();
		// 親が自邸されてない場合終了する
		if (!Parent) {
			Application.Quit();
		}
		// クリックされたとき
		if(Prefab != null){
			if(Input.GetButton("Fire1")){
				// マウスのポジションを取得
				Vector2 clickPosition = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(clickPosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, RayCastMaxDistance)){
					// あたった
					if(hit.collider.tag == "GridCollider"){
						if(!deleteFlag){
							GameObject fieldObject =  (GameObject)Instantiate(Prefab, hit.collider.transform.position, transform.rotation);
							fieldObject.transform.parent = Parent.transform;
						}
					}
					else{
						if(deleteFlag){
							Destroy(hit.collider.gameObject);
						}
					}
				}
			}
		}
	}

	void OnSelectionChange(string val){
		switch(val){
		case "Field":
			Prefab = fieldPrefab;
			deleteFlag = false;
			break;
		case "Stair":
			Prefab = stairPrefab;
			deleteFlag = false;
			break;
		case "BreakBlock":
			Prefab = breakBlockPrefab;
			deleteFlag = false;
			break;
		case "Enemy1":
			Prefab = enemy1Prefab;
			deleteFlag = false;
			break;
		case "Enemy2":
			Prefab = enemy2Prefab;
			deleteFlag = false;
			break;
		case "Goal":
			Prefab = goalPrefab;
			deleteFlag = false;
			break;
		case "ItemBlock":
			Prefab = itemBlockPrefab;
			deleteFlag = false;
			break;
		case "Pipe":
			Prefab = pipePrefab;
			deleteFlag = false;
			break;
		case "Delete":
			deleteFlag = true;
			break;
		default:
			Prefab = null;
			break;
		}
	}

	// 入力された名前をポップアップリストに追加（いつか使うかも）
	void OnSubmit(string val){
		//popUpListScript.items.Add(val);
	}


	void OnSaveButton(){
		Debug.Log ("Save");
		Object oldPrefab = PrefabUtility.GetPrefabParent (Parent);

		PrefabUtility.ReplacePrefab (Parent, oldPrefab);
	}
}

