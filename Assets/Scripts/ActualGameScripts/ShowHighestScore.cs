using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighestScore : MonoBehaviour
{
    public Text text;

    void Awake() {
        text.text = string.Format("HIGHEST SCORE: {0}", PlayerPrefs.GetInt("HighestScore"));
    }
}
