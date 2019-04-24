using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FishCounter : MonoBehaviour
{
    public static FishCounter Instance;

    [System.NonSerialized]
    public static int fishQuantity;

    Text score;
  
    void Awake() {
        Instance = this;
        fishQuantity = 0;
        score = GetComponent<Text>();
    }

    public void IncrementScore() {
        fishQuantity++;
        score.text = String.Format("Fish: {0}",fishQuantity);
    }
}
