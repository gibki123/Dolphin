using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DolphinDeath : MonoBehaviour
{
    public RectTransform deathMask;
    public RectTransform loosePanel;
    public Rigidbody dolphinRig;
    public Text text;
    public Slider breathSlider;
    public static bool additionalLife;

    private void Awake() {
        dolphinRig = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision){
        if (additionalLife) {
            OnAdditionalLifeLose();
        }
        else {
            OnDeath();
        }
    }

    public void ChangeHighestScoreText() {
        text.text = string.Format("HIGHEST SCORE: {0}", PlayerPrefs.GetInt("HighestScore"));
    }

    public void UpdateTotalNumberOfFish() {
        PlayerPrefs.SetInt("TotalNumberOfFish", PlayerPrefs.GetInt("TotalNumberOfFish") + FishCounter.fishQuantity);
    }

    void Update() {
        if (breathSlider.value == 0) {
            OnDeath();
        }
    }
    //Fish Score saved to PleyerPrefs file
    //Update Total number of fish
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
    // If upgrade is bought then afeter first collision you dont die and impact from collision is changed to 0
    void OnAdditionalLifeLose() {
        dolphinRig.angularVelocity = new Vector3(0, 0, 0);
        dolphinRig.velocity = new Vector3(GetComponent<DolphinMovement>().swimSpeed,0, 0);
        additionalLife = false;
    }
}
