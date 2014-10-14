using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Block))]
public class BlockEditer : Editor {

	public override void OnInspectorGUI()
	{
		Block obj = target as Block;
		obj.BlockType = (Block.BLOCK_TYPE)EditorGUILayout.EnumPopup ("BlockType", obj.BlockType);
		obj.BlockLife = EditorGUILayout.IntField("BlockLife", obj.BlockLife);
		if (obj.BlockType == Block.BLOCK_TYPE.ITEM_BLOCK) {
			obj.ItemType = (ItemController.ITEM_TYPE)EditorGUILayout.EnumPopup ("ItemObject", obj.ItemType);
		}
		EditorUtility.SetDirty( target );
	}	
}
