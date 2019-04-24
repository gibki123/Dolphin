using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    private Transform tsm;

    void Awake() {
        transform.position = offset;
        tsm = player.GetComponent<Transform>();
    }
    void Update() {
        transform.position = offset + tsm.position;
    }
}
