using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 position;
    [SerializeField] public List<Node> linkedNodes;

    private void Awake()
    {
        position = transform.position;
    }
}
