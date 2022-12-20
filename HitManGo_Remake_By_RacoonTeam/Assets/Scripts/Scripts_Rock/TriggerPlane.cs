using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlane : MonoBehaviour
{
    [SerializeField] private float scaleVelocity = 2f;
    
    private Node destination;
    private NodesPathFinding DFS;
    
    private void Awake()
    {
        DFS = new NodesPathFinding();
    }

    public void Init(Node enemyDestination)
    {
        destination = enemyDestination;
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        float elapsed = 0f;
        float duration = 2f;
        
        
        while (elapsed < duration)
        {
            transform.localScale = new Vector3(transform.localScale.x + scaleVelocity * Time.deltaTime,
                transform.localScale.y, transform.localScale.z + scaleVelocity * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(.5f);
        
        elapsed = 0f;
        
        while (elapsed < duration)
        {
            transform.localScale = new Vector3(transform.localScale.x - scaleVelocity * Time.deltaTime,
                transform.localScale.y, transform.localScale.z - scaleVelocity * Time.deltaTime);

            elapsed += Time.deltaTime;
            yield return null;
        }
        
        FindObjectOfType<LevelManager>().playerRef.DisableRockState();
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            
            List<Node> temp = DFS.Depth_First_Search(enemy.currentNode, destination);
            temp.Remove(temp[0]);
            enemy.pathToDestination = temp;
            enemy.destinationNode = destination;
            
            enemy.RotateTo(temp[0]);
        }
    }
}
