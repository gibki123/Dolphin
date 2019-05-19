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

    public void OnClickRightArrow() {
        if(currentUpgrade < ShopItemsGenerator.listCount - 1) {
            //DOTween external library for canvas animations
            //DoTween.Kill kills alle animations which were performed on this object
            DOTween.Kill(this);
            currentUpgrade++;
            transform.DOLocalMoveX(- currentUpgrade*ShopItemsGenerator.positionDifference, 0.5f);         
        }
    }

    public void OnClickLeftArrow() {
        if (currentUpgrade > 0) {
            DOTween.Kill(this);
            currentUpgrade--;
            transform.DOLocalMoveX(-currentUpgrade * ShopItemsGenerator.positionDifference, 0.5f);
        }
    }

    void CalculateMarginalDistance() {
        marginalDistance = ShopItemsGenerator.listCount * ShopItemsGenerator.positionDifference - ShopItemsGenerator.positionDifference; 
    }

    //Funcion Check if there is possibility to buy concrete upgrade if yes it refreshes UI of the canvas and apply an upgrade
    public void TryBuy() {
        int totalNumberOfFish = PlayerPrefs.GetInt("TotalNumberOfFish");
        if (currentUpgrade == 0 && ShopItemsGenerator.Instance.shopList[0].bought == false) {
            if(totalNumberOfFish >= ShopItemsGenerator.Instance.shopList[0].itemCost && PlayerPrefs.GetString("MaskBought") != "true") {
                PlayerPrefs.SetString("MaskBought","true");
                GameState.upgradesBought[0] = true;
                ShopItemsGenerator.Instance.shopList[0].bought = true;
                PlayerPrefs.SetInt("TotalNumberOfFish", totalNumberOfFish - ShopItemsGenerator.Instance.shopList[0].itemCost);
                ShopItemsGenerator.Instance.RefreshUI();
            }
        }
        if (currentUpgrade == 1 && ShopItemsGenerator.Instance.shopList[1].bought == false) {
            if (totalNumberOfFish >= ShopItemsGenerator.Instance.shopList[1].itemCost && PlayerPrefs.GetString("ShieldBought") != "true") {
                PlayerPrefs.SetString("ShieldBought", "true");
                GameState.upgradesBought[1] = true;
                ShopItemsGenerator.Instance.shopList[1].bought = true;
                PlayerPrefs.SetInt("TotalNumberOfFish", totalNumberOfFish - ShopItemsGenerator.Instance.shopList[1].itemCost);
                ShopItemsGenerator.Instance.RefreshUI();

            }
        }
        if (currentUpgrade == 2 && ShopItemsGenerator.Instance.shopList[2].bought == false) {
            if (totalNumberOfFish >= ShopItemsGenerator.Instance.shopList[2].itemCost && PlayerPrefs.GetString("DoubleFishBought") != "true") {
                PlayerPrefs.SetString("DoubleFishBought", "true");
                GameState.upgradesBought[2] = true;
                ShopItemsGenerator.Instance.shopList[2].bought = true;
                PlayerPrefs.SetInt("TotalNumberOfFish", totalNumberOfFish - ShopItemsGenerator.Instance.shopList[2].itemCost);
                ShopItemsGenerator.Instance.RefreshUI();
            }
        }
    }




}
