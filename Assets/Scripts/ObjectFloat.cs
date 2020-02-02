using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFloat : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody myRb;
    private float force = 30f;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();

        direction.x = Random.Range(-force, force);
        direction.y = Random.Range(-force, force);

        myRb.AddForce(direction, ForceMode.Impulse);
    }
}
