using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFrameColorSet : MonoBehaviour {

    // サンプル用
    private const int defaultPosX = -260;
    private const int posX = 150;
    private const int playertPosY = -30;

    // Start is called before the first frame update
    void Start() {
        Color[] ColorArray = { Color.red, Color.green, Color.blue, Color.cyan, Color.magenta, Color.yellow, Color.white, new Color(1.0f, 0.5f, 0.0f, 1.0f) };
        ColorArray = Shuffle(ColorArray, ColorArray.Length);

        var playerCard = GameObject.Find("CardGroup").transform;
        var playerDest = GameObject.Find("DestGroup").transform;

        // カード情報
        int index = 0;
        foreach (Transform childTransform in playerCard) {
            var cardStr = "Card (" + index + ")";
            if (childTransform.name == "Card" || childTransform.name == cardStr) {
                childTransform.GetComponent<Image>().color = ColorArray[index];
                childTransform.localPosition = new Vector3(defaultPosX + posX * index, playertPosY, 0);
                childTransform.localPosition = new Vector3(defaultPosX + posX * index, playertPosY, 0);
            }
            index++;
        }
        
        index = 0;
        foreach (Transform childTransform in playerDest) {
            var cardStr = "Destination (" + index + ")";
            if (childTransform.name == "Destination" || childTransform.name == cardStr) {
                childTransform.GetComponent<Image>().color = ColorArray[index];
                childTransform.localPosition = new Vector3(defaultPosX + posX * index, playertPosY, 0);
                childTransform.localPosition = new Vector3(defaultPosX + posX * index, playertPosY, 0);
            }
            index++;
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
