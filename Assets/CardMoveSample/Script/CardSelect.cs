using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    /// <summary> 描く線の追加予定位置表示用コンポーネント </summary>
    private LineRenderer lineGhostRenderer;

    /// <summary> 描く線のマテリアル </summary>
    public Material lineMaterial;

    /// <summary> 描く線の色 </summary>
    public Color lineColor;

    /// <summary> 描く線の太さ </summary>
    [Range(0, 10)] public float lineWidth;

    /// <summary> 描く線の開始位置 </summary>
    private Vector3 startLinePos;

    /// <summary> 描く線の終了位置 </summary>
    private Vector3 endLinePos;

    /// <summary> 線作成キャンセルフラグ </summary>
    private bool cancelFlg;

    // 
    CardManager cardManager;

    void Start() {

        // コンポーネント取得
        this.lineGhostRenderer = GetComponent<LineRenderer>();

        // 線の幅を決める
        this.lineGhostRenderer.startWidth = lineWidth;
        this.lineGhostRenderer.endWidth = lineWidth;

        // 頂点の数を決める
        this.lineGhostRenderer.positionCount = 5;

        // キャンセルフラグ
        cancelFlg = false;

        // カードオブジェクト情報
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();

    }

    // Update is called once per frame
    void Update() {
        // キャンセル状態
        if (Input.GetMouseButtonDown(1) || Input.touchCount > 1) {
            cancelFlg = true;
        }

        // キャンセルフラグが立っている時キャンセル
        if (cancelFlg) {
            this.DeleteLineCancel();
        }

        // キャンセル解除
        if (Input.GetMouseButtonDown(0) && cancelFlg) {
            cancelFlg = false;
        }

    }

    /// <summary>
    /// 描く線の追加予定位置表示の更新
    /// </summary>
    public void GhostLineUpdate() {
        // 座標の変換を行いマウス位置を取得
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        endLinePos = Camera.main.ScreenToWorldPoint(screenPosition);

        // 不足２点分取得
        Vector3 PosX2, PosY2;
        PosX2 = startLinePos;
        PosX2.y = endLinePos.y;

        PosY2 = endLinePos;
        PosY2.y = startLinePos.y;


        // 追加した頂点の座標を設定
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 2, PosX2);
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 3, endLinePos);
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 4, PosY2);
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 5, startLinePos);

    }

    /// <summary>
    /// 描く線の開始位置情報の更新
    /// </summary>
    public void LineStartPositionUpdate() {

        // 座標の変換を行いマウス位置を取得
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        startLinePos = Camera.main.ScreenToWorldPoint(screenPosition);

    }

    /// <summary>
    /// 描く線の終了位置情報の更新
    /// </summary>
    public void LineEndPositionUpdate() {

        // 座標の変換を行いマウス位置を取得
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        endLinePos = Camera.main.ScreenToWorldPoint(screenPosition);

    }

    /// <summary>
    /// 描く線の追加予定位置表示の初期化
    /// </summary>
    public void StartLineInit() {
        // 座標の変換を行いマウス位置を取得
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
        startLinePos = Camera.main.ScreenToWorldPoint(screenPosition);

        // 追加した頂点の座標を設定
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 1, startLinePos);
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 2, startLinePos);
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 3, startLinePos);
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 4, startLinePos);
        this.lineGhostRenderer.SetPosition(lineGhostRenderer.positionCount - 5, startLinePos);

    }

    /// <summary>
    /// 範囲内オブジェクト操作
    /// </summary>
    public void SelectControl() {

        var playerCard = cardManager.GetPlayerCard();

        for (int i = 0; i < playerCard.Length; i++) {
            Vector2 cardPos = playerCard[i].transform.position;
            Vector2 startPos, endPos;
            startPos.x = System.Math.Max(startLinePos.x, endLinePos.x);
            startPos.y = System.Math.Max(startLinePos.y, endLinePos.y);
            endPos.x = System.Math.Min(startLinePos.x, endLinePos.x);
            endPos.y = System.Math.Min(startLinePos.y, endLinePos.y);

            // 範囲内
            if (startPos.x >= cardPos.x && endPos.x <= cardPos.x
                && startPos.y >= cardPos.y && endPos.y <= cardPos.y) {
                playerCard[i].GetComponent<CardMove>().select = true;
                playerCard[i].GetComponent<CardMove>().selectCardPos = playerCard[i].GetComponent<CardMove>().transform.position;
                playerCard[i].GetComponent<CardMove>().selectCursorPos = endLinePos;

            }
            else {
                playerCard[i].GetComponent<CardMove>().select = false;
            }

        }

    }

    public void OnBeginDrag(PointerEventData data) {
        // キャンセル時無効
        if (cancelFlg) return;

        // 追加予定位置表示の初期化
        this.StartLineInit();

        // 開始位置情報の更新
        this.LineStartPositionUpdate();

    }

    public void OnDrag(PointerEventData data) {
        // キャンセル時無効
        if (cancelFlg) return;

        this.GhostLineUpdate();
    }

    public void OnEndDrag(PointerEventData data) {
        // キャンセル時無効
        if (cancelFlg) return;

        // 範囲内オブジェクト操作
        this.SelectControl();

        // 終了位置情報の更新
        this.LineEndPositionUpdate();

        // 追加予定位置表示の初期化
        this.StartLineInit();

    }

    /// <summary>
    /// 描く線のキャンセル
    /// </summary>
    public void DeleteLineCancel() {

        // 作成予定位置初期化
        StartLineInit();

    }

}
