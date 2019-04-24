using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour {

    public delegate void StartButton();
    public static StartButton OnClickStartButton;
    public delegate void ShopButton();
    public static ShopButton OnClickShoptButton;
    public delegate void TryAgainButton();
    public static TryAgainButton OnClickTryAgainButton;
    public RectTransform gameName;
    public RectTransform gameButtons;
    public RectTransform shopWindow;
    public RectTransform deathMaskPanel;
    public Transform dolphinPosition;
    public DolphinMovement movementScript;
    public Rigidbody dolphinRig;


    void Awake() {
        OnClickStartButton += MoveObjects;
        OnClickStartButton += EnableMoveScript;
        OnClickStartButton += ChangeGameStateToGame;
        OnClickShoptButton += MoveObjects;
        OnClickShoptButton += ShopClick;
        OnClickTryAgainButton += MoveBackObjects;
        OnClickTryAgainButton += ReturnDolphinInitPositionRotation;
        OnClickTryAgainButton += HideDeathPanel;
        OnClickTryAgainButton += DisableMoveScript;
    }

    public void ClickStartButton() {
        OnClickStartButton.Invoke();
    }

    public void ClickShopButton() {
        OnClickShoptButton.Invoke();
    }

    public void ClickTryAgainButton() {
        OnClickTryAgainButton.Invoke();
    }

    void MoveObjects() {
        gameButtons.DOLocalMoveY(-225f, 1f);
        gameName.DOAnchorPosY(gameName.sizeDelta.y, 1f);
    }

    void ShopClick() {
        shopWindow.DOLocalMoveX(0, 2f);
    }


    void EnableMoveScript() {
        dolphinRig.useGravity = true;
        movementScript.enabled = true;
    }

    void MoveBackObjects() {
        gameButtons.DOLocalMoveY(0, 1f);
        gameName.DOAnchorPosY(0, 1f);
    }

    void ReturnDolphinInitPositionRotation() {
        dolphinPosition.position = GameState.InitialPlayerPosition;
        dolphinPosition.rotation = GameState.InitialPlayerRotation;
    }

    void HideDeathPanel() {
        deathMaskPanel.sizeDelta = new Vector2(0, 0);
    }

    void DisableMoveScript() {
        dolphinRig.velocity = new Vector3(0, 0, 0);
        dolphinRig.angularVelocity = new Vector3(0, 0, 0);
        dolphinRig.useGravity = false;
        movementScript.enabled = false;
    }

    void ChangeGameStateToGame() {
        GameState.state = GameState.gameState.Game;
    }
}
