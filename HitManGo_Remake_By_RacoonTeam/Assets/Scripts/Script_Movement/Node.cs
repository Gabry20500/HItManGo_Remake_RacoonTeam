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
        DrawLines();
        position = transform.position;
        myButton = gameObject.transform.GetChild(0).GetComponent<SelectableButton>();
    }

    public void ActivateMyButton()
    {
        myButton.gameObject.SetActive(true);
    }

    public void DisableMyButton()
    {
        myButton.gameObject.SetActive(false);
    }

    private void DrawLines()
    {
        GameObject line = GameManager.instance.linePrefab;
        Vector3 distance;
        Vector3 dir;
        foreach(Node node in linkedNodes)
        {
            distance = node.transform.position - transform.position;
            dir = distance.normalized;
            float dot = Vector3.Dot(dir, Vector3.right);
            GameObject go = Instantiate(line, transform.position + distance / 3, new Quaternion(line.transform.rotation.x, line.transform.rotation.y, line.transform.rotation.z , line.transform.rotation.w));
            go.transform.up = dir;
        }

    }
}
