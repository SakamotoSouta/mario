using UnityEngine;
using System.Collections;

public class PlayerSEManager : MonoBehaviour {

	// プレイヤーの声再生用ラベル
	public enum PLAYER_SE_LABEL{
		PLAYER_SE_JUMP = 0,
		PLAYER_SE_PAWER_UP,
		PLAYER_SE_PAWER_DOWN,
		PLAYER_SE_DEAD,
		PLAYER_SE_CLEAR,
		PLAYER_SE_MAX
	}

	private AudioSource Audio;
	// SEの箱
	public AudioClip JumpSE;
	public AudioClip PawerUpSE;
	public AudioClip PawerDownSE;
	public AudioClip DeadSE;
	public AudioClip ClearSE;

	// Use this for initialization
	void Start () {
		Audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ラベルを送ってSEを再生
	 public void PlayerSEPlay(PLAYER_SE_LABEL Label){
		switch(Label){
		case PLAYER_SE_LABEL.PLAYER_SE_JUMP:
			Audio.PlayOneShot(JumpSE);
			break;
		case PLAYER_SE_LABEL.PLAYER_SE_PAWER_UP:
			Audio.PlayOneShot(PawerUpSE);
			break;
		case PLAYER_SE_LABEL.PLAYER_SE_PAWER_DOWN:
			Audio.PlayOneShot(PawerDownSE);
			break;
		case PLAYER_SE_LABEL.PLAYER_SE_DEAD:
			Audio.PlayOneShot(DeadSE);
			break;
		case PLAYER_SE_LABEL.PLAYER_SE_CLEAR:
			Audio.PlayOneShot(ClearSE);
			break;
		default:
			break;
		}
	}

}
