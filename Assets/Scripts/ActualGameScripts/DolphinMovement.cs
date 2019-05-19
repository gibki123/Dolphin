using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DolphinMovement : MonoBehaviour
{
    public Vector3 swimForce;
    public float swimSpeed;
    public Collider waterCollider;
    public Slider breathingSlider;
    public float holdingBreathTime = 10;
    public float breathRegenerationTime = 3;
    bool inWater;
    private Rigidbody rb;
    void Awake() {
        inWater = true;
        rb = GetComponent<Rigidbody>();
        ButtonHandler.OnClickStartButton += StartDolphin;
        enabled = false;
        rb.useGravity = false;
    }

    void Update() {
        if (!inWater) {
            RegenerateBreath();
        }
    }

    public void StartDolphin()
    {
        rb.velocity = new Vector3(swimSpeed, 0);
        GameState.gamePlay = true;
    }

    //If Dolphin is inside object named water
    void OnTriggerStay(Collider other)
    {
        if(other == waterCollider && GameState.gamePlay == true)
        {
            inWater = true;
            if (Input.GetButton("Fire1"))
            {
                rb.AddForce(swimForce * Time.deltaTime);
            }
            if (!GameState.upgradesBought[0]) {
                breathingSlider.value -= 1 / holdingBreathTime * Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if(other == waterCollider){
            inWater = false;
        }
    }

    void RegenerateBreath() {
        breathingSlider.value += 1 / breathRegenerationTime * Time.deltaTime;
    }


}
