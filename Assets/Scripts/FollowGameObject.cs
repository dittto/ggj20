using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    public GameObject gameObjectToFollow;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObjectToFollow.transform.position;
    }
}
