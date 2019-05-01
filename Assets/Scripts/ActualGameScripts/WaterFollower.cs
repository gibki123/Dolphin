using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollower : MonoBehaviour
{
    public Transform dolphinTransform;
    Vector3 waterPosition;

    void Update()
    {
        waterPosition = new Vector3(dolphinTransform.position.x, transform.position.y, transform.position.z);
        transform.position = waterPosition;
    }

}
