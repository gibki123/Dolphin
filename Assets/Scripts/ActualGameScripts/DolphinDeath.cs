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
    public Slider breathSlider;

    void OnCollisionEnter(Collision collision){
        OnDeath();
    }

    public void ChangeHighestScoreText() {
        text.text = string.Format("HIGHEST SCORE: {0}", PlayerPrefs.GetInt("HighestScore"));
    }

    public void UpdateTotalNumberOfFish() {
        PlayerPrefs.SetInt("TotalNumberOfFish", PlayerPrefs.GetInt("TotalNumberOfFish") + FishCounter.fishQuantity);
    }

    void Update() {
        Debug.Log(FishCounter.fishQuantity);
        if (breathSlider.value == 0) {
            OnDeath();
        }
    }

    void OnDeath() {
        GameState.gamePlay = false;
        deathMask.DOSizeDelta(new Vector2(loosePanel.sizeDelta.x, loosePanel.sizeDelta.y), 1f);
        PlayerPrefs.SetInt("FishScore", FishCounter.fishQuantity + PlayerPrefs.GetInt("FishScore"));
        if (FishCounter.fishQuantity > PlayerPrefs.GetInt("HighestScore")) {
            PlayerPrefs.SetInt("HighestScore", FishCounter.fishQuantity);
        }
        PlayerPrefs.Save();
        ChangeHighestScoreText();
        GameState.state = GameState.gameState.Menu;
        UpdateTotalNumberOfFish();
        FishCounter.fishQuantity = 0;
        TotalFishInGameUpdate.updateFish?.Invoke();
    }
}
