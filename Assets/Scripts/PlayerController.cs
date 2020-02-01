using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 100f;
    private Rigidbody2D playerRB;
    public ParticleSystem thrustParticles;
    public KeyCode controlKey;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(controlKey))
        {
            Debug.Log("Space Pressed");
            playerRB.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
            thrustParticles.Play();
        }
    }
}
