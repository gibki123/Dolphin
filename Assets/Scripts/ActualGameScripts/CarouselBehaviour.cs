using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarouselBehaviour : MonoBehaviour {


    float marginalDistance;
    int currentUpgrade;

    void Start() {
        currentUpgrade = 0;
        CalculateMarginalDistance();
    }

    public void OnClickLeftArrow() {
        if(currentUpgrade < ShopItemsGenerator.listCount - 1) {
            DOTween.Kill(this);
            currentUpgrade++;
            transform.DOLocalMoveX(- currentUpgrade*ShopItemsGenerator.positionDifference, 0.5f);         
        }
    }

    public void OnClickRightArrow() {
        if (currentUpgrade > 0) {
            DOTween.Kill(this);
            currentUpgrade--;
            transform.DOLocalMoveX(-currentUpgrade * ShopItemsGenerator.positionDifference, 0.5f);
        }
    }

    void CalculateMarginalDistance() {
        marginalDistance = ShopItemsGenerator.listCount * ShopItemsGenerator.positionDifference - ShopItemsGenerator.positionDifference; 
    }
}
