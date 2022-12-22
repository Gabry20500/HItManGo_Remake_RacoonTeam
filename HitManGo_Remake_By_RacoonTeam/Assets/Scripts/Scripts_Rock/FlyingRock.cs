using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRock : MonoBehaviour
{
    public Node throwDestination;
    private Player playerRef;

    [SerializeField] GameObject triggerPlaneObj;

    // Update is called once per frame
    public void Init(Node destination)
    {
        throwDestination = destination;
        StartCoroutine(Animation(throwDestination.position));
    }

    private void Update()
    {
        transform.Rotate(Vector3.right + Vector3.up);
    }

    private IEnumerator Animation(Vector3 destination)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(destination.x, transform.position.y, destination.z);

        Vector3 dir = (endPos + startPos) / 2;
        Vector3 midPos = new Vector3(dir.x, (dir.y + 15f), dir.z);

        yield return StartCoroutine(goMidPos(startPos, midPos));
        yield return StartCoroutine(goEndPos(midPos, endPos));

        transform.position = endPos;

        yield return null;
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        
        GameObject g =  Instantiate(triggerPlaneObj,new Vector3(endPos.x, endPos.y , endPos.z), rotation);
        g.GetComponent<TriggerPlane>().Init(throwDestination);
        
        Destroy(gameObject);
        yield return null;
    }

    private IEnumerator goMidPos(Vector3 startPos, Vector3 midPos)
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float perc = elapsed / duration;

            transform.position = Vector3.Lerp(startPos, midPos, perc);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator goEndPos(Vector3 midPos, Vector3 endPos)
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float perc = elapsed / duration;

            transform.position = Vector3.Lerp(midPos, endPos, perc);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
