using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public bool firstPlay = true;
    public static GameManager instance;
    public int lastLevelCompleted;
    private int currentLevel;
    public List<ClickablePlane> levelButton;

    private void Start()
    {
        lastLevelCompleted = 0;
        currentLevel = 1;
        
        UpdatePlanesLevel();
    }

    public void LoadLevel(string sceneName)
    {
        
        
    }

    public void ReloadLevelSelection()
    {
        
    }

    private void UpdatePlanesLevel()
    {
        for (int i = 4; i >= 0; i--)
        {
            if (i == lastLevelCompleted)
            {
                levelButton[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
