using UnityEngine;
using System.Collections;

public class GridPositionOffset : MonoBehaviour {
	ColliderController Contriller;

	// Use this for initialization
	void Start () {
		Contriller = GetComponent<ColliderController> ();
		transform.Translate(Contriller._gridSize * (Contriller._Size.x / 2), (Contriller._gridSize * (Contriller._Size.y / 2)), 0f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
