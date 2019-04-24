using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DolphinDeath : MonoBehaviour
{
    public RectTransform deathMask;
    public RectTransform loosePanel;
    public Text text;

    void OnCollisionEnter(Collision collision)
    {
        GameState.gamePlay = false;
        deathMask.DOSizeDelta(new Vector2(loosePanel.sizeDelta.x, loosePanel.sizeDelta.y), 1f);
        PlayerPrefs.SetInt("FishScore",FishCounter.fishQuantity + PlayerPrefs.GetInt("FishScore"));
        if(FishCounter.fishQuantity > PlayerPrefs.GetInt("HighestScore")) {
            PlayerPrefs.SetInt("HighestScore", FishCounter.fishQuantity);
        }
        PlayerPrefs.Save();
        ChangeHighestScoreText();
        GameState.state = GameState.gameState.Menu;
    }



    public void ChangeHighestScoreText() {
        text.text = string.Format("HIGHEST SCORE: {0}", PlayerPrefs.GetInt("HighestScore"));
    }
}
