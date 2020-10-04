using System;
using UnityEngine;


public class CardManager : MonoBehaviour {

    /// <summary> カードリスト </summary>
    private GameObject[] playerCard = new GameObject[0];

    /// <summary> 目的地リスト </summary>
    private GameObject[] playerDest = new GameObject[0];

    private void Start() {      
        Init();
    }

    public void Init() {
        var PlayerDest = GameObject.Find("PlayerDest").transform;
        var PlayerCard = GameObject.Find("PlayerCard").transform;
       
        // カード情報
        int index = 0;
        foreach (Transform childTransform in PlayerDest) {
            var cardStr = "Destination (" + index + ")";
            if (childTransform.name == "Destination" || childTransform.name == cardStr) {
                Array.Resize(ref playerDest, index + 1);
                playerDest[index] = childTransform.gameObject;
            }
            index++;
        }
        index = 0;
        foreach (Transform childTransform in PlayerCard) {
            var cardStr = "Card (" + index + ")";
            if (childTransform.name == "Card" || childTransform.name == cardStr) {
                Array.Resize(ref playerCard, index + 1);
                playerCard[index] = childTransform.gameObject;

            }
            index++;
        }
    }

    // 取得用関数
    public GameObject[] GetPlayerCard() {
        return playerCard;
    }
    public GameObject[] GetPlayerDest() {
        return playerDest;
    }
}