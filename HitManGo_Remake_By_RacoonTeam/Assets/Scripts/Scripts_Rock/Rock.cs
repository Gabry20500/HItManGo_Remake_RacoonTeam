using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Player playerRef;
    [SerializeField] Node[] nodes;

    public bool rockTriggered;

    private void Awake()
    {
        playerRef = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        rockTriggered = true;
    }
}
