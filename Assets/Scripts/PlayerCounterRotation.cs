using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterRotation : MonoBehaviour
{
    public Transform directionHolder;

    // Update is called once per frame
    void Update()
    {
        //directionHolder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, directionHolder.transform.rotation.z - gameObject.transform.rotation.z);
    }
}
