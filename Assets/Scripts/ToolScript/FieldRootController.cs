using UnityEngine;
using System.Collections;
using System.Linq;
public class FieldRootController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void AllDelete(){
		foreach ( var n in gameObject.GetChildren(false) )
		{
			Debug.Log( n.name );
			Destroy(n);
			// Base
			// Button
		}
	}
}

public static class GameObjectExtensions
{

	// すべての子オブジェクトを返す
	public static GameObject[] GetChildren( 
	                                       this GameObject self, 
	                                       bool includeInactive )
	{
		return self.GetComponentsInChildren<Transform>( includeInactive )
			.Where( c => c != self.transform )
				.Select( c => c.gameObject )
				.ToArray();
	}
}

public static class ComponentExtensions
{
	// すべての子オブジェクトを返す
	public static GameObject[] GetChildren( 
	                                       this Component self, 
	                                       bool includeInactive )
	{
		return self.GetComponentsInChildren<Transform>( includeInactive )
			.Where( c => c != self.transform )
				.Select( c => c.gameObject )
				.ToArray();
	}
}

