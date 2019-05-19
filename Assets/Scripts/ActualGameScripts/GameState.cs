using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class which is responsible for state of game
//this is place from where game know whether state is game or menu
//it also have an initial player position
public class GameState : MonoBehaviour {


    public enum gameState { Menu, Game };
    public static gameState state;
    public Transform dolphinPosition;
    public static bool gamePlay = false;
    public static Vector3 InitialPlayerPosition;
    public static Quaternion InitialPlayerRotation;
    public static bool[] upgradesBought = new bool[3];

    void Awake() {
        state = gameState.Menu;
        InitialPlayerPosition = dolphinPosition.position;
        InitialPlayerRotation = dolphinPosition.rotation;
        InitShop();

    }


    // Initializes concrete items in the shop it checks from PlayerPrefs file if there are bought or not
    void InitShop() {
        if (PlayerPrefs.GetString("ShieldBought") == "true") {
            upgradesBought[1] = true;
        }
        else {
            upgradesBought[1] = false;
        }
        if (PlayerPrefs.GetString("MaskBought") == "true") {
            upgradesBought[0] = true;
        }
        else {
            upgradesBought[0] = false;
        }
        if (PlayerPrefs.GetString("DoubleFishBought") == "true") {
            upgradesBought[2] = true;
        }
        else {
            upgradesBought[2] = false;
        }
    }
}
