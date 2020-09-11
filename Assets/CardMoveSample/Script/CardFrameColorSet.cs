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

        // 自分のカード枠
        for (int i = 0; i < playerCard.Length; i++) {
            var cardStr = "Card (" + i + ")";
            var ColorIndex = Random.Range(0, ColorArray.Length);
            if (playerCard[i].transform.name == "Card") {
                playerCard[i].GetComponent<Image>().color = ColorArray[ColorIndex];
                playerDest[i].GetComponent<Image>().color = ColorArray[ColorIndex];
                playerCard[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                playerDest[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);

            }
            else if (playerCard[i].transform.name == cardStr) {
                playerCard[i].GetComponent<Image>().color = ColorArray[ColorIndex];
                playerDest[i].GetComponent<Image>().color = ColorArray[ColorIndex];
                playerCard[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                playerDest[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);

            }
        }
    }
}
