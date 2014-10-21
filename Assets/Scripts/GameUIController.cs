using UnityEngine;
using System.Collections;

public class GameUIController : MonoBehaviour {

	public GameObject GameRule;
	private GameRule Rule;
	private GameObject Player;
	private	PlayerController pc;
	// プレハブ
	public GameObject labelPrefab;
	public GameObject texturePrefab;

	public Texture lifeTextureData;

	// 共通項目
	private UILabel Text;
	private UITexture Image;
	private UIAnchor Offset;
	// スコア表示
	private GameObject scoreLabel;
	// 残機表示
	private GameObject lifeLabel;
	private GameObject lifeTexture;
	// 残り時間
	private GameObject timeLabel;

	// 親オブジェクト
	GameObject Parent;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		pc = Player.GetComponent ("PlayerController") as PlayerController;

		// ゲーム」管理者の取得
		Rule = GameRule.GetComponent ("GameRule") as GameRule;
		// 親オブジェクトの検索
		Parent = GameObject.Find ("Panel");
		// スコア文字の生成
		scoreLabel = NGUITools.AddChild (Parent, labelPrefab);
		InitScoreUI ();

		// 残機文字の生成
		lifeLabel = NGUITools.AddChild (Parent, labelPrefab);
		// 残機テクスチャの生成
		lifeTexture = NGUITools.AddChild (Parent, texturePrefab);
		InitLifeUI ();

		// 時間文字の生成
		timeLabel = NGUITools.AddChild (Parent, labelPrefab);
		InitTimeUI ();
	}
	
	// Update is called once per frame
	void Update () {
		if (pc.Goal) {
			ClearUI();
		}
		// 各種UIの表示処理
		ScoreUI ();
		TimeUI ();
		LifeUI ();
	}

	// スコア表示の初期化
	void InitScoreUI(){
		scoreLabel.transform.localScale = new Vector2 (30f, 30f);
	}
	// 時間表示の初期化
	void InitTimeUI(){
		timeLabel.transform.localScale = new Vector2 (30f, 30f);
	}

	// 残機表示の初期化
	void InitLifeUI(){
		lifeLabel.transform.localScale = new Vector2 (30f, 30f);
		lifeTexture.transform.localScale = new Vector2 (30f, 30f);
	}

	// スコアの表示
	void ScoreUI(){
		Text = scoreLabel.GetComponent("UILabel") as UILabel;
		Text.pivot = UIWidget.Pivot.Left;
		Offset = scoreLabel.GetComponent ("UIAnchor") as UIAnchor;
		Offset.side = UIAnchor.Side.TopLeft;
		Offset.pixelOffset = new Vector2 (10, -40);
		int score = Rule.GetScore();
		Text.text = string.Format ("Score : {0:D4}", score);
	}

	// 時間の表示
	void TimeUI(){
		Text = timeLabel.GetComponent("UILabel") as UILabel;
		Text.pivot = UIWidget.Pivot.Left;
		Offset = timeLabel.GetComponent ("UIAnchor") as UIAnchor;
		Offset.side = UIAnchor.Side.Top;
		Offset.pixelOffset = new Vector2 (-50, -40);

		Text.text = "Time : " + (int)Rule.GameTime;
	}

	// 残機表示
	void LifeUI(){
		Text = lifeLabel.GetComponent("UILabel") as UILabel;
		Text.pivot = UIWidget.Pivot.Left;
		Offset = lifeLabel.GetComponent ("UIAnchor") as UIAnchor;
		Offset.side = UIAnchor.Side.TopRight;
		Offset.pixelOffset = new Vector2 (-100, -40);
		
		Text.text = "×" + Rule.GetLife();

		Image = lifeTexture.GetComponent ("UITexture") as UITexture;
		Image.mainTexture = lifeTextureData;
		Offset = lifeTexture.GetComponent ("UIAnchor") as UIAnchor;
		Offset.side = UIAnchor.Side.TopRight;
		Offset.pixelOffset = new Vector2 (-130, -40);
	}

	void ClearUI(){
		GameObject clearLabel = NGUITools.AddChild (Parent, labelPrefab);
		clearLabel.transform.localScale = new Vector2 (30f, 30f);
		Text = clearLabel.GetComponent ("UILabel") as UILabel;
		Text.pivot = UIWidget.Pivot.Center;
		Offset = timeLabel.GetComponent ("UIAnchor") as UIAnchor;
		Offset.side = UIAnchor.Side.Top;
		Text.text = "Clear!!";
	}

}
