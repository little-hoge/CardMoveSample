using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFrameColorSet : MonoBehaviour {

    // 
    CardManager cardManager;

    // Start is called before the first frame update
    void Start() {
        cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();

        // 読み込み完了後
        Invoke("Init", 0.2f);
       
    }

    private void Init() {
        var playerCard = cardManager.GetPlayerCard();
        var playerDest = cardManager.GetPlayerDest();

        Color[] ColorArray = { Color.red, Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow, Color.white, new Color(1.0f, 0.5f, 0.0f, 1.0f) };
        ColorArray = Shuffle(ColorArray, ColorArray.Length);

        // 初期設定
        for (int index = 0; index < playerCard.Length; index++) {
            playerCard[index].GetComponent<Image>().color = ColorArray[index];
        }
        for (int index = 0; index < playerDest.Length; index++) {
            playerDest[index].GetComponent<Image>().color = ColorArray[index];
           }

    }

    // シャッフル
    Color[] Shuffle(Color[] color, int maxIndex)
    {
        while (maxIndex > 1) {
            maxIndex--;
            int randIndex = UnityEngine.Random.Range(0, maxIndex + 1);

            // 入れ替え
            Color temp = color[randIndex];
            color[randIndex] = color[maxIndex];
            color[maxIndex] = temp;
        }
        return color;
    }
}
