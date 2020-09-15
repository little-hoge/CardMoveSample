//using UnityEngine;
//using UnityEngine.EventSystems;
//using DG.Tweening;
//using System;

//public class CardMoveManager : MonoBehaviour, IDragHandler {

//    /// <summary> 横操作の制限 </summary>
//    private const float limitPosMinX = -2.3f, limitPosMaxX = 2.3f;

//    /// <summary> 縦操作の制限 </summary>
//    private const float limitPosMinY = -4.6f, limitPosMaxY = 4.6f;

//    /// <summary> 目的地の位置 </summary>
//    public Transform destPos;

//    /// <summary> 移動速度 </summary>
//    float moveSpeed = 1.5f;

//    /// <summary> 自分の目的地リスト </summary>
//    GameObject[] playerDest = null;

//    /// <summary> 現在再生中のTween </summary>
//    private Tween tween;

//   // Sequence mySequence = DOTween.Sequence();
//    private void Start() {
//        playerDest = GameObject.FindGameObjectsWithTag("PlayerDestination");

//        // 自分のカード
//        if (transform.tag == "PlayerCard") {
//            for (int i = 0; i < playerDest.Length; i++) {

//                var cardStr = "Card (" + i + ")";
//                var destStr = "Destination (" + i + ")";

//                if (transform.name == "Card") {
//                    if (playerDest[i].name == "Destination") destPos = playerDest[i].transform;
//                }
//                else if (transform.name == cardStr) {
//                    if (playerDest[i].name == destStr) destPos = playerDest[i].transform;
//                }
//            }
//        }
//    }

//    public void OnDrag(PointerEventData data) {

//        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(data.position);
//        TargetPos.z = 0;

//        // 範囲制限
//        destPos.transform.position = new Vector3(
//            Mathf.Clamp(TargetPos.x, limitPosMinX, limitPosMaxX),
//            Mathf.Clamp(TargetPos.y, limitPosMinY, limitPosMaxY),
//            TargetPos.z);

//        // 既に移動中の時、破棄
//        if (this.tween != null) this.tween.Kill();

//        // 移動
//        var distance = Vector2.Distance(transform.position, destPos.transform.position);
//        tween = transform.DOMove(destPos.transform.position, distance * moveSpeed)
//            .SetEase(Ease.Flash)
//            .Play()
//            // 目標到達時破棄、ケード向き補正
//            .OnComplete(() => { transform.rotation = Quaternion.Euler(Vector3.zero); this.tween = null; });

//        // 回転
//        var vec2 = GetAngle(destPos.transform.position, transform.position);
//        transform.DORotate(new Vector3(0, 0, vec2 + 90), 0).SetEase(Ease.Flash).Play();

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