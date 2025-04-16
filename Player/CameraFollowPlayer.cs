using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        player = transform.Find("Player");
    }
}
