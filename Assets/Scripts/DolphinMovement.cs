using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolphinMovement : MonoBehaviour
{
    public Vector3 swimForce;
    public float swimSpeed;
    public Collider waterCollider;

    private Rigidbody rb;
    void Awake() {
        rb = GetComponent<Rigidbody>();
        ButtonHandler.OnClickStartButton += StartDolphin;
        enabled = false;
        rb.useGravity = false;
    }

    public void StartDolphin()
    {
        rb.velocity = new Vector3(swimSpeed, 0);
        GameState.gamePlay = true;
    }

    void OnTriggerStay(Collider other)
    {
        if(other = waterCollider)
        {
            if (Input.GetButton("Fire1") && GameState.gamePlay == true)
            {
                rb.AddForce(swimForce * Time.deltaTime);
            }
        }
    }
}
