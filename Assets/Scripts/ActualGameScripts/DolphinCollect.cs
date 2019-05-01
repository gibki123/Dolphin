using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DolphinCollect : MonoBehaviour
{
    Text score;
    private void OnTriggerEnter(Collider other) {
         if(other.transform.tag == "CollectableFish") {
            other.gameObject.SetActive(false);
            FishCounter.Instance.IncrementScore();
        }
    }
}
