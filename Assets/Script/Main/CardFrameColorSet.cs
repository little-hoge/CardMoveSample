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
        playerCard = GameObject.FindGameObjectsWithTag("PlayerCard");
        playerDest = GameObject.FindGameObjectsWithTag("PlayerDestination");

        // 自分のカード枠
        for (int i = 0; i < playerCard.Length; i++) {
            switch (playerCard[i].transform.name) {
                case "Card":
                    playerCard[i].GetComponent<Image>().color = Color.red;
                    playerDest[i].GetComponent<Image>().color = Color.red;
                    playerCard[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                    playerDest[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                    break;
                case "Card (1)":
                    playerCard[i].GetComponent<Image>().color = Color.green;
                    playerDest[i].GetComponent<Image>().color = Color.green;
                    playerCard[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                    playerDest[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                    break;
                case "Card (2)":
                    playerCard[i].GetComponent<Image>().color = Color.blue;
                    playerDest[i].GetComponent<Image>().color = Color.blue;
                    playerCard[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                    playerDest[i].transform.localPosition = new Vector3(defaultPosX + posX * i, playertPosY, 0);
                    break;
                default:
                    Debug.Log("想定外");
                    break;
            }
        }
    }

}
