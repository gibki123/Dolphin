using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public enum gameState {Menu,Game};
    public static gameState state;
    public Transform dolphinPosition;
    public static bool gamePlay = false;
    public static Vector3 InitialPlayerPosition;
    public static Quaternion InitialPlayerRotation;
    void Awake() {
        state = gameState.Menu;
        InitialPlayerPosition = dolphinPosition.position;
        InitialPlayerRotation = dolphinPosition.rotation;
    }
}
