using UnityEngine;
using UnityEngine.EventSystems;

public class CardMoveManager : MonoBehaviour, IDragHandler {

    /// <summary> 横操作の制限 </summary>
    private const float limitPosMinX = -2.3f, limitPosMaxX = 2.3f;

    /// <summary> 縦操作の制限 </summary>
    private const float limitPosMinY = -4.6f, limitPosMaxY = 4.6f;

    /// <summary> 目的地の位置 </summary>
    public Transform destPos;

    /// <summary> 移動速度 </summary>
    float moveSpeed = 0.01f;

    /// <summary> ラジアン変数 </summary>
    private float rad;

    /// <summary> 現在位置 </summary>
    private Vector3 Position;

    private void Start() {

        var playerDest = GameObject.Find("DestGroup").transform;

        // カード情報
        int index = 0;
        foreach (Transform childTransform in playerDest) {
            var destStr = "Destination (" + index + ")";
            var cardStr = "Card (" + index + ")";

            // 自分の目的地オブジェクト検索
            if ((childTransform.name == "Destination" && this.transform.name == "Card")
                || (childTransform.name == destStr && this.transform.name == cardStr)
                ) {
                destPos = childTransform.transform;
                break;
            }
            index++;
        }

        Position = new Vector2(destPos.position.x, destPos.position.y);
    }

    private void Update() {

        var lastPosition = Position;

        transform.position = destPos.transform.position;

        // 移動
        Position.x += moveSpeed * Mathf.Cos(rad);
        Position.y += moveSpeed * Mathf.Sin(rad);

        // 移動距離が一定以上の時移動
        if (Vector2.Distance(lastPosition, transform.position) > moveSpeed) {

            // 位置更新
            transform.position = Position;
        }
        else {
            // 前回値
            Position = lastPosition;
        }
        // 目標方向に回転
        var vec = (destPos.transform.position - transform.transform.position).normalized;
        transform.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);
    }

    public void OnDrag(PointerEventData data) {
        Vector3 TargetPos = Camera.main.ScreenToWorldPoint(data.position);
        TargetPos.z = 0;
        destPos.transform.position = TargetPos;

        // 範囲制限
        destPos.transform.position = new Vector3(
                  Mathf.Clamp(destPos.transform.position.x, limitPosMinX, limitPosMaxX),
                  Mathf.Clamp(destPos.transform.position.y, limitPosMinY, limitPosMaxY),
            destPos.transform.position.z);

        rad = Mathf.Atan2(
            destPos.transform.position.y - transform.position.y,
            destPos.transform.position.x - transform.position.x);
    }
}