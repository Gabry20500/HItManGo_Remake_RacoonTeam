using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 position;
    [SerializeField] public List<Node> linkedNodes;

    public List<Node> history;

    private void Awake()
    {
        position = transform.position;
    }
}
