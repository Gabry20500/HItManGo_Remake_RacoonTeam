using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] List<Node> activableNodes;
    [SerializeField] Player playerRef;


    private void Awake()
    {
        playerRef = FindObjectOfType<Player>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerRef.ActivateRockState();

            foreach (Node node in activableNodes)
            {
                node.ActivateMyButton();
            }
            this.gameObject.SetActive(false);
        }
    }
}
