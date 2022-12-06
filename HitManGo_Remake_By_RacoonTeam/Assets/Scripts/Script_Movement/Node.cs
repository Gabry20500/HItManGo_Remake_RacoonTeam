using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3 position;
    [SerializeField] public List<Node> linkedNodes;
    [SerializeField] private SelectableButton myButton;

    public List<Node> history;

    private void Awake()
    {
        position = transform.position;
    }

    public void ActivateMyButton()
    {
        myButton.enabled = true;
    }

    public void DisableMyButton()
    {
        myButton.enabled = false;
    }
}
