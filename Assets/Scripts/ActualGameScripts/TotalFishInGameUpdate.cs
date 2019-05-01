using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TotalFishInGameUpdate : MonoBehaviour
{
    public Text text;
    public delegate void UpdateFish();
    public static UpdateFish updateFish;

    private void Update() {
      //  Debug.Log(PlayerPrefs.GetInt("TotalNumberOfFish"));
    }

    private void Awake() {
        updateFish += UpdateText;
        updateFish?.Invoke();
    }

    void UpdateText() {
        text.text = string.Format("TOTAL FISH: {0}", PlayerPrefs.GetInt("TotalNumberOfFish"));
    }
}
