using UnityEngine;
using System.Collections;

namespace TAK_CameraController
{
	// マウスのボタンをあらわす番号がわかりにくかったので名前を付けた
	enum MouseButtonDown
	{
		MBD_LEFT = 0,
		MBD_RIGHT,
		MBD_MIDDLE,
	};
	
	public class CameraController : MonoBehaviour
	{
		[SerializeField]	// privateなメンバもインスペクタで編集したいときに付ける
		private GameObject focusObj = null;	// 注視点となるオブジェクト
		
		private Vector3 oldPos;	// マウスの位置を保存する変数
		
		// 注視点オブジェクトが未設定の場合、新規に生成する
		void setupFocusObject(string name)
		{
			var obj = this.focusObj = new GameObject(name);
			obj.transform.position = Vector3.zero;
			
			return;
		}
		
		void Start()
		{
			// 注視点オブジェクトの有無を確認
			if (this.focusObj == null)
				this.setupFocusObject("CameraFocusObject");
			
			// 注視点オブジェクトをカメラの親にする
			transform.parent = this.focusObj.transform;

			return;
		}
		
		void Update()
		{
			// マウス関係のイベントを関数にまとめる
			this.mouseEvent();
			
			return;
		}
		
		// マウス関係のイベント
		void mouseEvent()
		{
			// マウスホイールの回転量を取得
			var delta = Input.GetAxis("Mouse ScrollWheel");
			// 回転量が0でなければホイールイベントを処理
			if (delta != 0.0f)
				this.mouseWheelEvent(delta);
			
			// マウスボタンが押されたタイミングで、マウスの位置を保存する
			if (Input.GetMouseButtonDown((int)MouseButtonDown.MBD_LEFT) ||
			    Input.GetMouseButtonDown((int)MouseButtonDown.MBD_MIDDLE) ||
			    Input.GetMouseButtonDown((int)MouseButtonDown.MBD_RIGHT))
				this.oldPos = Input.mousePosition;
			
			// マウスドラッグイベント
			this.mouseDragEvent(Input.mousePosition);
			
			return;
		}
		
		// マウスホイールイベント
		void mouseWheelEvent(float delta)
		{
			// 注視点からカメラまでのベクトルを計算
			var focusToPosition = this.transform.position - this.focusObj.transform.position;
			
			// ホイールの回転量を元に上で求めたベクトルの長さを変える
			var post = focusToPosition * (1.0f - delta);
			
			// 長さを変えたベクトルの長さが一定以上あれば、カメラの位置を変更する
			if (post.magnitude > 0.01f)
				this.transform.position = this.focusObj.transform.position + post;
			
			return;
		}
		
		// マウスドラッグイベント関数
		void mouseDragEvent(Vector3 mousePos)
		{
			// マウスの現在の位置と過去の位置から差分を求める
			var diff = mousePos - oldPos;
			
			// 差分の長さが極小数より小さかったら、ドラッグしていないと判断する
			if (diff.magnitude < Vector3.kEpsilon)
				return;
			
			if (Input.GetMouseButton((int)MouseButtonDown.MBD_LEFT))
			{
				// マウス左ボタンをドラッグした場合(なにもしない)
			}
			else if (Input.GetMouseButton((int)MouseButtonDown.MBD_MIDDLE))
			{
				// マウス中ボタンをドラッグした場合(注視点の移動)
				this.cameraTranslate(-diff / 20.0f);
				
			}
			// 現在のマウス位置を、次回のために保存する
			this.oldPos = mousePos;
			
			return;
		}
		
		// カメラを移動する関数
		void cameraTranslate(Vector3 vec)
		{
			var focusTrans = this.focusObj.transform;
			var trans = this.transform;
			
			// カメラのローカル座標軸を元に注視点オブジェクトを移動する
			focusTrans.Translate((trans.right * vec.x) + (trans.up * vec.y));
			
			return;
		}
	}
}