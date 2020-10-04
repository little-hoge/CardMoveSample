//using UnityEngine;
//using UnityEngine.EventSystems;
//using DG.Tweening;

//public class CardMove : MonoBehaviour, IDragHandler {

//    /// <summary> 横操作の制限 </summary>
//    private const float limitPosMinX = -2.3f, limitPosMaxX = 2.3f;

//    /// <summary> 縦操作の制限 </summary>
//    private const float limitPosMinY = -4.6f, limitPosMaxY = 4.6f;

//    /// <summary> 目的地の位置 </summary>
//    public Transform destPos;

//    /// <summary> 選択状態 </summary>
//    public bool select;

//    /// <summary> 選択された時の位置 </summary>
//    public Vector3 selectCardPos, selectCursorPos;

//    /// <summary> 移動速度 </summary>
//    float moveSpeed = 1.5f;

//    /// <summary> 現在再生中のTween </summary>
//    private Tween tween;

//    // Sequence mySequence = DOTween.Sequence();
//    private void Start() {
//        var playerDest = GameObject.Find("PlayerDest").transform;

//        // カード情報
//        int index = 0;
//        foreach (Transform childTransform in playerDest) {
//            var destStr = "Destination (" + index + ")";
//            var cardStr = "Card (" + index + ")";

//            // 自分の目的地オブジェクト検索
//            if ((childTransform.name == "Destination" && this.transform.name == "Card")
//                || (childTransform.name == destStr && this.transform.name == cardStr)
//                ) {
//                destPos = childTransform.transform;
//                break;
//            }
//            index++;
//        }

//    }

//    private void Update() {

//        // 複数選択されていた時
//        if (!select) return;

//        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        screenPos.z = 0;
//        var Pos = screenPos;
//        Pos.x = selectCardPos.x + (screenPos.x - selectCursorPos.x);
//        Pos.y = selectCardPos.y + (screenPos.y - selectCursorPos.y);
//        destPos.position = Pos;


//        // 範囲制限
//        destPos.transform.position = new Vector3(
//                  Mathf.Clamp(destPos.transform.position.x, limitPosMinX, limitPosMaxX),
//                  Mathf.Clamp(destPos.transform.position.y, limitPosMinY, limitPosMaxY),
//            destPos.transform.position.z);

//        // 移動
//        move();

//    }

//    public void OnDrag(PointerEventData data) {

//        Vector3 screenPos = Camera.main.ScreenToWorldPoint(data.position);
//        screenPos.z = 0;
//        // 範囲制限
//        destPos.transform.position = new Vector3(
//            Mathf.Clamp(screenPos.x, limitPosMinX, limitPosMaxX),
//            Mathf.Clamp(screenPos.y, limitPosMinY, limitPosMaxY),
//            screenPos.z);

//        // 移動
//        move();
//    }


//    /// <summary>
//    /// 移動処理
//    /// </summary>
//    void move() {

//        // 既に移動中の時、破棄
//        if (this.tween != null) this.tween.Kill();

//        // 移動
//        var distance = Vector2.Distance(transform.position, destPos.transform.position);
//        tween = transform.DOMove(destPos.transform.position, distance * moveSpeed)
//            .SetEase(Ease.Flash)
//            .Play()
//            // 目標到達時破棄、ケード向き補正
//            .OnComplete(() => {
//                transform.rotation = Quaternion.Euler(Vector3.zero);
//                this.tween = null;
//            });

//        // 回転
//        var vec2 = GetAngle(destPos.transform.position, transform.position);
//        transform.DORotate(new Vector3(0, 0, vec2 + 90), 0)
//            .SetEase(Ease.Flash)
//            .Play();
//    }

//    /// <summary>
//    /// ２点間の角度を取得
//    /// </summary>
//    /// <param name="vec1"> 比較元ベクター </param>
//    /// <param name="vec2"> 比較先ベクター </param>
//    /// <returns> 比較元から比較先の角度を返す </returns>
//    float GetAngle(Vector2 vec1, Vector2 vec2) {
//        float dx = vec2.x - vec1.x;
//        float dy = vec2.y - vec1.y;
//        float rad = Mathf.Atan2(dy, dx);
//        return rad * 180f / Mathf.PI;
//    }
//}