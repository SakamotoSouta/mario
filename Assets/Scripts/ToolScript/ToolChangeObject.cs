using UnityEngine;
using System.Collections;

public class ToolChangeObject : MonoBehaviour
{
	// 置き換え後の素材
	public GameObject Prefab;
	public GameObject Parent;


	// Use this for initialization
	void Start()
	{
		if(Application.loadedLevelName != "FieldCreateTool"){
			if(!Parent){
				Parent = GameObject.Find("FieldObjectRoot");
			}
			//GameObject obj = (GameObject)Instantiate(Prefab, transform.position, transform.rotation);
			//obj.transform.parent = Parent.transform;
			Destroy(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
