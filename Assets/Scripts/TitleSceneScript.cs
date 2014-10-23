using UnityEngine;
using System.Collections;

public class TitleSceneScript : MonoBehaviour {

		void Update(){
			if(Input.GetKeyDown(KeyCode.Return)){
				FadeManager.Instance.LoadLevel("Game",1.0f);
			}
		}
}