using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject levelPawn;
    [SerializeField] public GameObject linePrefab;
    public GameObject buttons;

    [SerializeField] private int lastLevelCompleted = 0;
    [SerializeField] private int maxLevelCompleted = 0;
    [SerializeField] public List<ClickablePlane> levelButtons;


    private void Start()
    {
        UnlockLevelButton();
    }

    public void LoadLevel(string levelName)
    {
        buttons.SetActive(false);
        SceneManager.LoadScene(levelName);
    }

    public void SetLastLevelCompleted(int levelCompleted)
    {
        lastLevelCompleted = levelCompleted;
        if(maxLevelCompleted < lastLevelCompleted)
        {
            maxLevelCompleted = lastLevelCompleted;
        }
    }

    public void UnlockLevelButton()
    {   
        StartCoroutine(Animation(levelPawn, levelButtons[maxLevelCompleted].transform.position));
        
    }


    private IEnumerator Animation(GameObject toMove, Vector3 destination)
    {
        Vector3 startPos = toMove.transform.position;
        Vector3 endPos = new Vector3(destination.x, toMove.transform.position.y, destination.z);

        Vector3 dir = (endPos + startPos) / 2;
        Vector3 midPos = new Vector3(dir.x, (dir.y + 30f), dir.z);
        yield return StartCoroutine(Wait(1f));
        yield return StartCoroutine(goMidPos(toMove, startPos, midPos));
        yield return StartCoroutine(goEndPos(toMove, midPos, endPos));

        toMove.transform.position = endPos;
        for (int i = 0; i <= maxLevelCompleted; i++)
        {
            levelButtons[i].Unlock();
        }
        yield return null;
    }

    private IEnumerator goMidPos(GameObject toMove, Vector3 startPos, Vector3 midPos)
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float perc = elapsed / duration;

            toMove.transform.position = Vector3.Lerp(startPos, midPos, perc);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator goEndPos(GameObject toMove, Vector3 midPos, Vector3 endPos)
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float perc = elapsed / duration;

            toMove.transform.position = Vector3.Lerp(midPos, endPos, perc);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
