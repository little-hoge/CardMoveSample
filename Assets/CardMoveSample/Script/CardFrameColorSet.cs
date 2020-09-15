using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFrameColorSet : MonoBehaviour {

    /// <summary> 自分のカードリスト </summary>
    GameObject[] playerCard = null;

    /// <summary> 自分の目的地リスト </summary>
    GameObject[] playerDest = null;

    // テストパラメータ
    private const int defaultPosX = -260;
    private const int posX = 150;
    private const int playertPosY = -30;

    // Start is called before the first frame update
    void Start() {
        Color[] ColorArray = { Color.red, Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow, Color.white, new Color(1.0f, 0.5f, 0.0f, 1.0f) };
        playerCard = GameObject.FindGameObjectsWithTag("PlayerCard");
        playerDest = GameObject.FindGameObjectsWithTag("PlayerDestination");
        ColorArray = Shuffle(ColorArray, ColorArray.Length);

        // 自分のカード枠
        for (int i = 0; i < playerCard.Length; i++) {
            var colorIndex = (i+1)%ColorArray.Length;
            var cardStr = "Card (" + i + ")";
            if (playerCard[i].transform.name == "Card") {
                playerCard[i].GetComponent<Image>().color = ColorArray[colorIndex];
                playerDest[i].GetComponent<Image>().color = ColorArray[colorIndex];
                playerCard[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                playerDest[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);

            }
            else if (playerCard[i].transform.name == cardStr) {
                playerCard[i].GetComponent<Image>().color = ColorArray[colorIndex];
                playerDest[i].GetComponent<Image>().color = ColorArray[colorIndex];
                playerCard[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                playerDest[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);

            }
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
