using UnityEngine;
using System.Collections;

public class GridPositionOffset : MonoBehaviour {
	Grid _Grid;

	// Use this for initialization
	void Start () {
		_Grid = GetComponent<Grid> ();

		switch (_Grid.face) {
		case Grid.Face.xy:
			transform.Translate(_Grid.gridSize * (_Grid.size / 2), (_Grid.gridSize * (_Grid.size / 2)), 0f);
			break;
		case Grid.Face.zx:
			transform.position = new Vector3 ();
			break;
		case Grid.Face.yz:
			transform.position = new Vector3 ();
			break;
		default:
			transform.position = new Vector3 ();
			break;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
