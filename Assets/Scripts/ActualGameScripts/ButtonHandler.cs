using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour {

    public delegate void Button();
    public static Button OnClickStartButton;
    public static Button OnClickShopButton;
    public static Button OnClickTryAgainButton;
    public static Button OnClickBackButton;
    public RectTransform fishCounterScore;
    public RectTransform slider;
    public RectTransform gameName;
    public RectTransform gameButtons;
    public RectTransform shopWindow;
    public RectTransform deathMaskPanel;
    public Transform dolphinPosition;
    public DolphinMovement movementScript;
    public Rigidbody dolphinRig;
    public Slider breathSlider;
    public Text fishCounter;


    void Awake() {
        OnClickStartButton += MoveObjects;
        OnClickStartButton += EnableMoveScript;
        OnClickStartButton += ChangeGameStateToGame;
        OnClickStartButton += slideInGameUI;
        OnClickStartButton += SetAdditionalLife;
        OnClickShopButton += MoveObjects;
        OnClickShopButton += ShopClick;
        OnClickTryAgainButton += HideDeathPanel;
        OnClickTryAgainButton += MoveBackObjects;
        OnClickTryAgainButton += ReturnDolphinInitPositionRotation;       
        OnClickTryAgainButton += DisableMoveScript;
        OnClickTryAgainButton += ReturnBreathSliderValue;
        OnClickTryAgainButton += ReturnFishCounter;
        OnClickTryAgainButton += slideOutGameUI;
        OnClickBackButton += MoveBackObjects;


    }

    public void ClickStartButton() {
        OnClickStartButton.Invoke();
    }

    public void ClickBackButton() {
        OnClickBackButton.Invoke();
    }

    public void ClickShopButton() {
        OnClickShopButton.Invoke();
    }

    public void ClickTryAgainButton() {
        OnClickTryAgainButton.Invoke();
    }

    void MoveObjects() {
        gameButtons.DOLocalMoveY(-225f, 1f);
        gameName.DOAnchorPosY(gameName.sizeDelta.y, 1f);
    }

    void ShopClick() {
        shopWindow.DOLocalMoveX(0, 1.5f);
    }


    void EnableMoveScript() {
        dolphinRig.useGravity = true;
        movementScript.enabled = true;
    }

    void MoveBackObjects() {
        gameButtons.DOLocalMoveY(0, 1f);
        gameName.DOAnchorPosY(0, 1f);
        shopWindow.DOLocalMoveX(700, 1.5f);
    }

    void ReturnDolphinInitPositionRotation() {
        dolphinPosition.position = GameState.InitialPlayerPosition;
        dolphinPosition.rotation = GameState.InitialPlayerRotation;
    }

    void HideDeathPanel() {
        DOTween.KillAll();
        deathMaskPanel.DOSizeDelta(new Vector2(0, 0), 0); 
    }

    //Turns off dolphins movement
    void DisableMoveScript() {
        dolphinRig.velocity = new Vector3(0, 0, 0);
        dolphinRig.angularVelocity = new Vector3(0, 0, 0);
        dolphinRig.useGravity = false;
        movementScript.enabled = false;
    }

    void ChangeGameStateToGame() {
        GameState.state = GameState.gameState.Game;
    }

    void ReturnBreathSliderValue() {
        breathSlider.value = 1;
    }

    void ReturnFishCounter() {
        fishCounter.text = "Fish: 0";
    }

    void slideInGameUI() {
        slider.DOAnchorPosY(-5, 1f);
        fishCounterScore.DOAnchorPosY(-5, 1f);
    }

    void slideOutGameUI() {
        slider.DOAnchorPosY(30, 1f);
        fishCounterScore.DOAnchorPosY(30, 1f);
    }

    void SetAdditionalLife() {
        DolphinDeath.additionalLife = GameState.upgradesBought[1];
    }

    public void ExitApp() {
        Application.Quit();
    }
}
